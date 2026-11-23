using Negocio;
using System;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(
            object sender,
            EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            VentaNegocio negocio =
                new VentaNegocio();

            gvVentas.DataSource =
                negocio.listar();

            gvVentas.DataBind();
        }

        protected void btnNuevaVenta_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect(
                "FormularioVenta.aspx",
                false);
        }

        protected void gvVentas_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Detalle")
            {
                int idVenta =
                    int.Parse(
                        e.CommandArgument.ToString());

                Response.Redirect(
                    "VentaDetalle.aspx?id=" +
                    idVenta,
                    false);
            }
        }
    }
}