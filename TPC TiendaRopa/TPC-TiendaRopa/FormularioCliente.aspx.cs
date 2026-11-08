using System;
using System.Net.Mail;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioCliente : System.Web.UI.Page
    {
        private int obtenerId()
        {
            int id = 0;

            if (Request.QueryString["id"] != null)
                int.TryParse(
                    Request.QueryString["id"],
                    out id);

            return id;
        }

        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = obtenerId();

                if (id != 0)
                {
                    ClienteNegocio negocio =
                        new ClienteNegocio();

                    Cliente cliente =
                        negocio.buscar(id);

                    if (cliente != null)
                    {
                        lblTitulo.Text =
                            "Modificar Cliente";

                        txtNombre.Text =
                            cliente.Nombre;

                        txtApellido.Text =
                            cliente.Apellido;

                        txtDni.Text =
                            cliente.Dni;

                        txtTelefono.Text =
                            cliente.Telefono;

                        txtEmail.Text =
                            cliente.Email;
                    }
                }
            }
        }

        protected void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            ClienteNegocio negocio =
                new ClienteNegocio();

            int id =
                obtenerId();

            string nombre =
                txtNombre.Text.Trim().ToUpper();

            string apellido =
                txtApellido.Text.Trim().ToUpper();

            string dni =
                txtDni.Text.Trim();

            string telefono =
                txtTelefono.Text.Trim();

            string email =
                txtEmail.Text.Trim().ToLower();

            lblError.Visible = false;

            if (nombre.Length < 2)
            {
                mostrarError(
                    "El nombre debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (nombre.Length > 50)
            {
                mostrarError(
                    "El nombre debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (apellido.Length < 2)
            {
                mostrarError(
                    "El apellido debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (apellido.Length > 50)
            {
                mostrarError(
                    "El apellido debe tener entre 2 y 50 caracteres.");
                return;
            }

            if (dni == "")
            {
                mostrarError(
                    "Debe ingresar el DNI.");
                return;
            }

            if (!long.TryParse(dni, out _))
            {
                mostrarError(
                    "El DNI debe contener sólo números.");
                return;
            }

            if (dni.Length < 8 || dni.Length > 10)
            {
                mostrarError(
                    "El DNI debe tener entre 8 y 10 dígitos.");
                return;
            }

            if (todosLosDigitosIguales(dni))
            {
                mostrarError(
                    "El DNI ingresado no es válido.");
                return;
            }

            if (telefono == "")
            {
                mostrarError(
                    "Debe ingresar el teléfono.");
                return;
            }

            if (!long.TryParse(telefono, out _))
            {
                mostrarError(
                    "El teléfono debe contener sólo números.");
                return;
            }

            if (telefono.Length < 8 || telefono.Length > 10)
            {
                mostrarError(
                    "El teléfono debe tener entre 8 y 10 dígitos.");
                return;
            }

            if (todosLosDigitosIguales(telefono))
            {
                mostrarError(
                    "El teléfono ingresado no es válido.");
                return;
            }

            if (email == "")
            {
                mostrarError(
                    "Debe ingresar el email.");
                return;
            }

            if (!esEmailValido(email))
            {
                mostrarError(
                    "Debe ingresar un email válido.");
                return;
            }

            if (negocio.existeDni(dni, id))
            {
                mostrarError(
                    "Ya existe un cliente con ese DNI.");
                return;
            }

            if (negocio.existeEmail(email, id))
            {
                mostrarError(
                    "Ya existe un cliente con ese email.");
                return;
            }

            Cliente cliente =
                new Cliente();

            cliente.Id = id;
            cliente.Nombre = nombre;
            cliente.Apellido = apellido;
            cliente.Dni = dni;
            cliente.Telefono = telefono;
            cliente.Email = email;

            if (id == 0)
                negocio.agregar(cliente);
            else
                negocio.modificar(cliente);

            Response.Redirect(
                "Clientes.aspx",
                false);
        }

        private bool esEmailValido(
            string email)
        {
            try
            {
                MailAddress mail =
                    new MailAddress(email);

                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool todosLosDigitosIguales(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            char primerCaracter =
                valor[0];

            foreach (char caracter in valor)
            {
                if (caracter != primerCaracter)
                    return false;
            }

            return true;
        }


        private void mostrarError(
            string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}