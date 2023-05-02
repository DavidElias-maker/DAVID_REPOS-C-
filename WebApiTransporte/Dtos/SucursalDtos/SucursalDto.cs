using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class SucursalDto
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        
    }
}
