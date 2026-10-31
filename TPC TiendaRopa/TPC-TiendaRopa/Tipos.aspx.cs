using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Tipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TipoNegocio negocio = new TipoNegocio();

                gvTipos.DataSource = negocio.listar();
                gvTipos.DataBind();
            }
        }
    }
}