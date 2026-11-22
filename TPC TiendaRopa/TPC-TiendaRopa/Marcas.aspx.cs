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
            List<Marca> lista = negocio.listar(Seguridad.EsAdmin());

            string texto = txtBuscar.Text.Trim();

            if (texto != "")
            {
                lista = lista.FindAll(x => Filtros.Contiene(x.Nombre, texto));
            }

            gvMarcas.DataSource = lista;
            gvMarcas.DataBind();

            Filtros.MostrarResultado(lblResultado, lista.Count, texto != "", texto);
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
        protected void gvMarcas_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Marca marca = (Marca)e.Row.DataItem;

            if (!marca.Activo)
            {
                e.Row.CssClass = "table-secondary text-muted";
                e.Row.FindControl("pnlActiva").Visible = false;
                e.Row.FindControl("pnlInactiva").Visible = true;
            }
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
            if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id = int.Parse(e.CommandArgument.ToString());

                MarcaNegocio negocio = new MarcaNegocio();
                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}