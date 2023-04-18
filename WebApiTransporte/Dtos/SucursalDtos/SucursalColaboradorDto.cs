using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos.SucursalDtos
{
    public class SucursalColaboradorDto
    {
        public int Id { get; set; }
        
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
