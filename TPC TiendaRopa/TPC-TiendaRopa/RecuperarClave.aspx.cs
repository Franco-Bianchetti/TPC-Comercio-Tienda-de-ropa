using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class RecuperarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string usuario = txtUsuario.Text.Trim().ToLower();
            string dni = txtDni.Text.Trim();
            string clave = txtClave.Text.Trim();
            string repetir = txtRepetirClave.Text.Trim();

            if (usuario == "")
            {
                mostrarError("Ingresá tu usuario.");
                return;
            }

            if (!long.TryParse(dni, out _) || dni.Length < 8 || dni.Length > 10)
            {
                mostrarError("El DNI debe tener entre 8 y 10 números.");
                return;
            }

            if (clave.Length < 8)
            {
                mostrarError("La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            if (!Validaciones.ContieneNumero(clave))
            {
                mostrarError("La contraseña debe contener al menos un número.");
                return;
            }

            if (clave != repetir)
            {
                mostrarError("Las contraseñas no coinciden.");
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            int id = negocio.buscarIdPorUsuarioYDni(usuario, dni);

            if (id == 0)
            {
                mostrarError("Los datos ingresados no coinciden.");
                return;
            }

            negocio.cambiarClave(id, clave);

            Response.Redirect("Login.aspx?clave=ok", false);
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}