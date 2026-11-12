using System;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class FormularioUsuario : System.Web.UI.Page {
        private int obtenerId() {
            int id = 0;

            if (Request.QueryString["id"] != null)
                int.TryParse(
                    Request.QueryString["id"],
                    out id);

            return id;
        }

        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (!IsPostBack) {
                int id = obtenerId();

                if (id != 0) {
                    UsuarioNegocio negocio =
                        new UsuarioNegocio();

                    Usuario usuario =
                        negocio.buscar(id);

                    if (usuario != null) {
                        lblTitulo.Text =
                            "Modificar usuario";

                        txtNombre.Text =
                            usuario.Nombre;

                        txtApellido.Text =
                            usuario.Apellido;

                        txtDni.Text =
                            usuario.Dni;

                        txtUsuario.Text =
                            usuario.NombreUsuario;

                        txtClave.Attributes["value"] =
                            usuario.Clave;

                        ddlPerfil.SelectedValue =
                            usuario.Perfil.ToString();
                    }
                }
            }
        }

        protected void btnGuardar_Click(
            object sender,
            EventArgs e) {
            lblError.Visible = false;

            int id =
                obtenerId();

            string nombre =
                txtNombre.Text.Trim().ToUpper();

            string apellido =
                txtApellido.Text.Trim().ToUpper();

            string dni =
                txtDni.Text.Trim();

            string usuario =
                txtUsuario.Text.Trim().ToLower();

            string clave =
                txtClave.Text.Trim();

            if (nombre.Length < 2 ||
                nombre.Length > 50) {
                mostrarError(
                    "El nombre debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (apellido.Length < 2 ||
                apellido.Length > 50) {
                mostrarError(
                    "El apellido debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (!long.TryParse(dni, out _)) {
                mostrarError(
                    "El DNI debe contener sólo números.");
                return;
            }

            if (dni.Length < 8 ||
                dni.Length > 10) {
                mostrarError(
                    "El DNI debe tener entre 8 y 10 dígitos.");
                return;
            }

            if (todosLosDigitosIguales(dni)) {
                mostrarError(
                    "El DNI ingresado no es válido.");
                return;
            }

            if (usuario.Length < 3 ||
                usuario.Length > 30) {
                mostrarError(
                    "El usuario debe tener entre 3 y 30 caracteres.");
                return;
            }

            if (!usuarioValido(usuario)) {
                mostrarError(
                    "El usuario sólo puede contener letras y números.");
                return;
            }

            if (clave.Length < 8) {
                mostrarError(
                    "La contraseña debe tener al menos 8 caracteres.");
                return;
            }

            if (!contieneNumero(clave)) {
                mostrarError(
                    "La contraseña debe contener al menos un número.");
                return;
            }

            UsuarioNegocio negocio =
                new UsuarioNegocio();

            if (negocio.existeUsuario(usuario, id)) {
                mostrarError(
                    "Ya existe un usuario con ese nombre.");
                return;
            }

            if (negocio.existeDni(dni, id)) {
                mostrarError(
                    "Ya existe un usuario con ese DNI.");
                return;
            }

            Usuario nuevo =
                new Usuario();

            nuevo.Id = id;
            nuevo.Nombre = nombre;
            nuevo.Apellido = apellido;
            nuevo.Dni = dni;
            nuevo.NombreUsuario = usuario;
            nuevo.Clave = clave;

            nuevo.Perfil =
                ddlPerfil.SelectedValue ==
                "Administrador"
                ? TipoPerfil.Administrador
                : TipoPerfil.Vendedor;

            if (id == 0)
                negocio.agregar(nuevo);
            else
                negocio.modificar(nuevo);

            Response.Redirect(
                "Usuarios.aspx",
                false);
        }

        private bool contieneNumero(
            string texto) {
            foreach (char c in texto) {
                if (char.IsDigit(c))
                    return true;
            }

            return false;
        }

        private bool usuarioValido(
            string usuario) {
            foreach (char c in usuario) {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }

        private bool todosLosDigitosIguales(
            string valor) {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            char primero = valor[0];

            foreach (char c in valor) {
                if (c != primero)
                    return false;
            }

            return true;
        }

        private void mostrarError(
            string mensaje) {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}