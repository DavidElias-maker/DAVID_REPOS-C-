using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos.TransportistaDtos
{
    public class TransportistaDto
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public decimal Tarifa { get; set; }
    }
}
