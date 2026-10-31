using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProveedorNegocio negocio =
                    new ProveedorNegocio();

                gvProveedores.DataSource =
                    negocio.listar();

                gvProveedores.DataBind();
            }
        }
    }
}