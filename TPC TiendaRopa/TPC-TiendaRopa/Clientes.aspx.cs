using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Clientes : System.Web.UI.Page
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
            ClienteNegocio negocio = new ClienteNegocio();
            List<Cliente> lista = negocio.listar(Seguridad.EsAdmin());

            string texto = txtBuscar.Text.Trim();

            if (texto != "")
            {
                lista = lista.FindAll(x =>
                    Filtros.Contiene(x.Nombre, texto) ||
                    Filtros.Contiene(x.Apellido, texto) ||
                    Filtros.Contiene(x.Dni, texto) ||
                    Filtros.Contiene(x.Email, texto));
            }

            gvClientes.DataSource = lista;
            gvClientes.DataBind();

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
                "FormularioCliente.aspx",
                false);
        }
        
        protected void gvClientes_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Cliente cliente = (Cliente)e.Row.DataItem;

            if (!cliente.Activo)
            {
                e.Row.CssClass = "table-secondary text-muted";
                e.Row.FindControl("pnlActiva").Visible = false;
                e.Row.FindControl("pnlInactiva").Visible = true;
            }
        }

        protected void gvClientes_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id =
                    int.Parse(
                        e.CommandArgument.ToString());

                ClienteNegocio negocio = new ClienteNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }
            if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id = int.Parse(e.CommandArgument.ToString());

                ClienteNegocio negocio = new ClienteNegocio();
                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}