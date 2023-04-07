using System.IO;
using System.Xml.Serialization;

using WSP.Lib;

using ConstantesInfor = Informativus.Comuns.Constantes;

namespace RM.Dicionario.Serialization
{
  /// <summary>
  /// Gerenciar a serialização da configuração
  /// </summary>
  public class MainConfig
  {
    static MainConfig _instance;
    readonly string _fileName;

    /// <summary>
    /// Inicialização da classe: <see cref="MainConfig"/>.
    /// </summary>
    private MainConfig()
    {
      _fileName = Path.Combine(ConstantesInfor.LocalApplicationDataPath,
        "Plugins",
        "RMDicionarioConfig.xml");
    }

    /// <summary>
    /// Devolve a instãncia da classe: <see cref="MainConfig"/>
    /// </summary>
    /// <value>Instância da classe: <see cref="MainConfig"/></value>
    public static MainConfig Instance
    {
      get
      {
        if (_instance == null)
          _instance = new MainConfig();
        return _instance;
      }
    }

    /// <summary>
    /// Ler a configuração
    /// </summary>
    /// <returns>Instância de configuração</returns>
    public DicionarioConfig Get()
    {
      if (!File.Exists(_fileName))
        return new DicionarioConfig();
      var instance = WSPXMLSerializerObj.ReadXMLParams<DicionarioConfig>(_fileName);
      return instance;
    }

    /// <summary>
    /// Salvar a configuração
    /// </summary>
    /// <param name="dicionarioConfig">Configurações para o Plugin</param>
    public void Save(DicionarioConfig dicionarioConfig)
    {
      if (!Directory.Exists(Path.GetDirectoryName(_fileName)))
        Directory.CreateDirectory(Path.GetDirectoryName(_fileName));
      WSPXMLSerializerObj.SaveXMLParams(dicionarioConfig, _fileName);
    }
  }

  /// <summary>
  /// Configurações para o Plugin
  /// </summary>
  [XmlRoot(ElementName = "RMDicionarioConfig")]
  public class DicionarioConfig
  {
    /// <summary>
    /// Objeto para Geração de código
    /// </summary>
    public GeracaoCodigoConfig GeracaoCodigo { get; set; } = new GeracaoCodigoConfig();
  }

  /// <summary>
  /// Configurações para a Geração de código
  /// </summary>
  public class GeracaoCodigoConfig
  {
    /// <summary>
    /// Sigla do aplicativo
    /// </summary>
    [XmlAttribute]
    public string Sigla { get; set; }
  }
}
