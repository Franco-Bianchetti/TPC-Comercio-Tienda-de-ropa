using System;

namespace TPC_TiendaRopa {
    public partial class SiteMaster :
        System.Web.UI.MasterPage {
        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (Session["Usuario"] != null) {
                lblUsuario.Text =
                    "Bienvenido: " +
                    Session["Usuario"].ToString();

                lblUsuario.Visible = true;
                btnCerrarSesion.Visible = true;
            } else {
                lblUsuario.Visible = false;
                btnCerrarSesion.Visible = false;
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