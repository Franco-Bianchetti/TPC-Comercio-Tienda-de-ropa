using System;
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
                    TipoNegocio negocio = new TipoNegocio();
                    Tipo tipo = negocio.buscar(id);

                    if (tipo != null)
                    {
                        lblTitulo.Text = "Modificar tipo";
                        txtNombre.Text = tipo.Nombre;
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            int id = obtenerId();

            if (nombre == "")
            {
                lblError.Text = "Ingresá un nombre para el tipo.";
                lblError.Visible = true;
                return;
            }

            TipoNegocio negocio = new TipoNegocio();

            if (negocio.existe(nombre, id))
            {
                lblError.Text = "Ya existe un tipo con ese nombre.";
                lblError.Visible = true;
                return;
            }

            Tipo tipo = new Tipo();

            tipo.Id = id;
            tipo.Nombre = nombre;

            if (id == 0)
                negocio.agregar(tipo);
            else
                negocio.modificar(tipo);

            Response.Redirect("Tipos.aspx", false);
        }
    }
}