using System;
using System.Collections.Generic;
using System.Text;
using Informativus.Comuns.PlugIns;
using RM.Dicionario.Objetos;
using WSP.Lib.DataAccess;
using WSP.Lib;
using System.Windows.Forms;

namespace RM.Dicionario
{
  public class GeracaoCodigoPlugIn : IWSPInformativusManipularGridDicionarioBase
  {
    //WSPDataAccess _db;
    GeracaoCodigoObj _geradorCodigoObj;
    IPlugInHost _host;
    DataGridView _grid;

    List<WSPInformativusToolStripMenuItem> _menus;
    public GeracaoCodigoPlugIn()
    {
    }

    private GeracaoCodigoObj GeradorCodigoObj
    {
      get
      {
        if (_geradorCodigoObj == null)
          _geradorCodigoObj = new GeracaoCodigoObj(_grid);
        return _geradorCodigoObj;
      }
    }

    //private WSPDataAccess DB
    //{
    //  get
    //  {
    //    if (_db == null)
    //    {
    //      _db = new WSPDataAccess(Host.DbInfo.ProviderType,
    //        Host.DbInfo.ServerName, Host.DbInfo.DataBaseName,
    //        Host.DbInfo.UserName, Host.DbInfo.Password,
    //        Constantes.NomePlugIn);
    //    }
    //    return _db;
    //  }
    //}

    #region IPlugInHostBase Members

    public List<WSPInformativusToolStripMenuItem> Menus
    {
      get
      {
        if (_menus == null)
        {
          _menus = new List<WSPInformativusToolStripMenuItem>();
          GeradorCodigoObj.CriarMenus(_menus);
        }
        return _menus;
      }
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

    #region IWSPInformativusManipularGridDicionarioBase Members

    public System.Windows.Forms.DataGridView Grid
    {
      get
      {
        return _grid;
      }
      set
      {
        _grid = value;
      }
    }

    #endregion
  }
}
