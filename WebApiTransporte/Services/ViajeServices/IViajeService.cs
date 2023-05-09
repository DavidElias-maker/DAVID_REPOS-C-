using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.ViajeServices
{
    public interface IViajeService
    {
        public Task<ActionResult<ViajeInsert>> PostCicloFor(int transportistaId, [FromBody] List<ViajeInsert> viajeinsert);
        public Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid);

    }
}
