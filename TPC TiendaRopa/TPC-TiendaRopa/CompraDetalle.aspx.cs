using System;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class CompraDetalle : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDetalleCompra();
            }
        }

        private void cargarDetalleCompra()
        {
            string valorId =
                Request.QueryString["id"];

            int idCompra;

            if (!int.TryParse(
                    valorId,
                    out idCompra))
            {
                Response.Redirect(
                    "Compras.aspx",
                    false);

                return;
            }

            CompraNegocio negocio =
                new CompraNegocio();

            Compra compra =
                negocio.obtenerCompra(
                    idCompra);

            if (compra == null)
            {
                Response.Redirect(
                    "Compras.aspx",
                    false);

                return;
            }

            lblFecha.Text =
                compra.Fecha.ToString(
                    "dd/MM/yyyy HH:mm");

            lblUsuario.Text =
                compra.Usuario.Nombre +
                " " +
                compra.Usuario.Apellido;

            lblProveedor.Text =
                compra.Proveedor.RazonSocial;

            lblTipoComprobante.Text =
                compra.TipoComprobante;

            lblNumeroComprobante.Text =
                compra.NumeroComprobante;

            lblImporteTotal.Text =
                compra.Total.ToString("C");

            gvDetalle.DataSource =
                negocio.obtenerDetalle(
                    idCompra);

            gvDetalle.DataBind();
        }
    }
}