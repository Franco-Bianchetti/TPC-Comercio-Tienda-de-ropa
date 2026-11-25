using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Reportes : System.Web.UI.Page
    {
        private readonly ReporteNegocio _reporteNegocio = new ReporteNegocio();
        private readonly Dictionary<string, decimal> _acumulados = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        private int _contadorFilas = 0;

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
                txtDesde.Text = new DateTime(hoy.Year, hoy.Month, 1).ToString("yyyy-MM-dd");
                txtHasta.Text = hoy.ToString("yyyy-MM-dd");

                actualizarEncabezadoReporte();
                limpiarGrillaInicial();
            }
        }

        private void limpiarGrillaInicial()
        {
            gvResultados.DataSource = null;
            gvResultados.DataBind();
            lblCantidadRegistros.Text = "Registros encontrados: 0";
            mostrarMensaje("Seleccioná un tipo de reporte para comenzar.", false);
        }

        protected void ddlReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            actualizarEncabezadoReporte();
            lblMensaje.Visible = false;

            gvResultados.DataSource = null;
            gvResultados.DataBind();
            lblCantidadRegistros.Text = "Registros encontrados: 0";

            if (string.IsNullOrEmpty(ddlReporte.SelectedValue))
                mostrarMensaje("Seleccioná un tipo de reporte para comenzar.", false);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            actualizarEncabezadoReporte();

            if (string.IsNullOrEmpty(ddlReporte.SelectedValue))
            {
                mostrarMensaje("Primero seleccioná un tipo de reporte.", true);
                return;
            }

            cargarReporte();
        }

        private bool requiereRangoFechas()
        {
            return !string.IsNullOrEmpty(ddlReporte.SelectedValue) &&
                   ddlReporte.SelectedValue != "STOCK_BAJO";
        }

        private void actualizarEncabezadoReporte()
        {
            bool usaFechas = requiereRangoFechas();

            divDesde.Attributes["class"] = usaFechas ? "col-md-2 mb-3" : "col-md-2 mb-3 oculto-visual";
            divHasta.Attributes["class"] = usaFechas ? "col-md-2 mb-3" : "col-md-2 mb-3 oculto-visual";

            switch (ddlReporte.SelectedValue)
            {
                case "":
                    lblTituloReporte.Text = "Seleccione reporte";
                    lblDescripcionReporte.Text = "Elegí un tipo de reporte para consultar información y exportar a CSV.";
                    break;
                case "CLIENTES":
                    lblTituloReporte.Text = "Ranking de clientes frecuentes";
                    lblDescripcionReporte.Text = "Consultá los clientes con mayor monto acumulado y cantidad de compras en el período seleccionado.";
                    break;
                case "VENDEDORES":
                    lblTituloReporte.Text = "Ranking de vendedores";
                    lblDescripcionReporte.Text = "Consultá el rendimiento de cada vendedor según las ventas realizadas durante un período determinado.";
                    break;
                case "PRODUCTOS":
                    lblTituloReporte.Text = "Productos más vendidos";
                    lblDescripcionReporte.Text = "Consultá las prendas con mayor cantidad de unidades vendidas y facturación en el período seleccionado.";
                    break;
                case "STOCK_BAJO":
                    lblTituloReporte.Text = "Productos con bajo stock";
                    lblDescripcionReporte.Text = "Listá las prendas cuyo stock actual es menor o igual al stock mínimo para planificar la reposición.";
                    break;
                case "MEDIO_PAGO":
                    lblTituloReporte.Text = "Ventas por Medio de Pago";
                    lblDescripcionReporte.Text = "Consultá la cantidad de ventas, unidades y monto cobrado según cada medio de pago en el período.";
                    break;
                case "BALANCE":
                    lblTituloReporte.Text = "Balance de Ventas vs. Compras";
                    lblDescripcionReporte.Text = "Compará los ingresos por ventas contra los egresos por compras a proveedores y su diferencia por fecha.";
                    break;
            }
        }

        private bool obtenerFechas(out DateTime desde, out DateTime hasta)
        {
            desde = DateTime.MinValue;
            hasta = DateTime.MinValue;

            if (!requiereRangoFechas())
                return true;

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
                mostrarMensaje("La fecha desde no puede ser posterior a la fecha hasta.", true);
                return false;
            }

            return true;
        }

        private DataTable obtenerDatosReporte()
        {
            if (string.IsNullOrEmpty(ddlReporte.SelectedValue))
                return null;

            if (!obtenerFechas(out DateTime desde, out DateTime hasta))
                return null;

            switch (ddlReporte.SelectedValue)
            {
                case "CLIENTES":
                    return _reporteNegocio.obtenerRankingClientes(desde, hasta);
                case "VENDEDORES":
                    return _reporteNegocio.obtenerRankingVendedores(desde, hasta);
                case "PRODUCTOS":
                    return _reporteNegocio.obtenerTopProductos(desde, hasta);
                case "STOCK_BAJO":
                    return _reporteNegocio.obtenerProductosBajoStock();
                case "MEDIO_PAGO":
                    return _reporteNegocio.obtenerVentasPorMedioPago(desde, hasta);
                case "BALANCE":
                    return _reporteNegocio.obtenerBalanceVentasVsCompras(desde, hasta);
                default:
                    return null;
            }
        }

        private void cargarReporte()
        {
            lblMensaje.Visible = false;
            _acumulados.Clear();
            _contadorFilas = 0;

            DataTable tabla = obtenerDatosReporte();
            if (tabla == null)
                return;

            gvResultados.DataSource = tabla;
            gvResultados.DataBind();

            lblCantidadRegistros.Text = "Registros encontrados: " + tabla.Rows.Count;
        }

        protected void gvResultados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CultureInfo culturaAr = CultureInfo.GetCultureInfo("es-AR");

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView fila = e.Row.DataItem as DataRowView;
                if (fila == null)
                    return;

                _contadorFilas++;

                for (int i = 0; i < fila.Row.Table.Columns.Count; i++)
                {
                    string nombreCol = fila.Row.Table.Columns[i].ColumnName;
                    object valor = fila.Row[i];

                    if (valor == null || valor == DBNull.Value)
                        continue;

                    if (valor is DateTime fecha)
                    {
                        e.Row.Cells[i].Text = fecha.ToString("dd/MM/yyyy", culturaAr);
                    }
                    else if (esColumnaMonetaria(nombreCol))
                    {
                        if (decimal.TryParse(valor.ToString(), out decimal monto))
                        {
                            acumularValor(nombreCol, monto);
                            e.Row.Cells[i].Text = monto.ToString("C2", culturaAr);
                        }
                    }
                    else if (esColumnaCantidad(nombreCol))
                    {
                        if (decimal.TryParse(valor.ToString(), out decimal cant))
                        {
                            acumularValor(nombreCol, cant);
                            e.Row.Cells[i].Text = cant.ToString("N0", culturaAr);
                        }
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer && _contadorFilas > 0)
            {
                DataTable tabla = gvResultados.DataSource as DataTable;
                if (tabla == null)
                    return;

                e.Row.Cells[0].Text = "TOTAL";

                for (int i = 1; i < tabla.Columns.Count; i++)
                {
                    string nombreCol = tabla.Columns[i].ColumnName;

                    if (!_acumulados.TryGetValue(nombreCol, out decimal suma))
                        continue;

                    if (esColumnaMonetaria(nombreCol))
                    {
                        e.Row.Cells[i].Text = suma.ToString("C2", culturaAr);
                    }
                    else if (esColumnaCantidad(nombreCol))
                    {
                        e.Row.Cells[i].Text = suma.ToString("N0", culturaAr);
                    }
                }
            }
        }

        private void acumularValor(string nombreCol, decimal valor)
        {
            if (_acumulados.ContainsKey(nombreCol))
                _acumulados[nombreCol] += valor;
            else
                _acumulados[nombreCol] = valor;
        }

        private bool esColumnaMonetaria(string nombreCol)
        {
            return nombreCol.IndexOf("Total", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Ingresos", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Egresos", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Diferencia", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private bool esColumnaCantidad(string nombreCol)
        {
            return nombreCol.IndexOf("Cant.", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Cantidad", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Unidades", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   nombreCol.IndexOf("Stock", StringComparison.OrdinalIgnoreCase) >= 0;
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
            if (string.IsNullOrEmpty(ddlReporte.SelectedValue))
            {
                mostrarMensaje("Primero seleccioná un tipo de reporte para exportar.", true);
                return;
            }

            if (!obtenerFechas(out DateTime desde, out DateTime hasta))
                return;

            DataTable tabla = obtenerDatosReporte();
            if (tabla == null)
                return;

            if (tabla.Rows.Count == 0)
            {
                mostrarMensaje("No hay resultados para exportar.", true);
                return;
            }

            string nombreBase = obtenerNombreArchivoCsv();
            string sufijoArchivo = requiereRangoFechas()
                ? $"{desde:yyyyMMdd}_a_{hasta:yyyyMMdd}"
                : DateTime.Now.ToString("yyyyMMdd");
            string nombreCompleto = $"{nombreBase}_{sufijoArchivo}.csv";

            Dictionary<string, decimal> totalesCsv = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            StringBuilder csv = new StringBuilder();

            csv.AppendLine($"Reporte;{escaparCSV(lblTituloReporte.Text)}");
            if (requiereRangoFechas())
                csv.AppendLine($"Período;{escaparCSV($"{desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}")}");
            csv.AppendLine($"Generado;{escaparCSV(DateTime.Now.ToString("dd/MM/yyyy HH:mm"))}");
            csv.AppendLine();

            for (int c = 0; c < tabla.Columns.Count; c++)
            {
                if (c > 0) csv.Append(";");
                csv.Append(escaparCSV(tabla.Columns[c].ColumnName));
            }
            csv.AppendLine();

            foreach (DataRow fila in tabla.Rows)
            {
                for (int c = 0; c < tabla.Columns.Count; c++)
                {
                    if (c > 0) csv.Append(";");

                    string colName = tabla.Columns[c].ColumnName;
                    object val = fila[c];

                    if (val == null || val == DBNull.Value)
                    {
                        csv.Append("");
                    }
                    else if (val is DateTime dt)
                    {
                        csv.Append(escaparCSV(dt.ToString("dd/MM/yyyy")));
                    }
                    else if (val is decimal || val is int || val is long)
                    {
                        decimal num = Convert.ToDecimal(val, CultureInfo.InvariantCulture);

                        if (esColumnaMonetaria(colName) || esColumnaCantidad(colName))
                        {
                            if (totalesCsv.ContainsKey(colName))
                                totalesCsv[colName] += num;
                            else
                                totalesCsv[colName] = num;
                        }

                        csv.Append(num.ToString("0.00", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        csv.Append(escaparCSV(val.ToString()));
                    }
                }
                csv.AppendLine();
            }

            csv.Append("TOTAL");
            for (int c = 1; c < tabla.Columns.Count; c++)
            {
                csv.Append(";");
                string colName = tabla.Columns[c].ColumnName;

                if (totalesCsv.TryGetValue(colName, out decimal suma))
                    csv.Append(suma.ToString("0.00", CultureInfo.InvariantCulture));
            }
            csv.AppendLine();

            Response.Clear();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = "text/csv";
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader(
                "Content-Disposition",
                $"attachment; filename={nombreCompleto}"
            );

            Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            Response.Write(csv.ToString());
            Response.Flush();
            Response.End();
        }

        private string obtenerNombreArchivoCsv()
        {
            switch (ddlReporte.SelectedValue)
            {
                case "CLIENTES": return "RankingClientes";
                case "VENDEDORES": return "RankingVendedores";
                case "PRODUCTOS": return "ProductosMasVendidos";
                case "STOCK_BAJO": return "ProductosBajoStock";
                case "MEDIO_PAGO": return "VentasPorMedioPago";
                case "BALANCE": return "BalanceVentasVsCompras";
                default: return "Reporte";
            }
        }

        private string escaparCSV(string valor)
        {
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }
    }
}