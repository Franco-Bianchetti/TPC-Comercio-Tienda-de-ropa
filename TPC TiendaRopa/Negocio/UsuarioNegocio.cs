using Dominio;

namespace Negocio {
    public class UsuarioNegocio {
        public Usuario login(
            string usuario,
            string clave) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT
                        IdUsuario,
                        Usuario,
                        PasswordHash,
                        IdRol
                    FROM Usuarios
                    WHERE Usuario = @Usuario
                    AND PasswordHash = @Clave
                    AND Activo = 1");

                datos.setParametro(
                    "@Usuario",
                    usuario);

                datos.setParametro(
                    "@Clave",
                    clave);

                datos.ejecutarLectura();

                if (datos.Lector.Read()) {
                    Usuario aux =
                        new Usuario();

                    aux.Id =
                        (int)datos.Lector["IdUsuario"];

                    aux.NombreUsuario =
                        datos.Lector["Usuario"].ToString();

                    aux.Clave =
                        datos.Lector["PasswordHash"].ToString();

                    aux.Perfil =
                        ((int)datos.Lector["IdRol"] == 1)
                        ? TipoPerfil.Administrador
                        : TipoPerfil.Vendedor;

                    return aux;
                }

                return null;
            }
            finally {
                datos.cerrarConexion();
            }
        }
    }
}