using System.Web;
using System.Web.UI.WebControls;

namespace TPC_TiendaRopa
{
    public static class Filtros
    {
        public static bool Contiene(string campo, string texto)
        {
            return campo != null &&
                   campo.IndexOf(texto, System.StringComparison.OrdinalIgnoreCase) >= 0;
        }
        public static void MostrarResultado(
            Label etiqueta,
            int cantidad,
            bool hayFiltro,
            string textoBuscado = "")
        {
            if (!hayFiltro)
            {
                etiqueta.Visible = false;
                return;
            }

            etiqueta.Visible = true;

            if (cantidad == 0)
            {
                etiqueta.CssClass = "d-block mb-3 text-danger";

                etiqueta.Text = (textoBuscado == "")
                    ? "No se encontraron resultados."
                    : "No se encontraron resultados para \"" +
                      HttpUtility.HtmlEncode(textoBuscado) + "\".";
            }
            else
            {
                etiqueta.CssClass = "d-block mb-3 text-success";

                etiqueta.Text = (cantidad == 1)
                    ? "Se encontró 1 resultado."
                    : "Se encontraron " + cantidad + " resultados.";
            }
        }
    }
}