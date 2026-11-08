using System;
using System.Text.RegularExpressions;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioMarca : System.Web.UI.Page
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
                    MarcaNegocio negocio =
                        new MarcaNegocio();

                    Marca marca =
                        negocio.buscar(id);

                    if (marca != null)
                    {
                        lblTitulo.Text =
                            "Modificar marca";

                        txtNombre.Text =
                            marca.Nombre;
                    }
                }
            }
        }

        protected void btnGuardar_Click(
            object sender,
            EventArgs e)
        {
            string nombre =
                txtNombre.Text.Trim().ToUpper();

            int id =
                obtenerId();

            lblError.Visible = false;

            if (nombre == "")
            {
                mostrarError(
                    "Debe ingresar un nombre para la marca.");
                return;
            }

            if (nombre.Length < 2)
            {
                mostrarError(
                    "El nombre debe tener al menos 2 caracteres.");
                return;
            }

            if (nombre.Length > 50)
            {
                mostrarError(
                    "El nombre no puede superar los 50 caracteres.");
                return;
            }

            if (!esMarcaValida(nombre))
            {
                mostrarError(
                    "El nombre contiene caracteres no permitidos.");
                return;
            }

            MarcaNegocio negocio =
                new MarcaNegocio();

            if (negocio.existeActiva(nombre, id))
            {
                mostrarError(
                    "Ya existe una marca con ese nombre.");
                return;
            }

            if (negocio.existeInactiva(nombre))
            {
                mostrarError(
                    "La marca ya existe y se encuentra inactiva.");
                return;
            }

            Marca marca =
                new Marca();

            marca.Id = id;
            marca.Nombre = nombre;

            if (id == 0)
                negocio.agregar(marca);
            else
                negocio.modificar(marca);

            Response.Redirect(
                "Marcas.aspx",
                false);
        }

        private bool esMarcaValida(
            string nombre)
        {
            return Regex.IsMatch(
                nombre,
                @"^[A-ZÁÉÍÓÚÑ'&\-\.\s]{2,50}$");
        }

        private void mostrarError(
            string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}