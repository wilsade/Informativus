using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer menu de contexto para o Editor de consultas.
  /// Implementa a interface <see cref="IWSPQueryControlContext"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: Editor de consulta", "Wilsade", "2025/1", "1.0", "Menu de contexto no editor de consulta")]
  public class QueryControlExample : IWSPQueryControlContext
  {
    /// <summary>
    /// Inicialização da classe: <see cref="QueryControlExample"/>.
    /// </summary>
    public QueryControlExample()
    {

    }

    /// <summary>
    /// O texto do editor de consultas
    /// </summary>
    public string EditorText { get; set; }

    /// <summary>
    /// O texto selecionado no editor de consultas
    /// </summary>
    public string EditorSelectedText { get; set; }

    /// <summary>
    /// Informações do banco de dados
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// Lista de ações disponibilizadas pela aplicação externa
    /// </summary>
    public List<IWSPContextMenuActions> Actions
    {
      get
      {
        var acao1 = new AcoesMenu("Exibir todo o texto do editor", (sender, args) =>
        {
          MessageBox.Show(EditorText, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });
        var acao2 = new AcoesMenu("Transformar o texto selecionado em minúscula", (sender, args) =>
        {
          if (!string.IsNullOrWhiteSpace(EditorSelectedText))
            EditorSelectedText = EditorSelectedText.ToLower();
        });
        return new List<IWSPContextMenuActions>() { acao1, acao2 };
      }
    }
  }
}
