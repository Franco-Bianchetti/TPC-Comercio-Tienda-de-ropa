using System;
using System.Net.Mail;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioProveedor : System.Web.UI.Page
    {
        private int obtenerId()
        {
            int id = 0;

            if (Request.QueryString["id"] != null)
                int.TryParse(Request.QueryString["id"], out id);

            return id;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = obtenerId();

                if (id != 0)
                {
                    ProveedorNegocio negocio = new ProveedorNegocio();
                    Proveedor proveedor = negocio.buscar(id);

                    if (proveedor != null)
                    {
                        lblTitulo.Text = "Modificar proveedor";

                        txtRazonSocial.Text = proveedor.RazonSocial;
                        txtCuit.Text = proveedor.Cuit;
                        txtTelefono.Text = proveedor.Telefono;
                        txtEmail.Text = proveedor.Email;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            int id = obtenerId();

            string razonSocial =
                txtRazonSocial.Text.Trim().ToUpper();

            string cuit =
                txtCuit.Text.Trim();

            string telefono =
                txtTelefono.Text.Trim();

            string email =
                txtEmail.Text.Trim().ToLower();

            if (razonSocial.Length < 2 ||
                razonSocial.Length > 150)
            {
                mostrarError(
                    "La razón social debe tener entre 2 y 150 caracteres.");
                return;
            }

            if (!long.TryParse(cuit, out _))
            {
                mostrarError(
                    "El CUIT debe contener sólo números.");
                return;
            }

            if (cuit.Length != 11)
            {
                mostrarError(
                    "El CUIT debe tener exactamente 11 dígitos.");
                return;
            }

            if (todosLosDigitosIguales(cuit))
            {
                mostrarError(
                    "El CUIT ingresado no es válido.");
                return;
            }

            if (telefono != "")
            {
                if (!long.TryParse(telefono, out _))
                {
                    mostrarError(
                        "El teléfono debe contener sólo números.");
                    return;
                }

                if (telefono.Length < 8 ||
                    telefono.Length > 10)
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
            }

            if (email != "" &&
                !esEmailValido(email))
            {
                mostrarError(
                    "Debe ingresar un email válido.");
                return;
            }

            ProveedorNegocio negocio =
                new ProveedorNegocio();

            if (negocio.existeCuit(cuit, id))
            {
                mostrarError(
                    "Ya existe un proveedor registrado con ese CUIT.");
                return;
            }

            Proveedor proveedor =
                new Proveedor();

            proveedor.Id = id;
            proveedor.RazonSocial = razonSocial;
            proveedor.Cuit = cuit;
            proveedor.Telefono = telefono;
            proveedor.Email = email;

            if (id == 0)
                negocio.agregar(proveedor);
            else
                negocio.modificar(proveedor);

            Response.Redirect(
                "Proveedores.aspx",
                false);
        }

        private bool esEmailValido(string email)
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

        private bool todosLosDigitosIguales(
            string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            char primero = valor[0];

            foreach (char c in valor)
            {
                if (c != primero)
                    return false;
            }

            return true;
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}