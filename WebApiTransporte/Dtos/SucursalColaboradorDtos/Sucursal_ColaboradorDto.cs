namespace WebApiTransporte.Dtos.SucursalColaboradorDtos
{
    public class Sucursal_ColaboradorDto
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int SucursalId { get; set; }
        public decimal DistanciaKm { get; set; }
    }
}
