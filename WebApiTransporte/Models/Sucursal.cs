namespace WebApiTransporte.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Activo { get; set; } = "si";
    }
}
