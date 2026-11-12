using System;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (!IsPostBack) {
                Session.Clear();
            }
        }

        protected void btnIngresar_Click(
            object sender,
            EventArgs e) {
            string usuario =
                txtUsuario.Text.Trim();

            string clave =
                txtClave.Text.Trim();

            if (usuario == "" ||
                clave == "") {
                lblError.Text =
                    "Debe completar usuario y contraseña.";

                lblError.Visible = true;
                return;
            }

            UsuarioNegocio negocio =
                new UsuarioNegocio();

            Usuario usuarioLogueado =
                negocio.login(
                    usuario,
                    clave);

            if (usuarioLogueado == null) {
                lblError.Text =
                    "Usuario o contraseña incorrectos.";

                lblError.Visible = true;
                return;
            }

            Session["Usuario"] =
                usuarioLogueado.NombreUsuario;

            Session["Perfil"] =
                usuarioLogueado.Perfil;

            Response.Redirect(
                "Clientes.aspx",
                false);
        }
    }
}