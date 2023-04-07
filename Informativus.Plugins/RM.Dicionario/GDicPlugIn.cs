using System;
using System.Collections.Generic;
using System.Text;
using WSP.Lib.DataAccess;
using System.Data;
using WSP.Lib;
using System.Collections;
using RM.Dicionario.Objetos;
using WSP.Lib.DataAccess.DataSets;
using Informativus.Comuns.PlugIns;
using System.Windows.Forms;

namespace RM.Dicionario
{
  public class GDicPlugIn : IWSPInformativusDicionarioBase
  {
    WSPDataAccess _db;
    Hashtable _tabelas;
    IPlugInHost _host;

    public GDicPlugIn()
      : base ()
    {
      _tabelas = new Hashtable();
    }

    private MetaDataBase CriarMetaDataTabela(string nomeTabela)
    { 
      MetaDataBase obj;
      if (DB.ProviderType == WSPEnums.WSPProviderType.SqlClient)
        obj = new MetaDataSqlServer(_db, nomeTabela);
      else
        obj = new MetaDataOracle(_db, nomeTabela);
      return obj;
    }

    private GDicObj GetTabelaHash(string nomeTabela)
    { 
      if (_tabelas == null)
        _tabelas = new Hashtable();
      GDicObj obj = _tabelas[nomeTabela] as GDicObj;

      if (obj == null)
      {
        obj = new GDicObj(DB, CriarMetaDataTabela(nomeTabela));
        _tabelas.Add(nomeTabela, obj);
      }
      return obj;
    }
    
    private WSPDataAccess DB
    {
      get
      {
        if (_db == null)
        {
          _db = new WSPDataAccess(Host.DbInfo.ProviderType,
            Host.DbInfo.ServerName,
            Host.DbInfo.DataBaseName,
            Host.DbInfo.IntegratedSecurity,
            Host.DbInfo.UserName,
            Host.DbInfo.Password, 
            Host.DbInfo.TimeOut,
            Constantes.NomePlugIn);
        }
        return _db;
      }
    }

    #region IWSPMetaDataDicionario Members

    public string DescricaoNomeTabela(string nomeTabela)
    {
      GDicObj obj = GetTabelaHash(nomeTabela);
      return obj.DescricaoTabela;
    }

    public CamposTabela.Campos_TabelaDataTable DicionarioTabela(MetaDataBase tabela)
    {
      CamposTabela.Campos_TabelaDataTable dt = GetTabelaHash(tabela.NomeTabela).Dicionario;
      return dt;
    }

    /// <summary>
    /// Excluir uma tabela da lista interna
    /// </summary>
    /// <param name="nomeTabela">O nome da tabela a ser excluída</param>
    public void ExcluirTabela(string nomeTabela)
    { 
      _tabelas.Remove(nomeTabela);
    }

    public IPlugInHost Host
    {
      get
      {
        return _host;
      }
      set
      {
        _host = value;
      }
    }

    #endregion
  }
}
