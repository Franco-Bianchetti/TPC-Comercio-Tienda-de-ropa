using System;
using System.Text.RegularExpressions;
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

            string razonSocial = txtRazonSocial.Text.Trim();
            string cuit = txtCuit.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();

            int id = obtenerId();

            if (razonSocial.Length < 2 || razonSocial.Length > 150)
            {
                mostrarError("La razón social debe tener entre 2 y 150 caracteres.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cuit))
            {
                mostrarError("Debe ingresar un CUIT.");
                return;
            }

            if (cuit.Length < 10 || cuit.Length > 20)
            {
                mostrarError("El CUIT no tiene una longitud válida.");
                return;
            }

            if (!string.IsNullOrEmpty(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mostrarError("El formato del correo electrónico no es válido.");
                return;
            }

            ProveedorNegocio negocio = new ProveedorNegocio();

            if (negocio.existeCuit(cuit, id))
            {
                mostrarError("Ya existe un proveedor registrado con ese CUIT.");
                return;
            }

            Proveedor proveedor = new Proveedor
            {
                Id = id,
                RazonSocial = razonSocial,
                Cuit = cuit,
                Telefono = telefono,
                Email = email
            };

            if (id == 0)
                negocio.agregar(proveedor);
            else
                negocio.modificar(proveedor);

            Response.Redirect("Proveedores.aspx", false);
        }

        private void mostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}