using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();

                cargarVendedores();

                cargarGrilla();
            }
        }

        private void cargarClientes()
        {
            ClienteNegocio negocio =
                new ClienteNegocio();

            ddlCliente.DataSource =
                negocio.listar();

            ddlCliente.DataTextField =
                "NombreCompleto";

            ddlCliente.DataValueField =
                "Id";

            ddlCliente.DataBind();

            ddlCliente.Items.Insert(
                0,
                new ListItem(
                    "Todos los clientes",
                    "0"));
        }

        private void cargarVendedores()
        {
            UsuarioNegocio negocio =
                new UsuarioNegocio();

            ddlVendedor.DataSource =
                negocio.listar();

            ddlVendedor.DataTextField =
                "NombreCompleto";

            ddlVendedor.DataValueField =
                "Id";

            ddlVendedor.DataBind();

            ddlVendedor.Items.Insert(
                0,
                new ListItem(
                    "Todos los vendedores",
                    "0"));
        }

        private void cargarGrilla()
        {
            VentaNegocio negocio =
                new VentaNegocio();

            List<Venta> lista =
                negocio.listar();

            bool hayFiltro = false;

            // CLIENTE

            if (ddlCliente.SelectedValue != "0")
            {
                hayFiltro = true;

                int idCliente =
                    int.Parse(
                        ddlCliente.SelectedValue);

                lista = lista
                    .Where(x =>
                        x.Cliente.Id ==
                        idCliente)
                    .ToList();
            }

            // VENDEDOR

            if (ddlVendedor.SelectedValue != "0")
            {
                hayFiltro = true;

                int idVendedor =
                    int.Parse(
                        ddlVendedor.SelectedValue);

                lista = lista
                    .Where(x =>
                        x.Vendedor.Id ==
                        idVendedor)
                    .ToList();
            }

            // FECHA DESDE

            DateTime fechaDesde;

            if (DateTime.TryParse(
                    txtFechaDesde.Text,
                    out fechaDesde))
            {
                hayFiltro = true;

                lista = lista
                    .Where(x =>
                        x.Fecha.Date >=
                        fechaDesde.Date)
                    .ToList();
            }

            // FECHA HASTA

            DateTime fechaHasta;

            if (DateTime.TryParse(
                    txtFechaHasta.Text,
                    out fechaHasta))
            {
                hayFiltro = true;

                lista = lista
                    .Where(x =>
                        x.Fecha.Date <=
                        fechaHasta.Date)
                    .ToList();
            }

            gvVentas.DataSource =
                lista;

            gvVentas.DataBind();

            Filtros.MostrarResultado(
                lblResultado,
                lista.Count,
                hayFiltro);
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
            ddlCliente.SelectedIndex = 0;

            ddlVendedor.SelectedIndex = 0;

            txtFechaDesde.Text = "";

            txtFechaHasta.Text = "";

            cargarGrilla();
        }

        protected void btnNuevaVenta_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioVenta.aspx",
                false);
        }

        protected void gvVentas_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName ==
                "Detalle")
            {
                int idVenta =
                    int.Parse(
                        e.CommandArgument
                            .ToString());

                Response.Redirect(
                    "VentaDetalle.aspx?id="
                    + idVenta,
                    false);
            }
        }
    }
}