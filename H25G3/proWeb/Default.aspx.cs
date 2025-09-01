using Library.CAD;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductosNuevosYProximos();
            }
        }

        private void CargarProductosNuevosYProximos()
        {
            CADVenta cadVenta = new CADVenta();
            DataTable productos = cadVenta.ObtenerProductosNuevosYProximos();

            rptProductosNuevos.DataSource = productos;
            rptProductosNuevos.DataBind();
        }
    }
}