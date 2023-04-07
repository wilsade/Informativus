using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RM.Dicionario.Serialization;

using WSP.Lib;
using WSP.Lib.Models;

namespace RM.Dicionario.Objetos
{
  /// <summary>
  /// Classe para gerenciar geração de código da TOTVS:
  /// <para>Gerar DataProps</para>
  /// <para>Gerar ParamsProc</para>
  /// </summary>
  public class GeracaoCodigoObj
  {
    string _siglaModulo;

    /// <summary>
    /// Estrutura para agrupar informações de um determinado campo
    /// </summary>
    struct Campo
    {
      /// <summary>
      /// Nome do campo
      /// </summary>
      public string Nome;
      /// <summary>
      /// Tipo físico do banco
      /// </summary>
      public string TipoFisico;
      /// <summary>
      /// Tamanho
      /// </summary>
      public string Tamanho;
      /// <summary>
      /// Se o campo permite ou não valores nulos
      /// </summary>
      public bool IsNullable;
      /// <summary>
      /// Descricão do campo (conforme dicionário)
      /// </summary>
      public string Descricao;
      /// <summary>
      /// Precisão
      /// </summary>
      public int? Precisao;
    }

    #region Construtor
    /// <summary>
    /// Constrói um objeto para gerenciar geração de código da TOTVS
    /// </summary>
    /// <param name="linhasSelecionadas">Linhas selecionadas no dicionário de dados.</param>
    /// <exception cref="Exception">Fonte de dados não é um dicionário de dados</exception>
    public GeracaoCodigoObj(List<ColumnModel> linhasSelecionadas)
    {
      LinhasSelecionadas = linhasSelecionadas;
      _siglaModulo = MainConfig.Instance.Get().GeracaoCodigo.Sigla;
    }
    #endregion

    #region Private

    /// <summary>
    /// Cria um "Campo" baseado em uma row
    /// </summary>
    /// <param name="row">Row do dicionário de dados</param>
    /// <returns>Uma estrutura de "Campo" conforme informações da Row</returns>
    static Campo CampoInfo(ColumnModel row)
    {
      var campo = new Campo();
      if (row != null)
      {
        campo.Nome = row.Nome;
        campo.TipoFisico = row.TipoFisico;
        campo.Tamanho = Convert.ToString(row.Tamanho);
        campo.IsNullable = row.IsNullable;
        campo.Descricao = row.Descricao;
        campo.Precisao = row.Precisao;
      }
      return campo;
    }

    /// <summary>
    /// Sigla do módulo. Ex: Edu, Rhu
    /// </summary>
    private string SiglaModulo
    {
      get
      {
        if (string.IsNullOrEmpty(_siglaModulo))
          InputForSiglaModulo();
        return _siglaModulo;
      }
    }

    /// <summary>
    /// Input para Sigla de Módulo
    /// </summary>
    public void InputForSiglaModulo()
    {
      using (var form = new GeracaoCodigoConfigForm())
      {
        form.Sigla = _siglaModulo;
        if (form.ShowDialog() == DialogResult.OK)
        {
          var config = MainConfig.Instance.Get();
          config.GeracaoCodigo.Sigla = form.Sigla;
          MainConfig.Instance.Save(config);
          _siglaModulo = form.Sigla;
        }
      }
    }

    private void MenuItem_GerarDataProps(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(SiglaModulo))
      {
        StringBuilder strBuilder = new StringBuilder();
        StringBuilder strResources = new StringBuilder();
        foreach (ColumnModel row in LinhasSelecionadas.OrderBy(x => x.Id))
        {
          strBuilder.AppendLine(GerarDataProps(row));
          strResources.AppendLine(GerarResources(row));
        }

        if (strBuilder.Length > 0)
        {
          Clipboard.SetDataObject(strBuilder.AppendLine(Environment.NewLine).Append(strResources.ToString()).ToString(), true);
          MessageBox.Show(Properties.Resources.ResCopiadoAreaTransferencia,
            Properties.Resources.ResInformacao,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
    }

    private void MenuItem_GerarParamsProc(object sender, EventArgs e)
    {
      if (!string.IsNullOrEmpty(SiglaModulo))
      {
        StringBuilder strBuilder = new StringBuilder();
        StringBuilder strResources = new StringBuilder();
        foreach (ColumnModel row in LinhasSelecionadas.OrderBy(x => x.Id))
        {
          strBuilder.AppendLine(GerarParamsProc(row));
          strResources.AppendLine(GerarResources(row));
        }

        if (strBuilder.Length > 0)
        {
          Clipboard.SetDataObject(strBuilder.AppendLine(Environment.NewLine).Append(strResources.ToString()).ToString(), true);
          MessageBox.Show(Properties.Resources.ResCopiadoAreaTransferencia,
            Properties.Resources.ResInformacao,
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
    }

    /// <summary>
    /// Converer a primeira letra da string para maiúscula
    /// </summary>
    /// <param name="self">string a ser convertida</param>
    /// <returns>string com a primeira letra em maiúscula</returns>
    static string PriMaisucula(string self)
    {
      if (string.IsNullOrWhiteSpace(self))
        return self;

      if (self.Length == 1)
        return self.ToUpperInvariant();

      string depois = char.ToUpperInvariant(self[0]) + self.Substring(1).ToLower();
      return depois;
    }

    private void MenuItem_GerarMapeamentoEntidade(object sender, EventArgs e)
    {
      var str = new StringBuilder();
      if (LinhasSelecionadas.Any())
      {
        // Nome da tabela
        str.AppendFormat(Constantes.TnfMapConsts.BuilderToTable, LinhasSelecionadas[0].Opcional2).AppendLine(Environment.NewLine);

        // Chave
        var chaves = LinhasSelecionadas.Where(x => x.IsKey).OrderBy(x => x.Id);
        if (chaves.Any())
        {
          if (chaves.Count() == 1)
            str.AppendFormat(Constantes.TnfMapConsts.BuilderHasKey, $"k => k.{PriMaisucula(chaves.ElementAt(0).Nome)}");
          else
          {
            var nomes = chaves.Select(x => "k." + PriMaisucula(x.Nome));
            str.AppendFormat(Constantes.TnfMapConsts.BuilderHasKey, $"k => new {{ {string.Join(", ", nomes)} }}");
          }
          str.AppendLine(Environment.NewLine);
        }

        foreach (ColumnModel esteRow in LinhasSelecionadas.OrderBy(x => x.Id))
        {
          Campo c = CampoInfo(esteRow);
          string auxMaxLength = c.TipoFisico.ToUpper().Contains("CHAR") ?
            Environment.NewLine + "  " + $".HasMaxLength({c.Tamanho})" : "";
          str.AppendFormat(Constantes.TnfMapConsts.BuilderhasProperty,
            PriMaisucula(c.Nome),
            c.Nome,
            auxMaxLength,
            c.IsNullable ? "false" : "");
          str.AppendLine(Environment.NewLine);
        }

        Clipboard.SetDataObject(str.ToString(), true);
        MessageBox.Show(Properties.Resources.ResCopiadoAreaTransferencia,
          Properties.Resources.ResInformacao,
          MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    /// <summary>
    /// Gerar DataProps conforme a Row
    /// </summary>
    /// <param name="row">Linha da tabela de dicionário</param>
    /// <returns>string formatada contendo os valore de DataProps</returns>
    private string GerarDataProps(ColumnModel row)
    {
      string dataProps = string.Empty;
      if (row != null)
      {
        Campo c = CampoInfo(row);
        string nomeTabela = Convert.ToString(row.Opcional2).ToLower();
        if (string.IsNullOrEmpty(nomeTabela))
          nomeTabela = Constantes.NomeDataTable;

        dataProps = string.Format(Constantes.DataProps,
           nomeTabela,            // dataTable testar row.dataMember
           c.Nome,                // Nome do campo
           SiglaModulo,           // Para compor o resource do Caption
           c.Nome,                // Para compor o resource do caption
           c.TipoFisico.ToUpper().Contains("CHAR") ? c.Tamanho : Constantes.TamanhoPadraoCampo,
           c.IsNullable ? Constantes.True : Constantes.False); // AllowDBNull
      }
      return dataProps;
    }

    /// <summary>
    /// Gerar ParamsProc conforme a Row
    /// </summary>
    /// <param name="row">Row do dicionário de dados</param>
    /// <returns>string formatada contendo os valores de ParamsProc</returns>
    private string GerarParamsProc(ColumnModel row)
    {
      string paramsProc = string.Empty;

      if (row != null)
      {
        Campo c = CampoInfo(row);
        string nomeAux = c.Nome;
        if (!string.IsNullOrEmpty(c.Nome))
          nomeAux = string.Concat(c.Nome[0].ToString().ToUpper(), c.Nome.ToLower().Substring(1));

        //paramsProc = string.Format(Constantes.ParamsProc,
        //  SiglaModulo,             // Compor o Caption
        //  c.Nome,                  // Compor o Caption 
        //  AchaTipoCCharp(c),       // Tipo de dado (se for nullable, acrescenta "? "
        //  nomeAux,                 // Nome do parâmetro
        //  Constantes.AbreChave,    // {
        //  c.Nome.ToLower(),        // _nomeDoCampo private
        //  Constantes.FechaChave    // }
        //  );

        paramsProc = string.Format(Constantes.ParamsProc,
          SiglaModulo,             // Compor o Caption
          c.Nome,                  // Compor o Caption 
          AchaTipoCCharp(c),       // Tipo de dado (se for nullable, acrescenta "? "
          nomeAux,                 // Nome do parâmetro
          Constantes.AbreChave,    // {
          Constantes.FechaChave    // }
          );
      }

      return paramsProc;
    }

    /// <summary>
    /// Gerar os Resources conforme a Row
    /// </summary>
    /// <param name="row">Row do dicionário de dados</param>
    /// <returns>string formatada conforme valores da Row</returns>
    private string GerarResources(ColumnModel row)
    {
      string res = string.Empty;

      if (row != null)
      {
        Campo c = CampoInfo(row);

        res = string.Format(Constantes.Resource,
          SiglaModulo, c.Nome, c.Descricao);
      }
      return res;
    }

    /// <summary>
    /// Conforme o tipo de dados do campo, vamos "transformá-lo" em um tipo da linguagem CSharp
    /// </summary>
    private static string AchaTipoCCharp(Campo campo)
    {
      string p = campo.TipoFisico.ToUpper();

      // Se o campo não for obrigatório, adicionar "?"
      string obrigatorio = campo.IsNullable ? Constantes.IndicadorNullable : string.Empty;

      if (p.Contains("NUMBER"))
      {
        if (campo.Precisao == 5)
          return "short" + obrigatorio;
        else if (campo.Precisao == 10)
          return "int" + obrigatorio;
        else
          return "decimal" + obrigatorio;
      }

      if (p.Contains("CHAR") || p.Contains("TEXT"))
        return "string";
      else
      {
        if (p.Contains("DATETIME") || p.Contains("DATE"))
          return "DateTime" + obrigatorio;
        else if (p.Contains("SMALLINT"))
          return "short" + obrigatorio;
        else if (p.Contains("INT"))
          return "int" + obrigatorio;
        else
          return "decimal" + obrigatorio;
      }
    }

    #endregion

    #region Internal

    /// <summary>
    /// (Gets or sets) Linhas selecionadas no dicionário de dados
    /// </summary>
    internal List<ColumnModel> LinhasSelecionadas { get; set; }

    #endregion

    #region Métodos
    /// <summary>
    /// Ações que vamos disponibilizar para a aplicação que carregou nossa dll
    /// </summary>
    public List<IWSPContextMenuActions> CriarAcoes()
    {
      List<IWSPContextMenuActions> listaAcoes = new List<IWSPContextMenuActions>();
      MinhasAcoes estaAcao;

      // Gerar DataProps
      estaAcao = new MinhasAcoes(
        Properties.Resources.ResGerarDataPropsCampos,
        MenuItem_GerarDataProps)
      {
        MenuImage = Imagens.ListaImagens.Images[0]
      };
      listaAcoes.Add(estaAcao);

      // Gerar ParamsProc
      estaAcao = new MinhasAcoes(Properties.Resources.ResGerarParamsProc,
        MenuItem_GerarParamsProc)
      {
        MenuImage = Imagens.ListaImagens.Images[1]
      };
      listaAcoes.Add(estaAcao);

      estaAcao = new MinhasAcoes(Properties.Resources.StrTnfCreateEntityMap,
        MenuItem_GerarMapeamentoEntidade)
      {
        MenuImage = Imagens.ListaImagens.Images[2]
      };
      listaAcoes.Add(estaAcao);

      return listaAcoes;
    }
    #endregion

  }

  /// <summary>
  /// Ações que serão disponibilizadas a uma aplicação
  /// </summary>
  public class MinhasAcoes : IWSPContextMenuActions
  {
    /// <summary>
    /// Contrói um objeto para disponibilizar ações a uma aplicação
    /// </summary>
    /// <param name="caption">O título da ação</param>
    /// <param name="evento">O evento a ser disparado</param>
    public MinhasAcoes(string caption, EventHandler evento)
    {
      Caption = caption;
      Event = evento;
    }

    #region IWSPContextMenuActions Members

    /// <summary>
    /// O Texto da ação
    /// </summary>
    public string Caption { get; }

    /// <summary>
    /// O evento a ser disparado ao clicar na ação
    /// </summary>
    public EventHandler Event { get; }

    /// <summary>
    /// A imagem que irá aparecer no menu de contexto
    /// </summary>
    public System.Drawing.Image MenuImage { get; set; }
    /// <summary>
    /// Objeto qualquer
    /// </summary>
    public object Tag { get; set; }

    #endregion
  }


}
