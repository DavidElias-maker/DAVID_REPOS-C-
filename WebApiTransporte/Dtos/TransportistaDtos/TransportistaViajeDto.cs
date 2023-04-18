using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos.TransportistaDtos
{
    public class TransportistaViajeDto
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
    }
}
