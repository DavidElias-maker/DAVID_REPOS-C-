using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos.TransportistaDtos
{
    public class TransportistaDeleteDto
    {
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
    }
}
