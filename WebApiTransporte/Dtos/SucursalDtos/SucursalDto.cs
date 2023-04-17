using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class SucursalDto
    {
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        
    }
}
