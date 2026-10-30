using System.Collections.Generic;
using System.Web.Mvc;
using Dominio;

namespace Tienda_Ropa.Controllers
{
    public class MarcasController : Controller
    {
        public ActionResult Index()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            List<Marca> lista = negocio.listar();

            return View(lista);
        }
        public ActionResult Nuevo()
        {
            return View();
        }
    }
}