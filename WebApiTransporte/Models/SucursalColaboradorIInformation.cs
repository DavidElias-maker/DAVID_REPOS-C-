﻿namespace WebApiTransporte.Models
{
    public class SucursalColaboradorIInformation
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCompleto { get; set; }
        public decimal DistanciaKm { get; set; }
        public int colaboradorId { get; set; }
        public int sucursalId { get; set; }
        
       


    }
}
