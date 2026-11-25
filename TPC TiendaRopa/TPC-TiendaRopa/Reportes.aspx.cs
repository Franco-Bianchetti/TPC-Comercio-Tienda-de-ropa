
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.EsAdmin())
            {
                Response.Redirect("~/Default.aspx", true);
                return;
            }

            if (!IsPostBack)
            {
                DateTime hoy = DateTime.Today;

                txtDesde.Text = new DateTime(
                    hoy.Year, hoy.Month, 1
                ).ToString("yyyy-MM-dd");

                txtHasta.Text = hoy.ToString("yyyy-MM-dd");

                cargarRanking();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarRanking();
        }

        private bool obtenerFechas(out DateTime desde, out DateTime hasta)
        {
            desde = DateTime.MinValue;
            hasta = DateTime.MinValue;

            bool fechaDesdeValida = DateTime.TryParseExact(
                txtDesde.Text,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out desde
            );

            bool fechaHastaValida = DateTime.TryParseExact(
                txtHasta.Text,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out hasta
            );

            if (!fechaDesdeValida || !fechaHastaValida)
            {
                mostrarMensaje("Seleccioná ambas fechas.", true);
                return false;
            }

            if (desde > hasta)
            {
                mostrarMensaje(
                    "La fecha desde no puede ser posterior a la fecha hasta.",
                    true
                );

                return false;
            }

            return true;
        }

        private DataTable obtenerDatosRanking()
        {
            DateTime desde;
            DateTime hasta;

            if (!obtenerFechas(out desde, out hasta))
                return null;

            VentaNegocio negocio = new VentaNegocio();

            return negocio.obtenerRankingVendedores(desde, hasta);
        }

        private void cargarRanking()
        {
            lblMensaje.Visible = false;

            DataTable tabla = obtenerDatosRanking();

            if (tabla == null)
                return;

            gvRanking.DataSource = tabla;
            gvRanking.DataBind();

            lblCantidadVendedores.Text =
                "Vendedores encontrados: " + tabla.Rows.Count;
        }

        private void mostrarMensaje(string mensaje, bool error)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;

            lblMensaje.ForeColor = error
                ? System.Drawing.Color.DarkRed
                : System.Drawing.Color.DarkGreen;
        }


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            DataTable tabla = obtenerDatosRanking();

            if (tabla == null)
                return;

            if (tabla.Rows.Count == 0)
            {
                mostrarMensaje("No hay resultados para exportar.", true);
                return;
            }

            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Codigo;Nombre;Apellido;Cantidad de ventas;Unidades vendidas;Total vendido");

            foreach (DataRow fila in tabla.Rows)
            {
                csv.Append(escaparCSV(fila["Codigo"].ToString())).Append(";");
                csv.Append(escaparCSV(fila["Nombre"].ToString())).Append(";");
                csv.Append(escaparCSV(fila["Apellido"].ToString())).Append(";");
                csv.Append(fila["CantidadVentas"].ToString()).Append(";");
                csv.Append(fila["UnidadesVendidas"].ToString()).Append(";");

                decimal total = Convert.ToDecimal(fila["TotalVendido"]);

                csv.Append(total.ToString("0.00", CultureInfo.InvariantCulture));
                csv.AppendLine();
            }

            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;

            Response.AddHeader(
                "Content-Disposition",
                "attachment; filename=RankingVendedores.csv"
            );

            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            Response.Write(csv.ToString());
            Response.Flush();
            Response.End();
        }


        private string escaparCSV(string valor)
        {
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }
    }
}
