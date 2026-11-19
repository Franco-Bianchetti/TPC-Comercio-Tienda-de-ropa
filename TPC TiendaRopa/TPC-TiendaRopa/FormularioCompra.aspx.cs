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
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProveedores();
                

                Session["DetalleCompra"] =
                    new List<DetalleCompra>();

                Session["IndiceEdicion"] =
                    null;

                actualizarGrilla();
            }
        }

        private void cargarProveedores()
        {
            ProveedorNegocio negocio =
                new ProveedorNegocio();

            ddlProveedor.DataSource =
                negocio.listar();

            ddlProveedor.DataTextField =
                "RazonSocial";

            ddlProveedor.DataValueField =
                "Id";

            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(
                0,
                new ListItem(
                    "Seleccione proveedor",
                    "0"));
        }

        private void cargarProductos( int idProveedor)
        {
            ProductoNegocio negocio =
                new ProductoNegocio();

            ddlProducto.DataSource =
                negocio.listarPorProveedor(
                    idProveedor);

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

        protected void btnAgregar_Click(
            object sender,
            EventArgs e)
        {
            lblError.Visible = false;

            if (ddlProveedor.SelectedValue == "0")
            {
                mostrarError(
                    "Debe seleccionar un proveedor.");
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

            if (cantidad > 10000)
            {
                mostrarError(
                    "La cantidad ingresada es demasiado grande.");
                return;
            }

            decimal precio;

            string textoPrecio =
                txtPrecioCompra.Text
                    .Trim()
                    .Replace(',', '.');

            if (!decimal.TryParse(
                    textoPrecio,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out precio))
            {
                mostrarError(
                    "El precio debe ser válido.");
                return;
            }

            if (precio <= 0)
            {
                mostrarError(
                    "El precio debe ser mayor a cero.");
                return;
            }

            List<DetalleCompra> lista =
                (List<DetalleCompra>)
                Session["DetalleCompra"];

            int idProducto =
                int.Parse(
                    ddlProducto.SelectedValue);

            object indiceEdicion =
                Session["IndiceEdicion"];

            if (indiceEdicion == null)
            {
                if (lista.Any(
                    x => x.Producto.Id ==
                         idProducto))
                {
                    mostrarError(
                        "El producto ya se encuentra agregado a la compra.");

                    return;
                }
            }

            DetalleCompra detalle =
                new DetalleCompra();

            detalle.Producto =
                new Producto();

            detalle.Producto.Id =
                idProducto;

            detalle.Producto.Nombre =
                ddlProducto.SelectedItem.Text;

            detalle.Cantidad =
                cantidad;

            detalle.PrecioCompra =
                precio;

            if (indiceEdicion != null)
            {
                lista[(int)indiceEdicion] =
                    detalle;

                Session["IndiceEdicion"] =
                    null;

                btnCancelarEdicion.Visible =
                    false;
            }
            else
            {
                lista.Add(detalle);
            }

            Session["DetalleCompra"] =
                lista;

            ddlProveedor.Enabled =
                false;

            limpiarFormulario();

            actualizarGrilla();
        }

        protected void gvDetalle_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            List<DetalleCompra> lista =
                (List<DetalleCompra>)
                Session["DetalleCompra"];

            int indice =
                int.Parse(
                    e.CommandArgument.ToString());

            if (indice < 0 ||
                indice >= lista.Count)
                return;

            if (e.CommandName == "Eliminar")
            {
                lista.RemoveAt(indice);

                Session["DetalleCompra"] =
                    lista;

                if (lista.Count == 0)
                {
                    ddlProveedor.Enabled =
                        true;
                }

                actualizarGrilla();

                return;
            }

            if (e.CommandName == "Editar")
            {
                DetalleCompra detalle =
                    lista[indice];

                ddlProducto.SelectedValue =
                    detalle.Producto.Id.ToString();

                txtCantidad.Text =
                    detalle.Cantidad.ToString();

                txtPrecioCompra.Text =
                    detalle.PrecioCompra.ToString(
                        CultureInfo.InvariantCulture);

                Session["IndiceEdicion"] =
                    indice;

                btnCancelarEdicion.Visible =
                    true;
            }
        }

        protected void btnCancelarEdicion_Click(
            object sender,
            EventArgs e)
        {
            Session["IndiceEdicion"] =
                null;

            btnCancelarEdicion.Visible =
                false;

            limpiarFormulario();
        }

        protected void btnVaciarCompra_Click(
            object sender,
            EventArgs e)
        {
            Session["DetalleCompra"] =
                new List<DetalleCompra>();

            Session["IndiceEdicion"] =
                null;

            ddlProveedor.Enabled =
                true;

            btnCancelarEdicion.Visible =
                false;

            limpiarFormulario();

            actualizarGrilla();
        }

        protected void btnConfirmar_Click(
            object sender,
            EventArgs e)
        {
            List<DetalleCompra> lista =
                (List<DetalleCompra>)
                Session["DetalleCompra"];

            if (lista.Count == 0)
            {
                mostrarError(
                    "Debe agregar al menos un producto.");
                return;
            }

            Compra compra =
                new Compra();

            compra.Fecha =
                DateTime.Now;

            compra.Usuario =
                new Usuario();

            compra.Usuario.Id = 1;

            compra.Proveedor =
                new Proveedor();

            compra.Proveedor.Id =
                int.Parse(
                    ddlProveedor.SelectedValue);

            foreach (DetalleCompra detalle in lista)
            {
                compra.Detalles.Add(
                    detalle);
            }

            compra.Total =
                compra.CalcularTotal();

            CompraNegocio negocio =
                new CompraNegocio();

            negocio.guardar(
                compra);

            Session["DetalleCompra"] =
                new List<DetalleCompra>();

            Session["IndiceEdicion"] =
                null;

            btnCancelarEdicion.Visible =
                false;

            ddlProveedor.Enabled =
                true;

            ddlProveedor.SelectedIndex =
                0;

            limpiarFormulario();

            actualizarGrilla();

            mostrarError(
                "Compra registrada correctamente.");
        }
        protected void ddlProveedor_SelectedIndexChanged(object sender,
                 EventArgs e)
        {
            ddlProducto.Items.Clear();

            if (ddlProveedor.SelectedValue == "0")
                return;

            cargarProductos(
                int.Parse(
                    ddlProveedor.SelectedValue));
        }

        private void limpiarFormulario()
        {
            ddlProducto.SelectedIndex = 0;

            txtCantidad.Text = "";

            txtPrecioCompra.Text = "";
        }

        private void actualizarGrilla()
        {
            List<DetalleCompra> lista =
                (List<DetalleCompra>)
                Session["DetalleCompra"];

            gvDetalle.DataSource =
                lista;

            gvDetalle.DataBind();

            lblTotal.Text =
                "$" +
                lista.Sum(
                    x => x.Subtotal)
                .ToString("0.00");
        }

        private void mostrarError(
            string mensaje)
        {
            lblError.Text =
                mensaje;

            lblError.Visible =
                true;
        }
    }
}