using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class FormularioProducto : System.Web.UI.Page {
        private int obtenerId() {
            int id = 0;

            if (Request.QueryString["id"] != null)
                int.TryParse(
                    Request.QueryString["id"],
                    out id);

            return id;
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                cargarCombos();

                int id = obtenerId();

                if (id != 0)
                    cargarProducto(id);
            }
        }

        private void cargarCombos() {
            ddlMarca.DataSource = new MarcaNegocio().listar();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlTipo.DataSource = new TipoNegocio().listar();
            ddlTipo.DataTextField = "Nombre";
            ddlTipo.DataValueField = "Id";
            ddlTipo.DataBind();

            cblProveedores.DataSource = new ProveedorNegocio().listar();
            cblProveedores.DataTextField = "RazonSocial";
            cblProveedores.DataValueField = "Id";
            cblProveedores.DataBind();
        }

        private void cargarProducto(int id) {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.buscar(id);

            if (producto == null)
                return;

            lblTitulo.Text = "Modificar producto";

            txtCodigo.Text = producto.Codigo;
            txtNombre.Text = producto.Nombre;
            txtTalle.Text = producto.Talle;
            txtColor.Text = producto.Color;

            txtStockMinimo.Text =
                producto.StockMinimo.ToString();

            txtPorcentaje.Text =
                producto.PorcentajeGanancia.ToString(
                    "0.##",
                    CultureInfo.InvariantCulture);

            seleccionar(
                ddlMarca,
                producto.Marca.Id);

            seleccionar(
                ddlTipo,
                producto.Tipo.Id);

            foreach (ListItem item in cblProveedores.Items) {
                foreach (Proveedor proveedor in producto.Proveedores) {
                    if (proveedor.Id.ToString() == item.Value)
                        item.Selected = true;
                }
            }
        }

        private void seleccionar(
            DropDownList combo,
            int valor) {
            ListItem item =
                combo.Items.FindByValue(
                    valor.ToString());

            if (item != null)
                combo.SelectedValue =
                    item.Value;
        }

        protected void btnGuardar_Click(
            object sender,
            EventArgs e) {
            lblError.Visible = false;

            int id = obtenerId();

            string codigo =
                txtCodigo.Text.Trim().ToUpper();

            string nombre =
                txtNombre.Text.Trim().ToUpper();

            string talle =
                txtTalle.Text.Trim().ToUpper();

            string color =
                txtColor.Text.Trim().ToUpper();

            if (!Regex.IsMatch(
                codigo,
                @"^[A-Z0-9\-]{2,30}$")) {
                mostrarError(
                    "El código debe tener entre 2 y 30 caracteres, utilizando letras, números o guiones.");
                return;
            }

            if (nombre.Length < 2 ||
                nombre.Length > 150) {
                mostrarError(
                    "El nombre debe tener entre 2 y 150 caracteres.");
                return;
            }

            if (ddlMarca.SelectedValue == "0") {
                mostrarError(
                    "Debe seleccionar una marca.");
                return;
            }

            if (ddlTipo.SelectedValue == "0") {
                mostrarError(
                    "Debe seleccionar un tipo.");
                return;
            }

            if (talle.Length < 1 ||
                talle.Length > 20) {
                mostrarError(
                    "El talle debe tener entre 1 y 20 caracteres.");
                return;
            }

            if (!Regex.IsMatch(
                color,
                @"^[A-ZÁÉÍÓÚÑ\s]{2,50}$")) {
                mostrarError(
                    "El color debe contener únicamente letras y tener entre 2 y 50 caracteres.");
                return;
            }

            int stockMinimo;

            if (!int.TryParse(
                    txtStockMinimo.Text.Trim(),
                    out stockMinimo)
                || stockMinimo < 0) {
                mostrarError(
                    "El stock mínimo debe ser un número mayor o igual a cero.");
                return;
            }

            decimal porcentaje;

            string textoPorcentaje =
                txtPorcentaje.Text
                    .Trim()
                    .Replace(',', '.');

            if (!decimal.TryParse(
                    textoPorcentaje,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out porcentaje)
                || porcentaje <= 0
                || porcentaje > 999.99m) {
                mostrarError(
                    "El porcentaje de ganancia debe ser mayor a 0 y menor a 1000.");
                return;
            }

            Producto producto =
                new Producto();

            producto.Id = id;
            producto.Codigo = codigo;
            producto.Nombre = nombre;
            producto.Talle = talle;
            producto.Color = color;
            producto.StockMinimo = stockMinimo;
            producto.PorcentajeGanancia =
                Math.Round(
                    porcentaje,
                    2);

            producto.Marca = new Marca();
            producto.Marca.Id =
                int.Parse(
                    ddlMarca.SelectedValue);

            producto.Tipo = new Tipo();
            producto.Tipo.Id =
                int.Parse(
                    ddlTipo.SelectedValue);

            foreach (ListItem item in cblProveedores.Items) {
                if (item.Selected) {
                    Proveedor proveedor =
                        new Proveedor();

                    proveedor.Id =
                        int.Parse(item.Value);

                    producto.Proveedores.Add(
                        proveedor);
                }
            }

            if (producto.Proveedores.Count == 0) {
                mostrarError(
                    "Debe seleccionar al menos un proveedor.");
                return;
            }

            ProductoNegocio negocio =
                new ProductoNegocio();

            if (negocio.existeCodigo(codigo, id)) {
                mostrarError(
                    "Ya existe un producto con ese código.");
                return;
            }

            if (id == 0)
                negocio.agregar(producto);
            else
                negocio.modificar(producto);

            Response.Redirect(
                "Productos.aspx",
                false);
        }

        private void mostrarError(
            string mensaje) {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}