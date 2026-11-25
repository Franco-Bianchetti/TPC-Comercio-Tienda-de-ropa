using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();

            gvProductos.DataSource = negocio.listar();
            gvProductos.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProducto.aspx", false);
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                ProductoNegocio negocio = new ProductoNegocio();
                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}