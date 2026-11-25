using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class FormularioCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnCancelarEdicion.Enabled = false;
                lblStockActual.Text = "Stock actual: -";

                cargarProveedores();

                ddlProducto.Items.Clear();
                ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));

                Session["DetalleCompra"] = new List<DetalleCompra>();
                Session["IndiceEdicion"] = null;

                actualizarGrilla();
            }
        }

        private void cargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            ddlProveedor.DataSource = negocio.listar();
            ddlProveedor.DataTextField = "RazonSocial";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(0, new ListItem("Seleccione proveedor", "0"));
        }

        private void cargarProductos(int idProveedor)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            ddlProducto.DataSource = negocio.listarPorProveedor(idProveedor);
            ddlProducto.DataTextField = "DescripcionCompra";
            ddlProducto.DataValueField = "Id";
            ddlProducto.DataBind();

            ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));
            lblStockActual.Text = "Stock actual: -";
        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProducto.Items.Clear();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));
            lblStockActual.Text = "Stock actual: -";

            int idProveedor;
            if (!int.TryParse(ddlProveedor.SelectedValue, out idProveedor) || idProveedor <= 0)
                return;

            cargarProductos(idProveedor);
        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto;
            if (!int.TryParse(ddlProducto.SelectedValue, out idProducto) || idProducto <= 0)
            {
                lblStockActual.Text = "Stock actual: -";
                return;
            }

            ProductoNegocio negocio = new ProductoNegocio();
            Producto prod = negocio.buscar(idProducto);

            if (prod != null)
                lblStockActual.Text = $"Stock actual: {prod.StockActual} unidades";
            else
                lblStockActual.Text = "Stock actual: -";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (ddlProveedor.SelectedValue == "0")
            {
                mostrarError("Debe seleccionar un proveedor.");
                return;
            }

            if (ddlProducto.SelectedValue == "0")
            {
                mostrarError("Debe seleccionar un producto.");
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlTipoComprobante.SelectedValue))
            {
                mostrarError("Debe seleccionar el tipo de comprobante.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroComprobante.Text))
            {
                mostrarError("Debe ingresar el número de comprobante.");
                return;
            }

            int cantidad;
            if (!int.TryParse(txtCantidad.Text.Trim(), out cantidad))
            {
                mostrarError("La cantidad debe ser numérica.");
                return;
            }

            if (cantidad <= 0)
            {
                mostrarError("La cantidad debe ser mayor a cero.");
                return;
            }

            if (cantidad > 10000)
            {
                mostrarError("La cantidad ingresada es demasiado grande.");
                return;
            }

            decimal precio;
            string textoPrecio = txtPrecioCompra.Text.Trim().Replace(',', '.');

            if (!decimal.TryParse(textoPrecio, NumberStyles.Number, CultureInfo.InvariantCulture, out precio))
            {
                mostrarError("El precio debe ser válido.");
                return;
            }

            if (precio <= 0)
            {
                mostrarError("El precio debe ser mayor a cero.");
                return;
            }

            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];
            int idProducto = int.Parse(ddlProducto.SelectedValue);
            object indiceEdicion = Session["IndiceEdicion"];

            if (indiceEdicion == null)
            {
                if (lista.Any(x => x.Producto.Id == idProducto))
                {
                    mostrarError("El producto ya se encuentra agregado a la compra.");
                    return;
                }
            }

            DetalleCompra detalle = new DetalleCompra
            {
                Producto = new Producto
                {
                    Id = idProducto,
                    Nombre = ddlProducto.SelectedItem.Text
                },
                Cantidad = cantidad,
                PrecioCompra = precio
            };

            if (indiceEdicion != null)
            {
                lista[(int)indiceEdicion] = detalle;
                Session["IndiceEdicion"] = null;
                btnCancelarEdicion.Enabled = false;
                btnAgregar.Text = "Agregar";
            }
            else
            {
                lista.Add(detalle);
            }

            Session["DetalleCompra"] = lista;

            ddlProveedor.Enabled = false;
            ddlTipoComprobante.Enabled = false;
            txtNumeroComprobante.Enabled = false;

            limpiarFormulario();
            actualizarGrilla();
        }

        protected void gvDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];
            int indice = int.Parse(e.CommandArgument.ToString());

            if (indice < 0 || indice >= lista.Count)
                return;

            if (e.CommandName == "Eliminar")
            {
                lista.RemoveAt(indice);
                Session["DetalleCompra"] = lista;

                if (lista.Count == 0)
                {
                    ddlProveedor.Enabled = true;
                    ddlTipoComprobante.Enabled = true;
                    txtNumeroComprobante.Enabled = true;

                    ddlProveedor.SelectedIndex = 0;
                    ddlTipoComprobante.SelectedIndex = 0;
                    txtNumeroComprobante.Text = "";

                    ddlProducto.Items.Clear();
                    ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));
                    lblStockActual.Text = "Stock actual: -";
                }

                actualizarGrilla();
                return;
            }

            if (e.CommandName == "Editar")
            {
                DetalleCompra detalle = lista[indice];

                ddlProducto.SelectedValue = detalle.Producto.Id.ToString();
                txtCantidad.Text = detalle.Cantidad.ToString();
                txtPrecioCompra.Text = detalle.PrecioCompra.ToString("0.00", CultureInfo.InvariantCulture);

                ddlProducto_SelectedIndexChanged(ddlProducto, EventArgs.Empty);

                btnAgregar.Text = "Guardar cambios";
                Session["IndiceEdicion"] = indice;
                btnCancelarEdicion.Enabled = true;
            }
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            Session["IndiceEdicion"] = null;
            btnCancelarEdicion.Enabled = false;
            btnAgregar.Text = "Agregar";
            limpiarFormulario();
        }

        protected void btnVaciarCompra_Click(object sender, EventArgs e)
        {
            Session["DetalleCompra"] = new List<DetalleCompra>();
            Session["IndiceEdicion"] = null;

            ddlProveedor.Enabled = true;
            ddlTipoComprobante.Enabled = true;
            txtNumeroComprobante.Enabled = true;

            ddlProveedor.SelectedIndex = 0;
            ddlTipoComprobante.SelectedIndex = 0;
            txtNumeroComprobante.Text = "";

            btnCancelarEdicion.Enabled = false;
            btnAgregar.Text = "Agregar";

            ddlProducto.Items.Clear();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));
            lblStockActual.Text = "Stock actual: -";

            limpiarFormulario();
            actualizarGrilla();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];

            if (lista.Count == 0)
            {
                mostrarError("Debe agregar al menos un producto.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumeroComprobante.Text))
            {
                mostrarError("Debe ingresar el número de comprobante.");
                return;
            }

            Compra compra = new Compra
            {
                Fecha = DateTime.Now,
                Usuario = new Usuario { Id = (int)Session["IdUsuario"] },
                Proveedor = new Proveedor { Id = int.Parse(ddlProveedor.SelectedValue) },
                TipoComprobante = ddlTipoComprobante.SelectedValue,
                NumeroComprobante = txtNumeroComprobante.Text.Trim()
            };

            foreach (DetalleCompra detalle in lista)
                compra.Detalles.Add(detalle);

            compra.Total = compra.CalcularTotal();

            CompraNegocio negocio = new CompraNegocio();
            negocio.guardar(compra);

            Session["DetalleCompra"] = new List<DetalleCompra>();
            Session["IndiceEdicion"] = null;

            btnCancelarEdicion.Enabled = false;
            btnAgregar.Text = "Agregar";

            ddlProveedor.Enabled = true;
            ddlTipoComprobante.Enabled = true;
            txtNumeroComprobante.Enabled = true;

            ddlProveedor.SelectedIndex = 0;
            ddlTipoComprobante.SelectedIndex = 0;
            txtNumeroComprobante.Text = "";

            ddlProducto.Items.Clear();
            ddlProducto.Items.Insert(0, new ListItem("Seleccione producto", "0"));
            lblStockActual.Text = "Stock actual: -";

            limpiarFormulario();
            actualizarGrilla();

            mostrarExito("Compra registrada correctamente.");
        }

        private void limpiarFormulario()
        {
            if (ddlProducto.Items.Count > 0)
                ddlProducto.SelectedIndex = 0;

            txtCantidad.Text = "";
            txtPrecioCompra.Text = "";
            lblStockActual.Text = "Stock actual: -";
        }

        private void actualizarGrilla()
        {
            List<DetalleCompra> lista = (List<DetalleCompra>)Session["DetalleCompra"];

            gvDetalle.DataSource = lista;
            gvDetalle.DataBind();

            lblTotal.Text = "$" + lista.Sum(x => x.Subtotal).ToString("0.00");
        }

        private void mostrarError(string mensaje)
        {
            lblError.CssClass = "text-danger";
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        private void mostrarExito(string mensaje)
        {
            lblError.CssClass = "text-success";
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}