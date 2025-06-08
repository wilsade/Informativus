using System.Windows.Forms;

namespace RM.Dicionario
{
  /// <summary>
  /// Constantes
  /// </summary>
  public static class Constantes
  {
    /// <summary>
    /// Versão
    /// </summary>
    public const string Versao = "4.0.3.2";

    /// <summary>
    /// Nome do plug-in
    /// </summary>
    public const string NomePlugIn = "PlugIn RM.Dicionario";

    /// <summary>
    /// Nome da Gdic
    /// </summary>
    public const string GDic = "GDic";

    /// <summary>
    /// TabInfo
    /// </summary>
    public const string TabInfo = "TabInfo";

    /// <summary>
    /// Sql para recuperar o dicionário de uma tabela
    /// </summary>
    public const string SqlGDic = @"SELECT GDIC.* FROM GDIC (NOLOCK) WHERE GDIC.TABELA = '{0}'";

    /// <summary>
    /// Código de Data props
    /// </summary>
    public const string DataProps = "DataProps.SetProps({0}, \"{1}\",\r\n  ColProps.Caption, Properties.Resources.S{2}Caption{3},\r\n  ColProps.MaxLength, {4},\r\n  ColProps.AllowDBNull, {5},\r\n  ColProps.Visible, true,\r\n  ColProps.AllowEdit, true,\r\n  ColProps.AllowInsert, true,\r\n  ColProps.AllowFilter, true);\r\n";

    /// <summary>
    /// Nome de Resource
    /// </summary>
    public const string Resource = "S{0}Caption{1}\t{2}";

    /// <summary>
    /// Código de Params proc
    /// </summary>
    public const string ParamsProc = "[DataMember]\r\n[ParamsProc(typeof(Properties.Resources), \"S{0}Caption{1}\")]\r\n" +
                       "public {2} {3} {4} get; set; {5}\r\n";

    /// <summary>
    /// Nome de dataTable
    /// </summary>
    public const string NomeDataTable = "dataTable";

    /// <summary>
    /// Sigla padrão de módulo
    /// </summary>
    public const string SiglaModulo = "Edu";

    /// <summary>
    /// Tamanho padrão de campo
    /// </summary>
    public const string TamanhoPadraoCampo = "-1";

    /// <summary>
    /// Indica Campo obrigatório
    /// </summary>
    public const string CampoObrigatorio = " S";

    /// <summary>
    /// Constante para False
    /// </summary>
    public const string False = "false";

    /// <summary>
    /// Constante para True
    /// </summary>
    public const string True = "true";

    /// <summary>
    /// Abre chave
    /// </summary>
    public const string AbreChave = "{";

    /// <summary>
    /// Fecha chave
    /// </summary>
    public const string FechaChave = "}";

    /// <summary>
    /// Indicador nullable
    /// </summary>
    public const string IndicadorNullable = "?";

    internal static class TnfMapConsts
    {
      public const string BuilderToTable = "builder.ToTable(\"{0}\");";
      public const string BuilderHasKey = "builder.HasKey({0});";
      public const string BuilderhasProperty = @"builder.Property(p => p.{0})
  .HasColumnName(""{1}""){2}
  .IsRequired({3});";
    }
  }

  /// <summary>
  /// Imagens
  /// </summary>
  public static class Imagens
  {
    private static ImageList InternalGetListaImagens()
    {
      ImageList lista = new ImageList
      {
        ColorDepth = ColorDepth.Depth32Bit
      };
      lista.Images.Add(Properties.Resources.Properties.ToString(), Properties.Resources.Properties);
      lista.Images.Add(Properties.Resources.MigrationWizard.ToString(), Properties.Resources.MigrationWizard);
      lista.Images.Add(Properties.Resources.icoTnf.ToString(), Properties.Resources.icoTnf);
      return lista;
    }

    /// <summary>
    /// Recuperar um ImageList
    /// </summary>
    public static ImageList ListaImagens
    {
      get
      {
        if (_listaImagens == null)
          _listaImagens = InternalGetListaImagens();
        return _listaImagens;
      }
    }
    static ImageList _listaImagens;
  }
}
