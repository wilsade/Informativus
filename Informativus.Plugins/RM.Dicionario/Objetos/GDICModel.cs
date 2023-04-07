using System.Diagnostics;

namespace RM.Dicionario.Objetos
{
  /// <summary>
  /// Representa um item do dicionário de dados (GDIC)
  /// </summary>
  [DebuggerDisplay("{Coluna,nq}")]
  public class GDICModel
  {
    /// <summary>
    /// Tabela
    /// </summary>
    public string Tabela { get; set; }

    /// <summary>
    /// Coluna
    /// </summary>
    public string Coluna { get; set; }

    /// <summary>
    /// Descricao
    /// </summary>
    public string Descricao { get; set; }

    /// <summary>
    /// Relatorio
    /// </summary>
    public short? Relatorio { get; set; }

    /// <summary>
    /// Aplicacoes
    /// </summary>
    public string Aplicacoes { get; set; }
  }
}
