using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Tipos : System.Web.UI.Page
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
            TipoNegocio negocio =
                new TipoNegocio();

            gvTipos.DataSource =
                negocio.listar();

            gvTipos.DataBind();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioTipo.aspx",
                false);
        }

        protected void gvTipos_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                TipoNegocio negocio =
                    new TipoNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}