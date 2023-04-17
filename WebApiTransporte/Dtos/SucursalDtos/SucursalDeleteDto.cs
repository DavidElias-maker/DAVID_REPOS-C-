using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class SucursalDeleteDto
    {
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
