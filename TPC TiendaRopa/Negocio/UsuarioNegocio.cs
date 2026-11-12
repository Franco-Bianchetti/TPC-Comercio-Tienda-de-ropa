using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio {
    public class UsuarioNegocio {
        public Usuario login(
            string usuario,
            string clave) {
            AccesoDatos datos = new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT
                        IdUsuario,
                        Nombre,
                        Apellido,
                        Dni,
                        Usuario,
                        PasswordHash,
                        IdRol
                    FROM Usuarios
                    WHERE Usuario = @Usuario
                    AND PasswordHash = @Clave
                    AND Activo = 1");

                datos.setParametro("@Usuario", usuario);
                datos.setParametro("@Clave", clave);

                datos.ejecutarLectura();

                if (datos.Lector.Read()) {
                    Usuario aux = new Usuario();

                    aux.Id = (int)datos.Lector["IdUsuario"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Apellido =
                        datos.Lector["Apellido"].ToString();

                    aux.Dni =
                        datos.Lector["Dni"].ToString();

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

        public List<Usuario> listar() {
            List<Usuario> lista =
                new List<Usuario>();

            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT
                        IdUsuario,
                        Nombre,
                        Apellido,
                        Dni,
                        Usuario,
                        PasswordHash,
                        IdRol
                    FROM Usuarios
                    WHERE Activo = 1
                    ORDER BY IdUsuario");

                datos.ejecutarLectura();

                while (datos.Lector.Read()) {
                    Usuario aux =
                        new Usuario();

                    aux.Id =
                        (int)datos.Lector["IdUsuario"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Apellido =
                        datos.Lector["Apellido"].ToString();

                    aux.Dni =
                        datos.Lector["Dni"].ToString();

                    aux.NombreUsuario =
                        datos.Lector["Usuario"].ToString();

                    aux.Clave =
                        datos.Lector["PasswordHash"].ToString();

                    aux.Perfil =
                        ((int)datos.Lector["IdRol"] == 1)
                        ? TipoPerfil.Administrador
                        : TipoPerfil.Vendedor;

                    lista.Add(aux);
                }

                return lista;
            }
            finally {
                datos.cerrarConexion();
            }
        }

        public Usuario buscar(int id) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT
                        IdUsuario,
                        Nombre,
                        Apellido,
                        Dni,
                        Usuario,
                        PasswordHash,
                        IdRol
                    FROM Usuarios
                    WHERE IdUsuario = @Id
                    AND Activo = 1");

                datos.setParametro("@Id", id);

                datos.ejecutarLectura();

                if (datos.Lector.Read()) {
                    Usuario aux =
                        new Usuario();

                    aux.Id =
                        (int)datos.Lector["IdUsuario"];

                    aux.Nombre =
                        datos.Lector["Nombre"].ToString();

                    aux.Apellido =
                        datos.Lector["Apellido"].ToString();

                    aux.Dni =
                        datos.Lector["Dni"].ToString();

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

        public bool existeUsuario(
            string usuario,
            int idActual) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT IdUsuario
                    FROM Usuarios
                    WHERE Usuario = @Usuario
                    AND Activo = 1
                    AND IdUsuario <> @Id");

                datos.setParametro("@Usuario", usuario);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally {
                datos.cerrarConexion();
            }
        }

        public bool existeDni(
            string dni,
            int idActual) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    SELECT IdUsuario
                    FROM Usuarios
                    WHERE Dni = @Dni
                    AND Activo = 1
                    AND IdUsuario <> @Id");

                datos.setParametro("@Dni", dni);
                datos.setParametro("@Id", idActual);

                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            finally {
                datos.cerrarConexion();
            }
        }

        public void agregar(
            Usuario nuevo) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    INSERT INTO Usuarios
                    (
                        Nombre,
                        Apellido,
                        Dni,
                        Usuario,
                        PasswordHash,
                        IdRol,
                        Activo
                    )
                    VALUES
                    (
                        @Nombre,
                        @Apellido,
                        @Dni,
                        @Usuario,
                        @Password,
                        @IdRol,
                        1
                    )");

                datos.setParametro("@Nombre", nuevo.Nombre);
                datos.setParametro("@Apellido", nuevo.Apellido);
                datos.setParametro("@Dni", nuevo.Dni);
                datos.setParametro("@Usuario", nuevo.NombreUsuario);
                datos.setParametro("@Password", nuevo.Clave);

                datos.setParametro(
                    "@IdRol",
                    nuevo.Perfil ==
                    TipoPerfil.Administrador
                    ? 1
                    : 2);

                datos.ejecutarAccion();
            }
            finally {
                datos.cerrarConexion();
            }
        }

        public void modificar(
            Usuario usuario) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    UPDATE Usuarios
                    SET
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Dni = @Dni,
                        Usuario = @Usuario,
                        PasswordHash = @Password,
                        IdRol = @IdRol
                    WHERE IdUsuario = @Id");

                datos.setParametro("@Nombre", usuario.Nombre);
                datos.setParametro("@Apellido", usuario.Apellido);
                datos.setParametro("@Dni", usuario.Dni);
                datos.setParametro("@Usuario", usuario.NombreUsuario);
                datos.setParametro("@Password", usuario.Clave);

                datos.setParametro(
                    "@IdRol",
                    usuario.Perfil ==
                    TipoPerfil.Administrador
                    ? 1
                    : 2);

                datos.setParametro("@Id", usuario.Id);

                datos.ejecutarAccion();
            }
            finally {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id) {
            AccesoDatos datos =
                new AccesoDatos();

            try {
                datos.setConsulta(@"
                    UPDATE Usuarios
                    SET Activo = 0
                    WHERE IdUsuario = @Id");

                datos.setParametro("@Id", id);

                datos.ejecutarAccion();
            }
            finally {
                datos.cerrarConexion();
            }
        }
    }
}