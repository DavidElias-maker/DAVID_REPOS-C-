using WebApiTransporte.Validations;

namespace WebApiTransporte.Dtos
{
    public class ColaboradorDto
    {
       
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerApellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

       
    }
}
