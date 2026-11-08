using System;
using System.Text.RegularExpressions;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioTipo : System.Web.UI.Page
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
                    TipoNegocio negocio =
                        new TipoNegocio();

                    Tipo tipo =
                        negocio.buscar(id);

                    if (tipo != null)
                    {
                        lblTitulo.Text =
                            "Modificar tipo";

                        txtNombre.Text =
                            tipo.Nombre;
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
                    "Debe ingresar un nombre para el tipo.");
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

            if (!esTipoValido(nombre))
            {
                mostrarError(
                    "El nombre contiene caracteres no permitidos.");
                return;
            }

            TipoNegocio negocio =
                new TipoNegocio();

            if (negocio.existe(nombre, id))
            {
                mostrarError(
                    "Ya existe un tipo con ese nombre.");
                return;
            }

            if (negocio.existeInactivo(nombre))
            {
                mostrarError(
                    "El tipo ya existe y se encuentra inactivo.");
                return;
            }

            Tipo tipo =
                new Tipo();

            tipo.Id = id;
            tipo.Nombre = nombre;

            if (id == 0)
                negocio.agregar(tipo);
            else
                negocio.modificar(tipo);

            Response.Redirect(
                "Tipos.aspx",
                false);
        }

        private bool esTipoValido(
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