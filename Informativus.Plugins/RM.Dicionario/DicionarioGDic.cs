using Informativus.Comuns.PlugIns;

using RM.Dicionario.Objetos;

using WSP.Lib;
using WSP.Lib.Models;

namespace RM.Dicionario
{
  /// <summary>
  /// Classe para disponibilizar o dicionário de dados
  /// <para>Implementa a interface IInformativusDicionario</para>
  /// </summary>
  [PlugInIdentifier(
    "Dicionário de dados",
    "William Sade de Paiva",
    "2009",
    Constantes.Versao,
    "Provê as descrições das tabelas e colunas da base de dados da TOTVS")]
  public class DicionarioGDic : IInformativusDicionario
  {
    #region Construtor

    /// <summary>
    /// Construtor de um dicionário de dados da GDic
    /// <para>Temos que ter um construtor sem parâmetros para o Activator instanciar esta classe</para>
    /// </summary>
    public DicionarioGDic()
    {
      //_tabelas = new Hashtable();
      DbInfo = null;
    }

    #endregion     

    #region Métodos internos
    /// <summary>
    /// Recuperar a descrição da tabela e dos campos
    /// <para>Acessa a lista de tabelas e retorna o item. Se o item não existe, ele é criado</para>
    /// </summary>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <returns>IInformativusMembrosDicionarioTabela, que contém a tabela e os campos</returns>
    IInformativusMembrosDicionarioTabela GetMembrosDicionario(TableModel tableModel)
    {
      IInformativusMembrosDicionarioTabela item;
      var dicObj = new GDicObj(DbInfo);
      item = dicObj.GetMembrosDicionario(tableModel);
      return item;
    }
    #endregion

    #region IInformativusDicionario Members

    /// <summary>
    /// Provê informações do banco de dados conectado
    /// <para>Ao carregar a dll, vamos precisar "avisar" qual o banco que estamos conectado</para>
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// Dicionário de dados de uma tabela
    /// </summary>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <returns>Interface que "conhece" a descrição de uma tabela e de seus campos</returns>
    public IInformativusMembrosDicionarioTabela GetDicionarioTabela(TableModel tableModel)
    {
      return GetMembrosDicionario(tableModel);
    }

    /// <summary>
    /// Fechar a conexão
    /// </summary>
    public void CloseConnection()
    {
      System.Diagnostics.Trace.WriteLine("Dicionário GDIC não mantém conexão aberta");
    }

    #endregion
  }
}