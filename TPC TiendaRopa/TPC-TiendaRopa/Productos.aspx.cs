using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Productos : System.Web.UI.Page
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
            ProductoNegocio negocio =
                new ProductoNegocio();

            List<Producto> lista =
                negocio.listar(
                    Seguridad.EsAdmin());

            string texto =
                txtBuscar.Text.Trim();

            lblResultado.Visible =
                false;

            if (texto != "")
            {
                lista = lista.FindAll(x =>
                    contiene(x.Codigo, texto) ||
                    contiene(x.Nombre, texto) ||
                    contiene(x.Color, texto) ||
                    contiene(x.Talle, texto) ||
                    (x.Marca != null &&
                     contiene(x.Marca.Nombre, texto)) ||
                    (x.Tipo != null &&
                     contiene(x.Tipo.Nombre, texto)));

                lblResultado.Visible =
                    true;

                if (lista.Count == 0)
                {
                    lblResultado.CssClass =
                        "d-block mb-3 text-danger";

                    lblResultado.Text =
                        "No se encontraron resultados para \"" +
                        Server.HtmlEncode(texto) +
                        "\".";
                }
                else
                {
                    lblResultado.CssClass =
                        "d-block mb-3 text-success";

                    lblResultado.Text =
                        lista.Count == 1
                        ? "Se encontró 1 resultado."
                        : "Se encontraron " +
                          lista.Count +
                          " resultados.";
                }
            }

            gvProductos.DataSource =
                lista;

            gvProductos.DataBind();
        }

        private bool contiene(
            string campo,
            string texto)
        {
            return campo != null &&
                campo.IndexOf(
                    texto,
                    StringComparison.OrdinalIgnoreCase) >= 0;
        }

        protected void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            cargarGrilla();
        }

        protected void btnLimpiar_Click(
            object sender,
            EventArgs e)
        {
            txtBuscar.Text = "";
            cargarGrilla();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioProducto.aspx",
                false);
        }

        protected void gvProductos_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType !=
                DataControlRowType.DataRow)
                return;

            Producto producto =
                (Producto)e.Row.DataItem;

            if (!producto.Activo)
            {
                e.Row.CssClass =
                    "table-secondary text-muted";

                e.Row.FindControl(
                    "pnlActiva").Visible = false;

                e.Row.FindControl(
                    "pnlInactiva").Visible = true;

                return;
            }

            if (producto.StockActual <=
                producto.StockMinimo)
            {
                e.Row.CssClass =
                    "table-danger";
            }
        }

        protected void gvProductos_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                ProductoNegocio negocio =
                    new ProductoNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
            else if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                ProductoNegocio negocio =
                    new ProductoNegocio();

                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}