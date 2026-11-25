using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa {
    public partial class Usuarios : System.Web.UI.Page {
        protected void Page_Load(
            object sender,
            EventArgs e) {
            if (!IsPostBack) {
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> lista = negocio.listar(Seguridad.EsAdmin());

            string texto = txtBuscar.Text.Trim();

            if (texto != "")
            {
                lista = lista.FindAll(x =>
                    Filtros.Contiene(x.Nombre, texto) ||
                    Filtros.Contiene(x.Apellido, texto) ||
                    Filtros.Contiene(x.NombreUsuario, texto) ||
                    Filtros.Contiene(x.Dni, texto));
            }

            gvUsuarios.DataSource = lista;
            gvUsuarios.DataBind();

            Filtros.MostrarResultado(lblResultado, lista.Count, texto != "", texto);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            cargarGrilla();
        }

        protected void btnNuevo_Click(
            object sender,
            EventArgs e) {
            Response.Redirect(
                "FormularioUsuario.aspx",
                false);
        }

        protected void gvUsuarios_RowDataBound(
            object sender,
            GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            Usuario usuario = (Usuario)e.Row.DataItem;

            if (!usuario.Activo)
            {
                e.Row.CssClass = "table-secondary text-muted";

                var pnlActiva = e.Row.FindControl("pnlActiva");
                var pnlInactiva = e.Row.FindControl("pnlInactiva");

                if (pnlActiva != null)
                    pnlActiva.Visible = false;

                if (pnlInactiva != null)
                    pnlInactiva.Visible = true;
            }
        }

        protected void gvUsuarios_RowCommand(
            object sender,
            GridViewCommandEventArgs e) {

            if (e.CommandName == "Eliminar")
            {
                int id = int.Parse(e.CommandArgument.ToString());

                if (id == Convert.ToInt32(Session["IdUsuario"]))
                {
                    lblResultado.Visible = true;
                    lblResultado.CssClass = "d-block mb-3 text-danger";
                    lblResultado.Text = "No podés eliminar tu propio usuario.";

                    return;
                }

                UsuarioNegocio negocio = new UsuarioNegocio();

                negocio.eliminar(id);

                cargarGrilla();
            }

            if (e.CommandName == "Reactivar")
            {
                if (!Seguridad.EsAdmin())
                    return;

                int id = int.Parse(e.CommandArgument.ToString());

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.reactivar(id);

                cargarGrilla();
            }
        }
    }
}