using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listar();

            string texto = txtBuscar.Text.Trim();

            lblResultado.Visible = false;

            if (texto != "")
            {
                lista = lista.FindAll(x => contiene(x.Nombre, texto));

                lblResultado.Visible = true;

                if (lista.Count == 0)
                {
                    lblResultado.CssClass = "d-block mb-3 text-danger";
                    lblResultado.Text = "No se encontraron resultados para \"" +
                        Server.HtmlEncode(texto) + "\".";
                }
                else
                {
                    lblResultado.CssClass = "d-block mb-3 text-success";
                    lblResultado.Text = (lista.Count == 1)
                        ? "Se encontró 1 resultado."
                        : "Se encontraron " + lista.Count + " resultados.";
                }
            }

            gvMarcas.DataSource = lista;
            gvMarcas.DataBind();
        }

        private bool contiene(string campo, string texto)
        {
            return campo != null &&
                   campo.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            cargarGrilla();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioMarca.aspx",
                false);
        }

        protected void gvMarcas_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                MarcaNegocio negocio =
                    new MarcaNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}