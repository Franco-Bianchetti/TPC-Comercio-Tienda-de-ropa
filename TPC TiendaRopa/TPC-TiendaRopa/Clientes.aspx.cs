using System;
using Negocio;

namespace TPC_TiendaRopa
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClienteNegocio negocio = new ClienteNegocio();

                gvClientes.DataSource = negocio.listar();
                gvClientes.DataBind();
            }
        }
    }
}