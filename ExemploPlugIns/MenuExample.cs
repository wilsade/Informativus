using System;
using System.Drawing;
using System.Windows.Forms;

using Informativus.Comuns.PlugIns;

using WSP.Lib;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Exemplo de como inserir um botão no Menu do Informativus
  /// </summary>
  [PlugInIdentifier("Exemplo: Fornecer menu", "Wilsade - Github", "2021/1", "1.0", "Menu simples")]
  public class MenuExample : IMainMenu
  {
    /// <summary>
    /// Título do Menu
    /// </summary>
    public string RequiredCaptionLine1 => "Exibir";

    /// <summary>
    /// Título extra (opcional)
    /// </summary>
    public string OptionalCaptionLine2 => "mensagem";

    /// <summary>
    /// ToolTip que será exibido ao posicionar o Mouse sobre o botão
    /// </summary>
    public string ToolTip => "Pequena descrição";

    /// <summary>
    /// Imagem que será exibida no menu
    /// </summary>
    public Image ButtonImage => SystemIcons.Information.ToBitmap();

    /// <summary>
    /// Acontece quando o menu é clicado
    /// </summary>
    public EventHandler<InformativusEventArgs> OnClick => (sender, e) =>
      {
        string msg = "Você pode definir configurações para o seu plugIn aqui.";
        if (e.ParametrosBanco != null)
        {
          msg += Environment.NewLine +
            "Informações da base de dados conectada:" + Environment.NewLine +
            $"- Servidor: {e.ParametrosBanco.ServerName}" + Environment.NewLine +
            $"- Base de dados: {e.ParametrosBanco.DataBaseName}" + Environment.NewLine +
            $"- Segurança integrada: {e.ParametrosBanco.IntegratedSecurity}";
          if (!e.ParametrosBanco.IntegratedSecurity)
            msg += Environment.NewLine
            + $"- Usuário: {e.ParametrosBanco.UserName}";
        }
        MessageBox.Show(msg, "Informações", MessageBoxButtons.OK, MessageBoxIcon.Information);
      };
  }
}
