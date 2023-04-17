using WebApiTransporte.Validations;

namespace WebApiTransporte.Models
{
    public class Transportista
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Activo { get; set; } = "si";
        public decimal Tarifa { get; set; }
    }
}
