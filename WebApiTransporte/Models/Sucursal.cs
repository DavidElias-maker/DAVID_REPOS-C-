using WebApiTransporte.Validations;

namespace WebApiTransporte.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Activo { get; set; } = "si";
    }
}
