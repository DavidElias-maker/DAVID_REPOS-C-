using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.TransportistaServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/Transportista")]
    public class TransportistaController : ControllerBase
    {
        private readonly ITransportistaService _TransportistaService;
        public TransportistaController(ITransportistaService TransportistaService)
        {
            this._TransportistaService = TransportistaService;


        }
        [HttpPost]
        public async Task<ActionResult<Transportista>> PostTransportista(TransportistaDto transportistaDTO)
        { 
            var IngresarTransportista = await _TransportistaService.PostTransportista(transportistaDTO);

            return IngresarTransportista;

        }
        [HttpGet]
        public async Task<List<TransportistaDto>> GetTransportista()
        {

            var BuscarTransportista = await _TransportistaService.GetTransportista();

            return BuscarTransportista;

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTransportista(TransportistaDeleteDto transportistaDeleteDTO)
        {

            var BorrarTransportistas = await _TransportistaService.DeleteTransportista(transportistaDeleteDTO);

            return BorrarTransportistas;

        }

        [HttpPut]
        public async Task<ActionResult> UpdateTransportista(TransportistaDto transportistaDTO)
        {
            var ActualizarTransportistas = await _TransportistaService.UpdateTransportista(transportistaDTO);

            return ActualizarTransportistas;

        }

        [HttpGet("TransportistaViaje")]
        public async Task<List<TransportistaViajeDto>> GetTransportistaViaje()
        {

            var BuscarTransportistaViaje = await _TransportistaService.GetTransportistaViaje();

            return BuscarTransportistaViaje;

        }
    }
}
