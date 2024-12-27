
using System.Collections.Generic;
using System.Diagnostics;

using Informativus.Comuns.PlugIns;

using WSP.Lib;
using WSP.Lib.Models;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer informações do dicionário de dados: Tabela e Colunas
  /// Implementa a interface: <see cref="IInformativusDicionario"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: Dicionário de dados", "Wilsade - Github", "2020/2", "1.0", "Dicionário de dados básico")]
  public class DicionarioDadosExample : IInformativusDicionario
  {
    /// <summary>
    /// Provê informações do banco de dados conectado
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// Fechar a conexão com o banco
    /// </summary>
    public void CloseConnection()
    {
      Trace.WriteLine("Caso seja necessário, feche quaisquer conexões aqui");
    }

    /// <summary>
    /// Dicionário de dados de uma tabela
    /// </summary>
    /// <param name="tableModel">Tabela do banco de dados</param>
    /// <returns>Interface que "conhece" a descrição de uma tabela e de seus campos</returns>
    public IInformativusMembrosDicionarioTabela GetDicionarioTabela(TableModel tableModel)
    {
      return new MeuDicionarioObj(tableModel, DbInfo);
    }
  }

  /// <summary>
  /// Fornecer informações de uma tabela e seus campos
  /// Implementa a interface: <see cref="IInformativusMembrosDicionarioTabela"/>
  /// </summary>
  class MeuDicionarioObj : IInformativusMembrosDicionarioTabela
  {
    private readonly IParametrosAcessoBanco _dbInfo;

    public MeuDicionarioObj(TableModel tableModel, IParametrosAcessoBanco dbInfo)
    {
      Tabela = tableModel;
      _dbInfo = dbInfo;
    }

    bool IsPerson => _dbInfo.DataBaseName.Equals("AdventureWorks") && Tabela.ToString().Equals("Person.Person");

    /// <summary>
    /// Tabela
    /// </summary>
    public TableModel Tabela { get; }

    /// <summary>
    /// Descrição da tabela
    /// </summary>
    public string DescricaoTabela
    {
      get
      {
        if (IsPerson)
          return "Pessoa";
        return Tabela.ToString();
      }
    }

    /// <summary>
    /// Estrutura que contém as informações dos campos de uma tabela
    /// </summary>
    public List<ColumnModel> DicionarioCampos
    {
      get
      {
        if (IsPerson)
        {
          return new List<ColumnModel>
          {
            new ColumnModel()
            {
              Nome = "FirstName",
              Descricao = "Primeiro nome"
            }
          };
        }
        return new List<ColumnModel>();
      }
    }

    public void ConfigureColumns(System.Windows.Forms.DataGridViewColumnCollection columns)
    {
      Trace.WriteLine("Esta configuração é opcional");
    }
  }
}
