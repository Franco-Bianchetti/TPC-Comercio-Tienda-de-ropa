using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class VentaDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idVenta;
                if (!int.TryParse(Request.QueryString["id"], out idVenta))
                {
                    Response.Redirect("Ventas.aspx", false);
                    return;
                }

                cargarEmpresa();
                cargarVenta(idVenta);
                cargarDetalle(idVenta);

                hlPdf.NavigateUrl = "VentaDetalleImpresion.aspx?id=" + idVenta;
            }
        }

        private void cargarEmpresa()
        {
            EmpresaNegocio negocio = new EmpresaNegocio();
            Empresa empresa = negocio.obtener();

            if (empresa == null)
                return;

            lblRazonSocial.Text = empresa.RazonSocial;
            lblCUIT.Text = empresa.CUIT;
            lblDireccion.Text = empresa.Direccion;
            lblCondicionIVA.Text = empresa.CondicionIVA;
            lblPuntoVenta.Text = empresa.PuntoVenta;
            lblIngresosBrutos.Text = empresa.IngresosBrutos;
            lblFechaInicioActividad.Text = empresa.FechaInicioActividad.ToString("dd/MM/yyyy");
            lblCiudad.Text = empresa.Ciudad;
        }

        private void cargarVenta(int idVenta)
        {
            VentaNegocio negocio = new VentaNegocio();
            Venta venta = negocio.obtenerVenta(idVenta);

            if (venta == null)
            {
                Response.Redirect("Ventas.aspx", false);
                return;
            }

            switch (venta.TipoFactura)
            {
                case "FACTURA A":
                    lblLetra.Text = "A";
                    lblCodigoComprobante.Text = "CÓD. 01";
                    break;

                case "FACTURA C":
                    lblLetra.Text = "C";
                    lblCodigoComprobante.Text = "CÓD. 11";
                    break;

                case "PRESUPUESTO":
                    lblTituloComprobante.Text = "PRESUPUESTO";
                    lblLetra.Text = "X";
                    lblCodigoComprobante.Text = "";
                    imgQR.Visible = false;
                    break;
            }

            lblComprobante.Text = venta.NumeroComprobante;
            lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy");

            if (venta.Cliente != null)
            {
                lblCliente.Text = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
                lblDniCliente.Text = string.IsNullOrWhiteSpace(venta.Cliente.Dni)
                    ? "-"
                    : venta.Cliente.Dni;
                lblDireccionCliente.Text = string.IsNullOrWhiteSpace(venta.Cliente.Direccion)
                    ? "-"
                    : venta.Cliente.Direccion;
                lblTelefonoCliente.Text = string.IsNullOrWhiteSpace(venta.Cliente.Telefono)
                    ? "-"
                    : venta.Cliente.Telefono;
                lblEmailCliente.Text = string.IsNullOrWhiteSpace(venta.Cliente.Email)
                    ? "-"
                    : venta.Cliente.Email;
            }
            else
            {
                lblCliente.Text = "-";
                lblDniCliente.Text = "-";
                lblDireccionCliente.Text = "-";
                lblTelefonoCliente.Text = "-";
                lblEmailCliente.Text = "-";
            }

            if (venta.Vendedor != null)
            {
                string codigo = string.IsNullOrWhiteSpace(venta.Vendedor.Codigo)
                    ? ""
                    : venta.Vendedor.Codigo + " - ";

                lblVendedor.Text = codigo + venta.Vendedor.Nombre + " " + venta.Vendedor.Apellido;
            }
            else
            {
                lblVendedor.Text = "-";
            }

            lblFormaPago.Text = venta.FormaPago;

            lblCAE.Text = string.IsNullOrWhiteSpace(venta.CAE)
                ? "N/A"
                : venta.CAE;

            lblVencimientoCAE.Text = venta.VencimientoCAE.HasValue
                ? venta.VencimientoCAE.Value.ToString("dd/MM/yyyy")
                : "N/A";

            decimal subtotal = (venta.FormaPago == "EFECTIVO")
                ? venta.Total / 0.80m
                : venta.Total;

            decimal descuento = subtotal - venta.Total;

            lblSubtotal.Text = "$ " + subtotal.ToString("N2");
            lblDescuento.Text = "$ " + descuento.ToString("N2");
            lblTotal.Text = "$ " + venta.Total.ToString("N2");
        }

        private void cargarDetalle(int idVenta)
        {
            VentaNegocio negocio = new VentaNegocio();
            List<DetalleVenta> lista = negocio.obtenerDetalle(idVenta);

            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();
        }
    }
}