using System;
using System.Windows.Forms;

namespace RM.Dicionario
{
  /// <summary>
  /// Tela para configuração de Geração de código
  /// </summary>
  public partial class GeracaoCodigoConfigForm : Form
  {
    /// <summary>
    /// Inicialização da classe: <see cref="GeracaoCodigoConfigForm"/>.
    /// </summary>
    public GeracaoCodigoConfigForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Sigla informada
    /// </summary>
    public string Sigla
    {
      get => edtSigla.Text;
      set => edtSigla.Text = value;
    }

    private void GeracaoCodigoConfigForm_Load(object sender, EventArgs e)
    {
      Icon = Properties.Resources.Properties;
    }

    private void GeracaoCodigoConfigForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
        Close();
      else if (e.KeyCode == Keys.Enter && btnOK.Enabled)
        DialogResult = DialogResult.OK;
    }

    private void edtSigla_TextChanged(object sender, EventArgs e)
    {
      btnOK.Enabled = !string.IsNullOrEmpty(edtSigla.Text);
      lbExemplo.Visible = btnOK.Enabled;
      lbExemplo.Text = $"S{edtSigla.Text}ExemploNome";
    }

    private void GeracaoCodigoConfigForm_Shown(object sender, EventArgs e)
    {
      edtSigla_TextChanged(sender, e);
    }
  }
}
