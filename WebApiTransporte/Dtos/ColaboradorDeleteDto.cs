using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class ColaboradorDeleteDto
    {
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerApellido { get; set; }
    }
}
