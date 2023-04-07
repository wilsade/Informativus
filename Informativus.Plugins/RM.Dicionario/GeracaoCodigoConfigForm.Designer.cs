
namespace RM.Dicionario
{
  partial class GeracaoCodigoConfigForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancelar = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.edtSigla = new System.Windows.Forms.TextBox();
      this.lbExemplo = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(316, 160);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 4;
      this.btnOK.Text = "&OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // btnCancelar
      // 
      this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancelar.Location = new System.Drawing.Point(397, 160);
      this.btnCancelar.Name = "btnCancelar";
      this.btnCancelar.Size = new System.Drawing.Size(75, 23);
      this.btnCancelar.TabIndex = 5;
      this.btnCancelar.Text = "&Cancelar";
      this.btnCancelar.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 20);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(458, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "A geração de Props / ParamsProc é feita através do Menu de contexto do Dicionário" +
    " de dados.";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(319, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "O Resource que será gerado será composto pela sigla do sistema.";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.ForeColor = System.Drawing.Color.Blue;
      this.label3.Location = new System.Drawing.Point(12, 75);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(229, 13);
      this.label3.TabIndex = 2;
      this.label3.Text = "Informe a sigla do módulo do sistema (Ex: Edu):";
      // 
      // edtSigla
      // 
      this.edtSigla.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.edtSigla.Location = new System.Drawing.Point(12, 91);
      this.edtSigla.MaxLength = 5;
      this.edtSigla.Name = "edtSigla";
      this.edtSigla.Size = new System.Drawing.Size(100, 26);
      this.edtSigla.TabIndex = 3;
      this.edtSigla.TextChanged += new System.EventHandler(this.edtSigla_TextChanged);
      // 
      // lbExemplo
      // 
      this.lbExemplo.AutoSize = true;
      this.lbExemplo.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbExemplo.ForeColor = System.Drawing.Color.Red;
      this.lbExemplo.Location = new System.Drawing.Point(12, 120);
      this.lbExemplo.Name = "lbExemplo";
      this.lbExemplo.Size = new System.Drawing.Size(70, 15);
      this.lbExemplo.TabIndex = 6;
      this.lbExemplo.Text = "lbExemplo";
      // 
      // GeracaoCodigoConfigForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(480, 191);
      this.Controls.Add(this.lbExemplo);
      this.Controls.Add(this.edtSigla);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnCancelar);
      this.Controls.Add(this.btnOK);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GeracaoCodigoConfigForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Sobre a Geração de Props / ParamsProc";
      this.Load += new System.EventHandler(this.GeracaoCodigoConfigForm_Load);
      this.Shown += new System.EventHandler(this.GeracaoCodigoConfigForm_Shown);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GeracaoCodigoConfigForm_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancelar;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtSigla;
    private System.Windows.Forms.Label lbExemplo;
  }
}