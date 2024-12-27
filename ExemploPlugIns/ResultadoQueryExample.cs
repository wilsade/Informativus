using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer menu de contexto no resultado de consultas.
  /// Implementa a interface: <see cref="IWSPDataSourceQueryResult"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: Resultado de consulta", "Wilsade - Github", "2020/2", "1.0", "Menu de contexto no resultado de consulta")]
  public class ResultadoQueryExample : IWSPDataSourceQueryResult
  {
    /// <summary>
    /// Informações do banco de dados
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// O DataSet que irá conter o resultado de uma consulta
    /// </summary>
    public DataSet ResultDataSet { get; set; }

    /// <summary>
    /// Linhas selecionadas na Grid
    /// </summary>
    public IEnumerable<object> SelectedRows { get; set; }

    /// <summary>
    /// Lista de ações disponibilizadas pela aplicação externa
    /// </summary>
    public List<IWSPContextMenuActions> Actions
    {
      get
      {
        var acao1 = new AcoesMenu("Copiar coluna 01 das linhas selecionadas", (sender, args) =>
        {
          var lst = SelectedRows.OfType<DataGridViewRow>().Select(x => Convert.ToString(x.Cells[0].Value));
          var str = string.Join(Environment.NewLine, lst);
          Clipboard.SetDataObject(str, true);
        });
        var acao2 = new AcoesMenu("Exibir total de registros", (sender, args) =>
        {
          var str = new StringBuilder();
          foreach (DataTable dataTable in ResultDataSet.Tables)
          {
            str.AppendLine($"Total de {dataTable.TableName}: {dataTable.Rows.Count}");
          }
          MessageBox.Show(str.ToString());
        });
        return new List<IWSPContextMenuActions>() { acao1, acao2 };
      }
    }
  }
}
