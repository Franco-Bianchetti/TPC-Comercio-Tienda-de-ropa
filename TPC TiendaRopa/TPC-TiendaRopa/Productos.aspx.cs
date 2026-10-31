using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();

                gvProductos.DataSource =
                    negocio.listar();

                gvProductos.DataBind();
            }
        }
    }
}