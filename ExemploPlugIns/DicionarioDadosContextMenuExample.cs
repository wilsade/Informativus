
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Informativus.Comuns.PlugIns;

using WSP.Lib;
using WSP.Lib.Models;

namespace Informativus.ExemploPlugIns
{
  /// <summary>
  /// Fornecer menu de contexto a partir do dicionário de dados
  /// Implementa a interface: <see cref="IDataGridViewDicionarioHandler"/>
  /// </summary>
  [PlugInIdentifier("Exemplo: Menu do Dicionário de dados", "Wilsade - Github", "2020/2", "1.0", "Menu para o Dicionário de dados")]
  public class DicionarioDadosContextMenuExample : IDataGridViewDicionarioHandler
  {
    /// <summary>
    /// Provê informações do banco de dados conectado
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// Linhas selecionadas no dicionário de dados
    /// </summary>
    public List<ColumnModel> LinhasSelecionadas { get; set; }

    /// <summary>
    /// Lista de ações disponibilizadas
    /// </summary>
    public List<IWSPContextMenuActions> Actions
    {
      get
      {
        var acao = new AcoesMenu("Copiar Nome e Descrição", (sender, args) =>
        {
          var lst = LinhasSelecionadas.Select(x => $"{x.Nome};{x.Descricao}");
          var str = string.Join(Environment.NewLine, lst);
          Clipboard.SetDataObject(str, true);
        });
        return new List<IWSPContextMenuActions>() { acao };
      }
    }
  }
}
