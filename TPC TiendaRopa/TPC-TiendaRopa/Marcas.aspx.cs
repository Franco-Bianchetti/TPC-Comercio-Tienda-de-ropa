using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            MarcaNegocio negocio =
                new MarcaNegocio();

            gvMarcas.DataSource =
                negocio.listar();

            gvMarcas.DataBind();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioMarca.aspx",
                false);
        }

        protected void gvMarcas_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                MarcaNegocio negocio =
                    new MarcaNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}