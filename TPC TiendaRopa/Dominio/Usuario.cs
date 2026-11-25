namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string NombreUsuario { get; set; }

        public string Clave { get; set; }

        public TipoPerfil Perfil { get; set; }

        public bool Activo { get; set; }

        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + Apellido;
            }
        }

        public string CodigoNombre
        {
            get
            {
                return Codigo + " - " + Nombre + " " + Apellido;
            }
        }
    }
}