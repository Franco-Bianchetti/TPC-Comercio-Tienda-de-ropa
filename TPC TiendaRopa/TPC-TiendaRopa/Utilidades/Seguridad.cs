using System.Web;
using Dominio;

namespace TPC_TiendaRopa
{
    public static class Seguridad
    {
        public static bool EsAdmin()
        {
            object perfil = HttpContext.Current.Session["Perfil"];

            return perfil is TipoPerfil &&
                   (TipoPerfil)perfil == TipoPerfil.Administrador;
        }
    }
}