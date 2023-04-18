using WebApiTransporte.Validations;

namespace WebApiTransporte.Models
{
    public class Sucursal_Colaborador
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int SucursalId { get; set; }
        public decimal DistanciaKm { get; set; }
        public Boolean Activo { get; set; } = true;
    }
}
