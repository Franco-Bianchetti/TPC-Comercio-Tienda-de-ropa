using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class VentaDetalle :
        System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                    Response.Redirect(
                        "Ventas.aspx",
                        false);

                    return;
                }

                int idVenta =
                    int.Parse(
                        Request.QueryString["id"]);

                cargarEmpresa();

                cargarVenta(idVenta);

                cargarDetalle(idVenta);
            }
        }

        private void cargarEmpresa()
        {
            EmpresaNegocio negocio =
                new EmpresaNegocio();

            Empresa empresa =
                negocio.obtener();

            if (empresa == null)
                return;

            lblRazonSocial.Text =
                empresa.RazonSocial;

            lblCUIT.Text =
                empresa.CUIT;

            lblDireccion.Text =
                empresa.Direccion;

            lblCondicionIVA.Text =
                "IVA RESPONSABLE INSCRIPTO";

            lblPuntoVenta.Text =
                empresa.PuntoVenta;

            lblIngresosBrutos.Text =
                empresa.IngresosBrutos;

            lblFechaInicioActividad.Text =
                empresa.FechaInicioActividad
                    .ToString("dd/MM/yyyy");

            lblCiudad.Text =
                empresa.Ciudad;
        }

        private void cargarVenta(
            int idVenta)
        {
            VentaNegocio negocio =
                new VentaNegocio();

            Venta venta =
                negocio.obtenerVenta(
                    idVenta);

            if (venta == null)
            {
                Response.Redirect(
                    "Ventas.aspx",
                    false);

                return;
            }

            lblComprobante.Text =
                venta.NumeroComprobante;

            lblFecha.Text =
                venta.Fecha
                    .ToString("dd/MM/yyyy");

            lblCliente.Text =
                venta.Cliente.Nombre +
                " " +
                venta.Cliente.Apellido;

            lblVendedor.Text =
                venta.Vendedor.Nombre +
                " " +
                venta.Vendedor.Apellido;

            lblFormaPago.Text =
                venta.FormaPago;

            lblCAE.Text =
                string.IsNullOrWhiteSpace(
                    venta.CAE)
                ? "N/A"
                : venta.CAE;

            lblVencimientoCAE.Text =
                venta.VencimientoCAE.HasValue
                ? venta.VencimientoCAE.Value
                    .ToString("dd/MM/yyyy")
                : "N/A";

            decimal subtotal;

            if (venta.FormaPago ==
                "EFECTIVO")
            {
                subtotal =
                    venta.Total / 0.80m;
            }
            else
            {
                subtotal =
                    venta.Total;
            }

            decimal descuento =
                subtotal - venta.Total;

            lblSubtotal.Text =
                "$ " +
                subtotal.ToString("N2");

            lblDescuento.Text =
                "$ " +
                descuento.ToString("N2");

            lblTotal.Text =
                "$ " +
                venta.Total.ToString("N2");
        }

        private void cargarDetalle(
            int idVenta)
        {
            VentaNegocio negocio =
                new VentaNegocio();

            List<DetalleVenta> lista =
                negocio.obtenerDetalle(
                    idVenta);

            gvDetalle.DataSource =
                lista;

            gvDetalle.DataBind();
        }
    }
}