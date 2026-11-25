using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class CompraDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCompra;
                if (!int.TryParse(Request.QueryString["id"], out idCompra))
                {
                    Response.Redirect("Compras.aspx", false);
                    return;
                }

                cargarCompra(idCompra);
                cargarDetalle(idCompra);
            }
        }

        private void cargarCompra(int idCompra)
        {
            CompraNegocio negocio = new CompraNegocio();
            Compra compra = negocio.obtenerCompra(idCompra);

            if (compra == null)
            {
                Response.Redirect("Compras.aspx", false);
                return;
            }

            lblTipoComprobante.Text = compra.TipoComprobante;
            lblNumeroComprobante.Text = compra.NumeroComprobante;
            lblFecha.Text = compra.Fecha.ToString("dd/MM/yyyy HH:mm");

            lblProveedor.Text = compra.Proveedor != null
                ? compra.Proveedor.RazonSocial
                : "-";

            lblUsuario.Text = compra.Usuario != null
                ? compra.Usuario.Codigo + " - " + compra.Usuario.Nombre + " " + compra.Usuario.Apellido
                : "-";

            lblTotal.Text = "$ " + compra.Total.ToString("N2");
        }

        private void cargarDetalle(int idCompra)
        {
            CompraNegocio negocio = new CompraNegocio();
            List<DetalleCompra> lista = negocio.obtenerDetalle(idCompra);

            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();
        }
    }
}