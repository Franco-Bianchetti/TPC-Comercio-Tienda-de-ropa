namespace TPC_TiendaRopa
{
    public static class Validaciones
    {
        public static bool ContieneNumero(string texto)
        {
            foreach (char c in texto)
            {
                if (char.IsDigit(c))
                    return true;
            }

            return false;
        }

        public static bool SoloLetras(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            bool tieneLetra = false;

            foreach (char c in texto)
            {
                if (char.IsLetter(c))
                    tieneLetra = true;
                else if (c != ' ' && c != '-' && c != '\'')
                    return false;
            }

            return tieneLetra;
        }

        public static bool UsuarioValido(string usuario)
        {
            bool tieneLetra = false;

            foreach (char c in usuario)
            {
                if (char.IsLetter(c))
                    tieneLetra = true;
                else if (!char.IsDigit(c))
                    return false;
            }

            return tieneLetra;
        }

        public static bool TodosIguales(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            char primero = valor[0];

            foreach (char c in valor)
            {
                if (c != primero)
                    return false;
            }

            return true;
        }
    }
}