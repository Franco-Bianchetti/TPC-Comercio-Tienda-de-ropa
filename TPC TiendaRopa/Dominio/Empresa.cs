using System;

namespace Dominio
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }

        public string RazonSocial { get; set; }

        public string CUIT { get; set; }

        public string IngresosBrutos { get; set; }

        public string Direccion { get; set; }

        public string CondicionIVA { get; set; }

        public string PuntoVenta { get; set; }

        public DateTime FechaInicioActividad { get; set; }

        public string Ciudad { get; set; }
    }
}