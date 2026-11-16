using System;

namespace TPC_TiendaRopa {
    public partial class SiteMaster :
        System.Web.UI.MasterPage {
        protected void Page_Init(
        object sender,
        EventArgs e)
            {
              string paginaActual =
                 Request.AppRelativeCurrentExecutionFilePath;

            if (!paginaActual.Contains("Login") &&
                Session["Usuario"] == null)
            {
                Response.Redirect(
                    "~/Login.aspx",
                    true);
            }
        }
        protected void Page_Load(
            object sender,
            EventArgs e) {
            string paginaActual =
                Request.AppRelativeCurrentExecutionFilePath;

            if (paginaActual.Contains("Login")) {
                menuPrincipal.Visible = false;
                panelUsuario.Visible = false;
                return;
            }

            menuPrincipal.Visible = true;

            if (Session["Usuario"] != null) {
                panelUsuario.Visible = true;

                lblUsuario.Text =
                    "Bienvenido: " +
                    Session["Usuario"].ToString();
            } else {
                panelUsuario.Visible = false;
            }
        }

        protected void btnCerrarSesion_Click(
            object sender,
            EventArgs e) {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(
                "Login.aspx",
                false);
        }
    }
}