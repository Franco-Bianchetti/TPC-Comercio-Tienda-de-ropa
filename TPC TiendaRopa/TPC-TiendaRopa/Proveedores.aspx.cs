using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Proveedores : System.Web.UI.Page
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
                ProveedorNegocio negocio =
                    new ProveedorNegocio();

                gvProveedores.DataSource =
                    negocio.listar();

                gvProveedores.DataBind();
        }
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProveedor.aspx", false);
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.eliminar(id);
                cargarGrilla();
            }
        }
    }
}

