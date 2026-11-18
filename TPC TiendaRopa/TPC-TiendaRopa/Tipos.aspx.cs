using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Tipos : System.Web.UI.Page
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
            TipoNegocio negocio = new TipoNegocio();
            List<Tipo> lista = negocio.listar();

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

            gvTipos.DataSource = lista;
            gvTipos.DataBind();
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
                "FormularioTipo.aspx",
                false);
        }

        protected void gvTipos_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                TipoNegocio negocio =
                    new TipoNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
        }
    }
}