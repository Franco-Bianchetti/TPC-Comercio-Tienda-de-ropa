using System;
using Negocio;
using System.Collections.Generic;
using Dominio;

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
            CompraNegocio negocio =
                new CompraNegocio();

            int idProveedor = 0;

            int.TryParse(
                ddlProveedor.SelectedValue,
                out idProveedor);

            string comprobante =
                txtComprobante.Text.Trim();

            DateTime fechaDesde;

            DateTime? desde = null;

            if (DateTime.TryParse(
                    txtFechaDesde.Text,
                    out fechaDesde))
            {
                desde = fechaDesde;
            }

            DateTime fechaHasta;

            DateTime? hasta = null;

            if (DateTime.TryParse(
                    txtFechaHasta.Text,
                    out fechaHasta))
            {
                hasta = fechaHasta;
            }

            List<Compra> lista =
                 negocio.listar(
                    idProveedor,
                    comprobante,
                    desde,
                    hasta);

            gvCompras.DataSource = lista;
            gvCompras.DataBind();

            bool hayFiltro =
                idProveedor > 0 ||
                comprobante != "" ||
                desde.HasValue ||
                hasta.HasValue;

            Filtros.MostrarResultado(lblResultado, lista.Count, hayFiltro);
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

            txtComprobante.Text = "";

            txtFechaDesde.Text = "";

            txtFechaHasta.Text = "";

            cargarGrilla();
        }


        protected void btnNuevaCompra_Click(
            object sender,
            EventArgs e) {
            Response.Redirect(
                "FormularioCompra.aspx",
                false);
        }

        protected void gvCompras_RowCommand(
            object sender,
            System.Web.UI.WebControls.GridViewCommandEventArgs e)
                {
                    if (e.CommandName == "VerDetalle")
                    {
                        int idCompra =
                            int.Parse(
                                e.CommandArgument.ToString());

                        Response.Redirect(
                            "CompraDetalle.aspx?id=" +
                            idCompra,
                            false);
                    }
                }

    }
}