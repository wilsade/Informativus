using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer menu de contexto para o Editor de consultas.
  /// Implementa a interface <see cref="IWSPQueryControlContext"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: Editor de consulta", "Wilsade - Github", "2025/1", "1.0", "Menu de contexto no editor de consulta")]
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
    public List<IWSPContextMenuActionsEx> Actions
    {
      get
      {
        var imagePath = @"caminho\da\sua\imagem.png";
        var menuComSubMenu = new AcoesMenuEx("Ações do meu plug-in com submenu")
        {
          MenuImage = System.IO.File.Exists(imagePath) ? new Bitmap(imagePath) : null,
          SubMenus = new ToolStripItem[]
          {
            new ToolStripMenuItem("Exibir todo o texto do editor", image: null, onClick: (sender, args) =>
            {
              MessageBox.Show(EditorText, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }),

            new ToolStripMenuItem("Transformar o texto selecionado em minúscula", image: null, onClick: (sender, args) =>
            {
              if (!string.IsNullOrWhiteSpace(EditorSelectedText))
              {
                ChangeSelectedText(EditorSelectedText.ToLower());
              }
            })
          }
        };

        var menuUnico = new AcoesMenuEx("Banco de dados", (sender, args) =>
        {
          string msg = $"Banco de dados informado: {DbInfo}";
          MessageBox.Show(msg, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });

        return new List<IWSPContextMenuActionsEx> { menuComSubMenu, menuUnico };

      }
    }

    /// <summary>
    /// Responsável por alterar o texto selecionado do Editor
    /// </summary>
    public Action<string> ChangeSelectedText { get; set; }
  }
}
