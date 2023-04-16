using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiTransporte.Validations;

namespace WebApiTransporte.Models
{
    public class Colaborador
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerNombre { get; set; }
        [PrimeraLetraMayuscula]
        public string PrimerApellido { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Activo { get; set; }
    }
}
