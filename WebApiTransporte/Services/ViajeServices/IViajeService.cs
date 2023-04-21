using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.ViajeServices
{
    public interface IViajeService
    {
        public Task<ActionResult<Viaje_Detalle>> PostAñadirColaboradorViaje(int sucursal, int colaborador, Viaje_Detalle request);
        public Task<ActionResult<Viaje>> PostNuevoViaje(Viaje viaje);
        public ActionResult<Viaje> PostCalcularViaje(Viaje viaje_detalle, int sucursalcolaboradorid, int transportista);
        public Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid);

    }
}
