using WebApiTransporte.Dtos.ViajeDtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Dtos
{
    public class ViajeInformationDto
    {
        public ViajeDto viajedto { get; set; }
        public Viaje_DetalleDto viaje_detalledto { get; set; }

    }
}
