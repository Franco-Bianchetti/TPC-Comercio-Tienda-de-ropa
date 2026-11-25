using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Productos : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFiltros();
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            ProductoNegocio negocio = new ProductoNegocio();

            int idMarca = 0;
            int idTipo = 0;

            int.TryParse(ddlMarca.SelectedValue, out idMarca);
            int.TryParse(ddlTipo.SelectedValue, out idTipo);

            List<Producto> lista = negocio.listarFiltrado(
                txtBuscar.Text.Trim(),
                idMarca,
                idTipo,
                txtTalle.Text.Trim(),
                txtColor.Text.Trim(),
                chkStockBajo.Checked,
                Seguridad.EsAdmin()
            );

            gvProductos.DataSource = lista;
            gvProductos.DataBind();

            bool hayFiltro =
                txtBuscar.Text.Trim() != "" ||
                idMarca > 0 ||
                idTipo > 0 ||
                txtTalle.Text.Trim() != "" ||
                txtColor.Text.Trim() != "" ||
                chkStockBajo.Checked;

            Filtros.MostrarResultado(lblResultado, lista.Count, hayFiltro);
        }

        private void cargarFiltros()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            ddlMarca.DataSource = marcaNegocio.listar();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlMarca.Items.Insert(
                0,
                new ListItem("Todas las marcas", "0")
            );


            TipoNegocio tipoNegocio = new TipoNegocio();

            ddlTipo.DataSource = tipoNegocio.listar();
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataBind();

            ddlTipo.Items.Insert(
                0,
                new ListItem("Todos los tipos", "0")
            );
        }

        protected void btnBuscar_Click(
            object sender,
            EventArgs e)
        {
            cargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlMarca.SelectedIndex = 0;
            ddlTipo.SelectedIndex = 0;
            txtTalle.Text = "";
            txtColor.Text = "";
            chkStockBajo.Checked = false;

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