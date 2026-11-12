using System;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class Usuarios : System.Web.UI.Page {
        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (!IsPostBack) {
                cargarGrilla();
            }
        }

        private void cargarGrilla() {
            UsuarioNegocio negocio =
                new UsuarioNegocio();

            gvUsuarios.DataSource =
                negocio.listar();

            gvUsuarios.DataBind();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e) {
            Response.Redirect(
                "FormularioUsuario.aspx",
                false);
        }

        protected void gvUsuarios_RowCommand(
            object sender,
            GridViewCommandEventArgs e) {
            if (e.CommandName == "Eliminar") {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                UsuarioNegocio negocio =
                    new UsuarioNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}