using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Informativus.Comuns.PlugIns;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer informações da base de dados
  /// Implementa a interface: <see cref="ILoadDatabasesInfo"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: DatabaseInfo", "Wilsade - Github", "2020/2", "1.0", "Total de tabelas da base (SQL Server)")]
  public class DatabaseInfoExample : ILoadDatabasesInfo
  {
    /// <summary>
    /// Recuperar informações de uma base de dados
    /// </summary>
    /// <param name="dbInfo">Parâmetros de acesso ao banco</param>
    /// <returns>Informações de uma base de dados</returns>
    public List<ICaptionDescription> GetDataBaseInfo(IParametrosAcessoBanco dbInfo)
    {
      if (dbInfo.TipoBanco == TipoBancoEnum.SqlServer)
      {
        var builder = new SqlConnectionStringBuilder()
        {
          DataSource = dbInfo.ServerName,
          InitialCatalog = dbInfo.DataBaseName,
          IntegratedSecurity = dbInfo.IntegratedSecurity,
          UserID = dbInfo.UserName,
          Password = dbInfo.Password
        };

        using (var connection = new SqlConnection(builder.ToString()))
        {
          connection.Open();
          const string sql = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES";
          using (var command = new SqlCommand(sql, connection))
          {
            var total = command.ExecuteScalar();
            return new List<ICaptionDescription>
            {
               new CaptionAndDescription("Total de tabelas", Convert.ToInt32(total).ToString())
            };
          }
        }
      }
      return new List<ICaptionDescription>();
    }
  }

  /// <summary>
  /// Implementar a interface: <see cref="ICaptionDescription"/>
  /// </summary>
  class CaptionAndDescription : ICaptionDescription
  {
    /// <summary>
    /// Inicialização da classe: <see cref="CaptionAndDescription"/>.
    /// </summary>
    /// <param name="caption">Rótulo</param>
    /// <param name="description">Descrição</param>
    public CaptionAndDescription(string caption, string description)
    {
      Caption = caption;
      Description = description;
    }

    /// <summary>
    /// Rótulo
    /// </summary>
    public string Caption { get; }

    /// <summary>
    /// Descrição
    /// </summary>
    public string Description { get; }
  }
}
