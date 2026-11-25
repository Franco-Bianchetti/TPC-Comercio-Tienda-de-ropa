using System;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            string nombre = txtNombre.Text.Trim().ToUpper();
            string apellido = txtApellido.Text.Trim().ToUpper();
            string dni = txtDni.Text.Trim();
            string usuario = txtUsuario.Text.Trim().ToLower();
            string clave = txtClave.Text.Trim();
            string repetir = txtRepetirClave.Text.Trim();

            if (nombre.Length < 2 || nombre.Length > 50)
            {
                mostrarError("El nombre debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (!Validaciones.SoloLetras(nombre))
            {
                mostrarError("El nombre sólo puede contener letras.");
                return;
            }

            if (apellido.Length < 2 || apellido.Length > 50)
            {
                mostrarError("El apellido debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (!Validaciones.SoloLetras(apellido))
            {
                mostrarError("El apellido sólo puede contener letras.");
                return;
            }

            if (!long.TryParse(dni, out _))
            {
                mostrarError("El DNI debe contener sólo números.");
                return;
            }

            if (dni.Length < 8 || dni.Length > 10)
            {
                mostrarError("El DNI debe tener entre 8 y 10 dígitos.");
                return;
            }

            if (Validaciones.TodosIguales(dni))
            {
                mostrarError("El DNI ingresado no es válido.");
                return;
            }

            if (usuario.Length < 3 || usuario.Length > 30)
            {
                mostrarError("El usuario debe tener entre 3 y 30 caracteres.");
                return;
            }

            if (!Validaciones.UsuarioValido(usuario))
            {
                mostrarError("El usuario sólo puede contener letras y números, y debe incluir al menos una letra.");
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

            if (negocio.existeUsuario(usuario, 0))
            {
                mostrarError("Ya existe un usuario con ese nombre.");
                return;
            }

            if (negocio.existeDni(dni, 0))
            {
                mostrarError("Ya existe un usuario con ese DNI.");
                return;
            }

            Usuario nuevo = new Usuario();

            nuevo.Nombre = nombre;
            nuevo.Apellido = apellido;
            nuevo.Dni = dni;
            nuevo.NombreUsuario = usuario;
            nuevo.Clave = clave;

            nuevo.Perfil = ddlPerfil.SelectedValue == "Administrador"
                ? TipoPerfil.Administrador
                : TipoPerfil.Vendedor;

            negocio.agregar(nuevo, false, false);

            Response.Redirect("Login.aspx?registro=pendiente", false);
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}