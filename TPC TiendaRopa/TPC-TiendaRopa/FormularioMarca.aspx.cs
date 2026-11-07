using System;
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
                    MarcaNegocio negocio = new MarcaNegocio();
                    Marca marca = negocio.buscar(id);

                    if (marca != null)
                    {
                        lblTitulo.Text = "Modificar marca";
                        txtNombre.Text = marca.Nombre;
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
                lblError.Text = "Ingresá un nombre para la marca.";
                lblError.Visible = true;
                return;
            }

            MarcaNegocio negocio = new MarcaNegocio();

            if (negocio.existe(nombre, id))
            {
                lblError.Text = "Ya existe una marca con ese nombre.";
                lblError.Visible = true;
                return;
            }

            Marca marca = new Marca();
            marca.Id = id;
            marca.Nombre = nombre;

            if (id == 0)
                negocio.agregar(marca);
            else
                negocio.modificar(marca);

            Response.Redirect("Marcas.aspx", false);
        }
    }
}