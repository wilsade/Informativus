using System;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer ações para a aplicação
  /// Implementa a interface: <see cref="IWSPContextMenuActions"/>
  /// </summary>
  class AcoesMenu : IWSPContextMenuActions
  {
    /// <summary>
    /// Inicialização da classe: <see cref="AcoesMenu"/>.
    /// </summary>
    /// <param name="caption">Texto do menu</param>
    /// <param name="evento">O evento a ser disparado ao clicar na ação</param>
    public AcoesMenu(string caption, EventHandler evento)
    {
      Caption = caption;
      Event = evento;
    }

    /// <summary>
    /// Texto do menu
    /// </summary>
    public string Caption { get; }

    /// <summary>
    /// A imagem que irá aparecer no menu de contexto
    /// </summary>
    public System.Drawing.Image MenuImage { get; set; }

    /// <summary>
    /// Objeto qualquer
    /// </summary>
    public object Tag { get; set; }

    /// <summary>
    /// O evento a ser disparado ao clicar na ação
    /// </summary>
    public EventHandler Event { get; }
  }
}
