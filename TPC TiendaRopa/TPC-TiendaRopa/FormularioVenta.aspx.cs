using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioVenta : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCancelarEdicion.Enabled = false;

                cargarClientes();

                cargarProductos();

                Session["DetalleVenta"] =
                    new List<DetalleVenta>();

                Session["IndiceEdicion"] =
                    null;

                actualizarGrilla();

                lblDescuento.Text = "$0,00";

                lblTotal.Text = "$0,00";
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
                    "Seleccione cliente",
                    "0"));
        }

        private void cargarProductos()
        {
            ProductoNegocio negocio =
                new ProductoNegocio();

            ddlProducto.DataSource =
                negocio.listarConStock();

            ddlProducto.DataTextField =
                "DescripcionCompra";

            ddlProducto.DataValueField =
                "Id";

            ddlProducto.DataBind();

            ddlProducto.Items.Insert(
                0,
                new ListItem(
                    "Seleccione producto",
                    "0"));
        }

        protected void ddlProducto_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            txtPrecioUnitario.Text = "";

            if (ddlProducto.SelectedValue == "0")
                return;

            VentaNegocio negocio =
                new VentaNegocio();

            Producto producto =
                negocio.obtenerProducto(
                    int.Parse(
                        ddlProducto.SelectedValue));

            if (producto == null)
                return;

            txtStock.Text =
                producto.StockActual.ToString();

            txtPrecioUnitario.Text =
                producto.ObtenerPrecioVenta()
                    .ToString("0.00");
        }
        protected void ddlFormaPago_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            actualizarGrilla();
        }

        private void actualizarGrilla()
        {
            List<DetalleVenta> lista =
                Session["DetalleVenta"] as
                List<DetalleVenta>;

            if (lista == null)
            {
                lista = new List<DetalleVenta>();
            }

            gvDetalle.DataSource =
                lista;

            gvDetalle.DataBind();

            decimal subtotal =
                lista.Sum(x => x.Subtotal);

            lblSubtotal.Text =
                "$" + subtotal.ToString("N2");

            decimal descuento = 0;

            if (ddlFormaPago.SelectedValue ==
                "EFECTIVO")
            {
                descuento =
                    subtotal * 0.20m;
            }

            lblDescuento.Text =
                "$" + descuento.ToString("N2");

            lblTotal.Text =
                "$" + (subtotal - descuento)
                .ToString("N2");
        }

        private void limpiarFormulario()
        {
            ddlProducto.SelectedIndex = 0;

            txtCantidad.Text = "";

            txtPrecioUnitario.Text = "";

            txtStock.Text = "";
        }

        private void mostrarError(
            string mensaje)
        {
            lblError.CssClass =
                "text-danger";

            lblError.Text =
                mensaje;

            lblError.Visible =
                true;
        }

        private void mostrarExito(
            string mensaje)
        {
            lblError.CssClass =
                "text-success";

            lblError.Text =
                mensaje;

            lblError.Visible =
                true;
        }

        protected void btnAgregar_Click(
            object sender,
            EventArgs e)
        {
            lblError.Visible = false;

            if (ddlCliente.SelectedValue == "0")
            {
                mostrarError(
                    "Debe seleccionar un cliente.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    ddlTipoFactura.SelectedValue))
            {
                mostrarError(
                    "Debe seleccionar un tipo de factura.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    ddlFormaPago.SelectedValue))
            {
                mostrarError(
                    "Debe seleccionar una forma de pago.");

                return;
            }

            if (ddlProducto.SelectedValue == "0")
            {
                mostrarError(
                    "Debe seleccionar un producto.");

                return;
            }

            int cantidad;

            if (!int.TryParse(
                    txtCantidad.Text.Trim(),
                    out cantidad))
            {
                mostrarError(
                    "La cantidad debe ser numérica.");

                return;
            }

            if (cantidad <= 0)
            {
                mostrarError(
                    "La cantidad debe ser mayor a cero.");

                return;
            }

            VentaNegocio negocio =
                new VentaNegocio();

            Producto producto =
                negocio.obtenerProducto(
                    int.Parse(
                        ddlProducto.SelectedValue));

            if (producto == null)
            {
                mostrarError(
                    "No se encontró el producto.");

                return;
            }

            if (cantidad > producto.StockActual)
            {
                mostrarError(
                    "Stock insuficiente. Disponible: "
                    + producto.StockActual);

                return;
            }

            List<DetalleVenta> lista =
                Session["DetalleVenta"] as
                List<DetalleVenta>;

            if (lista == null)
            {
                lista = new List<DetalleVenta>();
            }

            int idProducto =
                producto.Id;

            object indiceEdicion =
                Session["IndiceEdicion"];

            if (indiceEdicion == null)
            {
                if (lista.Any(
                    x => x.Producto.Id ==
                         idProducto))
                {
                    mostrarError(
                        "El producto ya se encuentra agregado a la venta.");

                    return;
                }
            }

            DetalleVenta detalle =
                new DetalleVenta();

            detalle.Producto =
                producto;

            detalle.Cantidad =
                cantidad;

            detalle.PrecioUnitario =
                producto.ObtenerPrecioVenta();

            if (indiceEdicion != null)
            {
                lista[(int)indiceEdicion] =
                    detalle;

                Session["IndiceEdicion"] =
                    null;

                btnCancelarEdicion.Enabled =
                    false;

                btnAgregar.Text =
                    "Agregar";
            }
            else
            {
                lista.Add(detalle);
            }

            Session["DetalleVenta"] =
                lista;

            ddlCliente.Enabled = false;

            ddlTipoFactura.Enabled = false;

            ddlFormaPago.Enabled = false;

            actualizarGrilla();

            limpiarFormulario();

            lblError.Visible = false;
        }

        protected void btnCancelarEdicion_Click(
            object sender,
            EventArgs e)
        {
            Session["IndiceEdicion"] =
                null;

            btnCancelarEdicion.Enabled =
                false;

            btnAgregar.Text =
                "Agregar";

            limpiarFormulario();
        }

        protected void gvDetalle_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            List<DetalleVenta> lista =
                Session["DetalleVenta"] as
                List<DetalleVenta>;

            if (lista == null)
            {
                lista = new List<DetalleVenta>();
            }

            int indice =
                int.Parse(
                    e.CommandArgument.ToString());

            if (indice < 0 ||
                indice >= lista.Count)
                return;

            if (e.CommandName == "Eliminar")
            {
                lista.RemoveAt(indice);

                Session["DetalleVenta"] =
                    lista;

                if (lista.Count == 0)
                {
                    ddlCliente.Enabled = true;

                    ddlTipoFactura.Enabled = true;

                    ddlFormaPago.Enabled = true;
                }

                actualizarGrilla();

                return;
            }

            if (e.CommandName == "Editar")
            {
                DetalleVenta detalle =
                    lista[indice];

                ddlProducto.SelectedValue =
                    detalle.Producto.Id.ToString();

                txtCantidad.Text =
                    detalle.Cantidad.ToString();

                txtPrecioUnitario.Text =
                    detalle.PrecioUnitario
                        .ToString("0.00");

                txtStock.Text =
                    detalle.Producto.StockActual
                        .ToString();

                btnAgregar.Text =
                    "Guardar cambios";

                Session["IndiceEdicion"] =
                    indice;

                btnCancelarEdicion.Enabled =
                    true;
            }
        }

        protected void btnConfirmar_Click(
            object sender,
            EventArgs e)
        {
            List<DetalleVenta> lista =
                Session["DetalleVenta"] as List<DetalleVenta>;

            if (lista == null)
            {
                mostrarError(
                    "La venta está vacía.");

                return;
            }

            if (lista == null ||
                lista.Count == 0)
            {
                mostrarError(
                    "Debe agregar al menos un producto.");

                return;
            }

            if (ddlCliente.SelectedValue == "0")
            {
                mostrarError(
                    "Debe seleccionar un cliente.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    ddlTipoFactura.SelectedValue))
            {
                mostrarError(
                    "Debe seleccionar un tipo de factura.");

                return;
            }

            if (string.IsNullOrWhiteSpace(
                    ddlFormaPago.SelectedValue))
            {
                mostrarError(
                    "Debe seleccionar una forma de pago.");

                return;
            }

            Venta venta =
                new Venta();

            venta.Fecha =
                DateTime.Now;

            venta.Cliente =
                new Cliente();

            venta.Cliente.Id =
                int.Parse(
                    ddlCliente.SelectedValue);

            if (Session["IdUsuario"] == null)
            {
                mostrarError(
                    "La sesión ha expirado.");

                return;
            }
            venta.Vendedor =
                new Usuario();

            venta.Vendedor.Id =
                (int)Session["IdUsuario"];

            venta.TipoFactura =
                ddlTipoFactura.SelectedValue;

            venta.FormaPago =
                ddlFormaPago.SelectedValue;

            VentaNegocio negocio =
                new VentaNegocio();

            if (venta.TipoFactura ==
                "PRESUPUESTO")
            {
                venta.NumeroComprobante =
                    "N/A";

                venta.CAE =
                    "N/A";

                venta.VencimientoCAE =
                    null;
            }
            else
            {
                venta.NumeroComprobante =
                    negocio.ObtenerProximoNumeroComprobante();

                venta.CAE =
                    DateTime.Now.ToString("yyyyMMdd")
                    + new Random().Next(1000, 9999);

                venta.VencimientoCAE =
                    DateTime.Today.AddDays(10);
            }

            foreach (DetalleVenta detalle
                in lista)
            {
                venta.Detalles.Add(
                    detalle);
            }

            decimal subtotal =
                venta.CalcularTotal();

            decimal descuento = 0;

            if (venta.FormaPago ==
                "EFECTIVO")
            {
                descuento =
                    subtotal * 0.20m;
            }

            venta.Total =
                subtotal - descuento;

            negocio.guardar(
                venta);

            Session["DetalleVenta"] =
                new List<DetalleVenta>();

            Session["IndiceEdicion"] =
                null;

            btnCancelarEdicion.Enabled =
                false;

            btnAgregar.Text =
                "Agregar";

            ddlCliente.SelectedIndex = 0;

            ddlTipoFactura.SelectedIndex = 0;

            ddlFormaPago.SelectedIndex = 0;

            ddlCliente.Enabled = true;

            ddlTipoFactura.Enabled = true;

            ddlFormaPago.Enabled = true;

            limpiarFormulario();

            actualizarGrilla();

            lblDescuento.Text =
                "$0,00";

            lblSubtotal.Text =
                "$0,00";

            lblTotal.Text =
                "$0,00";

            mostrarExito(
                "Venta registrada correctamente y stock actualizado.");
        }

        protected void btnVaciarVenta_Click(
            object sender,
            EventArgs e)
        {
            Session["DetalleVenta"] =
                new List<DetalleVenta>();

            Session["IndiceEdicion"] =
                null;

            btnCancelarEdicion.Enabled =
                false;

            btnAgregar.Text =
                "Agregar";

            ddlCliente.SelectedIndex = 0;

            ddlTipoFactura.SelectedIndex = 0;

            ddlFormaPago.SelectedIndex = 0;

            limpiarFormulario();

            actualizarGrilla();

            lblDescuento.Text = "$0,00";

            lblSubtotal.Text =   "$0,00";

            lblTotal.Text = "$0,00";

            ddlCliente.Enabled = true;

            ddlTipoFactura.Enabled = true;

            ddlFormaPago.Enabled = true;
        }
    }
}