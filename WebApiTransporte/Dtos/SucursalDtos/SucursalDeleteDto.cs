using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class SucursalDeleteDto
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
