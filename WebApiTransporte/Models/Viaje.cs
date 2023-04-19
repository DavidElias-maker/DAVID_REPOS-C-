namespace WebApiTransporte.Models
{
    public class Viaje
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public int TransportistaId { get; set; }
        public int SucursalId { get; set; }
    }
}
