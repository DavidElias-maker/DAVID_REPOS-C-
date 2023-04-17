using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.TransportistaServices
{
    public interface ITransportistaService
    {
        public Task<ActionResult<TransportistaDto>> GetTransportista(string PrimerNombre);

        public Task<ActionResult<Transportista>> PostTransportista(TransportistaDto transportistaDTO);

        public Task<ActionResult> UpdateTransportista(TransportistaDto TransportistaDTO);
        public Task<ActionResult> DeleteTransportista(TransportistaDeleteDto transportistaDeleteDTO);
    }
}
