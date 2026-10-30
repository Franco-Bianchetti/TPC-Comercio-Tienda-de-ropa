using System.Collections.Generic;
using System.Web.Mvc;
using Dominio;

namespace Tienda_Ropa.Controllers
{
    public class ClientesController : Controller
    {
        public ActionResult Index()
        {
            ClienteNegocio negocio = new ClienteNegocio();

            List<Cliente> lista = negocio.listar();

            return View(lista);
        }
    }
}