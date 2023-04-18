using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.TransportistaDtos;
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
        public async Task<ActionResult> PostTransportista(TransportistaDto transportistaDTO)
        {
            var IngresarTransportistas = await _TransportistaService.PostTransportista(transportistaDTO);
            if (IngresarTransportistas is null)
            {
                return BadRequest($"Ya existe un transportista con el mismo nombre");
            }
            return Ok("ingresado de manera exitosa");

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
    }
}
