using System;

namespace TPC_TiendaRopa
{
    public partial class SiteMaster :
        System.Web.UI.MasterPage
    {

        private bool esPaginaPublica()
        {
            string pagina =
                Request.AppRelativeCurrentExecutionFilePath;

            return pagina.Contains("Login") ||
                   pagina.Contains("Registro") ||
                   pagina.Contains("RecuperarClave");
        }

        protected void Page_Init(
            object sender,
            EventArgs e)
        {
            if (!esPaginaPublica() &&
                Session["Usuario"] == null)
            {
                Response.Redirect(
                    "~/Login.aspx",
                    true);
            }
        }


        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (esPaginaPublica())
            {
                menuPrincipal.Visible = false;
                panelUsuario.Visible = false;
                return;
            }

            menuPrincipal.Visible = true;

            bool esAdmin = Seguridad.EsAdmin();

            liMarcas.Visible = esAdmin;
            liProveedores.Visible = esAdmin;
            liTipos.Visible = esAdmin;
            liUsuarios.Visible = esAdmin;
            liCompras.Visible = esAdmin;
            liReportes.Visible = esAdmin;

            if (Session["Usuario"] != null)
            {
                panelUsuario.Visible = true;

                lblUsuario.Text =
                    "Bienvenido: " +
                    Session["Usuario"].ToString();
            }
            else
            {
                panelUsuario.Visible = false;
            }
        }



        protected void btnCerrarSesion_Click(
            object sender,
            EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(
                "Login.aspx",
                false);
        }
    }
}