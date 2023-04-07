using System;
using System.Collections.Generic;
using System.Drawing;

using Informativus.Comuns.PlugIns;

using RM.Dicionario.Objetos;

using WSP.Lib;
using WSP.Lib.Models;

namespace RM.Dicionario
{
  /// <summary>
  /// Manipular uma Grid que contém o dicionário de dados
  /// <para>Implementa a interface <seealso cref="IDataGridViewDicionarioHandler"/></para>
  /// </summary>
  [PlugInIdentifier(
    "Geração de código",
    "William Sade de Paiva",
    "2018/2",
    Constantes.Versao,
    "Geração de código para desenvolvimento de classes TOTVS-SA")]
  public class MenusGridDicionario : IDataGridViewDicionarioHandler, IMainMenu
  {
    /// <summary>
    /// Inicialização da classe: <see cref="MenusGridDicionario"/>.
    /// </summary>
    public MenusGridDicionario()
    {

    }

    #region Private
    /// <summary>
    /// Objeto para geração de código
    /// </summary>
    GeracaoCodigoObj GeracaoObj
    {
      get
      {
        if (_geracaoObj == null)
          _geracaoObj = new GeracaoCodigoObj(_linhasSelecionadas);
        return _geracaoObj;
      }

    }
    GeracaoCodigoObj _geracaoObj;
    #endregion

    #region IMenuDataGridViewDicionario Members

    /// <summary>
    /// Parâmetros de acesso ao banco
    /// </summary>
    public IParametrosAcessoBanco DbInfo { get; set; }

    /// <summary>
    /// (Gets or sets) Linhas selecionadas no dicionário de dados
    /// </summary>
    public List<ColumnModel> LinhasSelecionadas
    {
      get { return _linhasSelecionadas; }
      set
      {
        _linhasSelecionadas = value;
        if (_linhasSelecionadas != null)
          GeracaoObj.LinhasSelecionadas = value;
      }
    }
    List<ColumnModel> _linhasSelecionadas;

    /// <summary>
    /// Ações que vamos disponibilizar para a aplicação que carregou nossa dll
    /// </summary>
    public List<IWSPContextMenuActions> Actions
    {
      get
      {
        if (_listaAcoes == null)
          _listaAcoes = GeracaoObj.CriarAcoes();
        return _listaAcoes;
      }
    }

    List<IWSPContextMenuActions> _listaAcoes;

    #endregion

    #region IMainMenu Members
    /// <summary>
    /// Texto da primeira linha do botão
    /// </summary>
    string IMainMenu.RequiredCaptionLine1 => "Geração de";

    /// <summary>
    /// Texto da segunda linha do botão
    /// </summary>
    string IMainMenu.OptionalCaptionLine2 => "código";

    /// <summary>
    /// Tooltip a ser exibido no MouseHover
    /// </summary>
    string IMainMenu.ToolTip => "Sobre a Geração de código para a TOTVS...";

    /// <summary>
    /// Imagem do botão
    /// </summary>
    Image IMainMenu.ButtonImage => Properties.Resources.geracaoCodigo;

    /// <summary>
    /// Acontece quando o botão é clicado
    /// </summary>
    EventHandler<InformativusEventArgs> IMainMenu.OnClick => (a, b) =>
        {
          GeracaoObj.InputForSiglaModulo();
        };

    #endregion

  }
}
