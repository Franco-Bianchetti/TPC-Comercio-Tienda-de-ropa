using System;
using Negocio;

namespace TPC_TiendaRopa {
    public partial class Compras : System.Web.UI.Page {
        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (!IsPostBack) {
                cargarGrilla();
            }
        }

        private void cargarGrilla() {
            CompraNegocio negocio =
                new CompraNegocio();

            gvCompras.DataSource =
                negocio.listar();

            gvCompras.DataBind();
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