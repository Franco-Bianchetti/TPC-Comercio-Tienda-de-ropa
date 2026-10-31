using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class TipoNegocio
    {
        public List<Tipo> listar()
        {
            List<Tipo> lista = new List<Tipo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(
                    "SELECT IdTipo, Nombre FROM Tipos ORDER BY IdTipo");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Tipo aux = new Tipo();

                    aux.Id = Convert.ToInt32(datos.Lector["IdTipo"]);
                    aux.Nombre = datos.Lector["Nombre"].ToString().Trim();

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}