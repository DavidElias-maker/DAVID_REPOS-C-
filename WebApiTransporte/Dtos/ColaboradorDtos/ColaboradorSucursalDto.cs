using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos.ColaboradorDtos
{
    public class ColaboradorSucursalDto
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerApellido { get; set; }
    }
}
