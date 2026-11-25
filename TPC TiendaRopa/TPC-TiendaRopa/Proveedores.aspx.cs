using Negocio;
using System;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Dominio;

namespace TPC_TiendaRopa
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();
            List<Proveedor> lista = negocio.listar(Seguridad.EsAdmin());

            string texto = txtBuscar.Text.Trim();

            if (texto != "")
            {
                lista = lista.FindAll(x =>
                    Filtros.Contiene(x.RazonSocial, texto) ||
                    Filtros.Contiene(x.Cuit, texto) ||
                    Filtros.Contiene(x.Email, texto));
            }

            gvProveedores.DataSource = lista;
            gvProveedores.DataBind();

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
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProveedor.aspx", false);
        }

        protected void gvProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Proveedor proveedor = (Proveedor)e.Row.DataItem;

            if (!proveedor.Activo)
            {
                e.Row.CssClass = "table-secondary text-muted";
                e.Row.FindControl("pnlActiva").Visible = false;
                e.Row.FindControl("pnlInactiva").Visible = true;
            }
        }

        protected void gvProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.eliminar(id);
                cargarGrilla();
            }
            if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id = int.Parse(e.CommandArgument.ToString());

                ProveedorNegocio negocio = new ProveedorNegocio();
                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}


