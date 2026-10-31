using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MarcaNegocio negocio = new MarcaNegocio();

                gvMarcas.DataSource = negocio.listar();
                gvMarcas.DataBind();
            }
        }
    }
}