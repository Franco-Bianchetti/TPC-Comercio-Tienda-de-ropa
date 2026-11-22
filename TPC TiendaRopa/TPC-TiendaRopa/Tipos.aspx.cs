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
            List<Tipo> lista = negocio.listar(Seguridad.EsAdmin());

            string texto = txtBuscar.Text.Trim();

            if (texto != "")
            {
                lista = lista.FindAll(x => Filtros.Contiene(x.Nombre, texto));
            }

            gvTipos.DataSource = lista;
            gvTipos.DataBind();

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
                "FormularioTipo.aspx",
                false);
        }

        protected void gvTipos_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Tipo tipo = (Tipo)e.Row.DataItem;

            if (!tipo.Activo)
            {
                e.Row.CssClass = "table-secondary text-muted";
                e.Row.FindControl("pnlActiva").Visible = false;
                e.Row.FindControl("pnlInactiva").Visible = true;
            }
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

                TipoNegocio negocio = new TipoNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
            if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id = int.Parse(e.CommandArgument.ToString());

                TipoNegocio negocio = new TipoNegocio();
                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}