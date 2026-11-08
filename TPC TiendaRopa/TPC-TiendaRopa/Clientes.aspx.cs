using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Clientes : System.Web.UI.Page
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
            ClienteNegocio negocio = new ClienteNegocio();

            gvClientes.DataSource =
                negocio.listar();

            gvClientes.DataBind();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioCliente.aspx",
                false);
        }

        protected void gvClientes_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                ClienteNegocio negocio =
                    new ClienteNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}