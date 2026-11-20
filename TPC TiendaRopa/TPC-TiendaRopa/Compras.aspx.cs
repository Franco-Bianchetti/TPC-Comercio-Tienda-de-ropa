using System;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class Compras : System.Web.UI.Page {
        protected void Page_Load(
            object sender,
            EventArgs e){

            if (!IsPostBack){

                    cargarProveedores();

                    cargarGrilla();
                }
            } 

        private void cargarProveedores()
        {
            ProveedorNegocio negocio = new ProveedorNegocio();

            ddlProveedor.DataSource = negocio.listar();

            ddlProveedor.DataTextField = "RazonSocial";

            ddlProveedor.DataValueField = "Id";

            ddlProveedor.DataBind();

            ddlProveedor.Items.Insert(
                0,
                new System.Web.UI.WebControls.ListItem(
                    "Todos los proveedores",
                    "0"
                )
            );
        }



        private void cargarGrilla()
        {
            CompraNegocio negocio = new CompraNegocio();

            int idProveedor = 0;

            int.TryParse(
                ddlProveedor.SelectedValue,
                out idProveedor
            );

            gvCompras.DataSource =
                negocio.listar(idProveedor);

            gvCompras.DataBind();
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
            ddlProveedor.SelectedIndex = 0;

            cargarGrilla();
        }


        protected void btnNuevaCompra_Click(
            object sender,
            EventArgs e) {
            Response.Redirect(
                "FormularioCompra.aspx",
                false);
        }
    }
}