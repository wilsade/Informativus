using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Informativus.Comuns.PlugIns;

using WSP.Lib;
using WSP.Lib.DataAccess;
using WSP.Lib.Extensions;
using WSP.Lib.Interfaces;
using WSP.Lib.Models;

namespace RM.Dicionario.Objetos
{
  /// <summary>
  /// Contém toda a regra para recuperação do dicionário de dados
  /// </summary>
  public class GDicObj
  {
    #region Variáveis

    /// <summary>
    /// Parâmetros de acesso ao banco
    /// </summary>
    readonly IParametrosAcessoBanco _dbParams;

    #endregion

    #region Construtor

    /// <summary>
    /// Objeto que conhece toda a regra para recuperação do dicionário de dados
    /// </summary>
    /// <param name="paramAcessoBanco"></param>
    public GDicObj(IParametrosAcessoBanco paramAcessoBanco)
    {
      _dbParams = paramAcessoBanco;
    }

    #endregion

    /// <summary>
    /// Objeto de acesso ao banco de dados
    /// </summary>
    WSPDataAccess GetData()
    {
      var par = new WSPParametrosAcessoBanco
      {
        TipoBanco = _dbParams.TipoBanco,
        ServerName = _dbParams.ServerName,
        DataBaseName = _dbParams.DataBaseName,
        IntegratedSecurity = _dbParams.IntegratedSecurity,
        UserName = _dbParams.UserName,
        Password = _dbParams.Password,
        ApplicationName = Constantes.NomePlugIn,
        IsSysDBA = _dbParams.IsSysDBA
      };

      var data = new WSPDataAccess(par)
      {
        SkipParser = true
      };
      return data;
    }

    #region Métodos internos
    /// <summary>
    /// Criar um objeto para recuperar informações físicas de uma tabela do banco de dados
    /// </summary>
    /// <param name="data">Data</param>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <returns>Meta data</returns>
    static MetaDataBase GetMetaData(WSPDataAccess data, TableModel tableModel)
    {
      return MetaDataBase.CreateInstante(data, tableModel);
    }

    /// <summary>
    ///  Recuperar todas as informações de uma tabela e de seus campos
    ///  <para>Acessa a GDIC para recuperação das descrições da tabela e dos campos</para></summary>
    /// <param name="data">Acesso ao banco</param>
    /// <param name="metaData">Objeto que conhece as informações físicas de uma tabela do banco de dados</param>
    /// <param name="descricaoTabela">Descrição da tabela (conforme GDIC)</param>
    /// <returns>Dicionário de dados</returns>
    static List<ColumnModel> GetDicionario(IWSPDapperDbs data, MetaDataBase metaData, out string descricaoTabela)
    {
      // Carregar a GDIC
      var lstModel = new List<GDICModel>();
      var gdic = metaData.GetUserTables("GDIC", DbTipoProcuraEnum.Tabelas, true);
      if (gdic.Any())
      {
        string sql = string.Format(metaData is MetaDataSqlServer ? Constantes.SqlGDic : Constantes.SqlGDic.ReplaceInsensitive("(NOLOCK)", string.Empty), metaData.Tabela.Name);
        lstModel = data.Query<GDICModel>(sql);
      }

      // Preencher a descrição do nome da tabela
      if (lstModel.Count > 0)
      {
        var row = lstModel[0];
        if (row.Coluna.Equals("#"))
          descricaoTabela = row.Descricao + " (" + row.Aplicacoes + ")";
        else
          descricaoTabela = metaData.Tabela.Name + ": " + Properties.Resources.ResNaoExisteDicionario;
      }
      else
        descricaoTabela = metaData.Tabela.Name + ": " + Properties.Resources.ResNaoExisteDicionario;

      // Preencher o dicionário
      GDICModel rowGDic;
      var lstDicionario = metaData.Campos;
      foreach (var campo in lstDicionario)
      {
        rowGDic = lstModel.FirstOrDefault(x => x.Tabela.EqualsInsensitive(metaData.Tabela.Name) &&
          x.Coluna.EqualsInsensitive(campo.Nome));

        // Descrição da coluna
        if (!string.IsNullOrEmpty(rowGDic?.Descricao))
          campo.Descricao = rowGDic.Descricao;
        else
          campo.Descricao = Properties.Resources.ResNaoExisteDicionario;

        // Tipo de dados
        if (campo.TipoFisico == campo.TipoDefinido)
          campo.TipoDefinido = ColumnModel.INDICADOR_CAMPO_NAOCHAVE_NAOOBRIGATORIO;

        // Vamos usar o Opcional1 para Aplicações
        if (rowGDic != null)
          campo.Opcional1 = rowGDic.Aplicacoes;
        else
          campo.Opcional1 = ColumnModel.INDICADOR_CAMPO_NAOCHAVE_NAOOBRIGATORIO;

        // Vamos usar o Opcional2 para o nome da tabela
        campo.Opcional2 = metaData.Tabela.Name;
      }

      return lstDicionario;
    }

    #endregion

    #region Métodos
    /// <summary>
    /// Dicionário de dados de uma tabela
    /// </summary>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <returns>Interface que "conhece" a descrição de uma tabela e de seus campos</returns>
    public IInformativusMembrosDicionarioTabela GetMembrosDicionario(TableModel tableModel)
    {
      using (var data = GetData())
      {
        MetaDataBase metaData = GetMetaData(data, tableModel);
        List<ColumnModel> lstDicionario = GetDicionario(data, metaData, out string descricaoTabela);

        MembrosDicionario membros = new MembrosDicionario(tableModel, descricaoTabela, lstDicionario);
        return membros;
      }
    }

    #endregion
  }

  /// <summary>
  /// Classe para armazenar a descrição de uma tabela e de seus campos
  /// <para>Implementa a interface IInformativusMembrosDicionarioTabela</para>
  /// </summary>
  internal class MembrosDicionario : IInformativusMembrosDicionarioTabela
  {
    /// <summary>
    /// Cria um objeto para armazenar a descrição de uma tabela e de seus campos
    /// </summary>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <param name="descricaoTabela">Descrição da tabela (conforme GDIC)</param>
    /// <param name="dicionarioCampos">Dicionário dos campos</param>
    public MembrosDicionario(TableModel tableModel, string descricaoTabela, List<ColumnModel> dicionarioCampos)
    {
      Tabela = tableModel;
      DescricaoTabela = descricaoTabela;
      DicionarioCampos = dicionarioCampos;
    }

    #region IInformativusMembrosDicionarioTabela Members

    /// <summary>
    /// Nome da tabela
    /// </summary>
    public TableModel Tabela { get; }

    /// <summary>
    /// Descrição da tabela
    /// </summary>
    public string DescricaoTabela { get; }

    /// <summary>
    /// Dicionário de campos da tabela
    /// </summary>
    public List<ColumnModel> DicionarioCampos { get; }

    /// <summary>
    /// Configurar as colunas da Grid de Dicionário
    /// </summary>
    /// <param name="columns">Columns</param>
    public void ConfigureColumns(DataGridViewColumnCollection columns)
    {
      var coluna = columns[nameof(ColumnModel.Opcional1)];
      if (coluna != null)
      {
        coluna.HeaderText = Properties.Resources.ResAplicacoes;
        coluna.DisplayIndex = 10;
      }

      coluna = columns[nameof(ColumnModel.DefaultValue)];
      if (coluna != null)
      {
        coluna.DisplayIndex = 11;
      }

      coluna = columns[nameof(ColumnModel.Seed_Value)];
      if (coluna != null)
      {
        coluna.DisplayIndex = 12;
      }

      coluna = columns[nameof(ColumnModel.Increment_Value)];
      if (coluna != null)
      {
        coluna.DisplayIndex = 13;
      }
    }

    #endregion
  }

}
