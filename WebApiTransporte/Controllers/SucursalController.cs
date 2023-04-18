using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.SucursalServices;

namespace WebApiTransporte.Controllers
{

    [ApiController]
    [Route("api/Sucursal")]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalService _SucursalService;

        public SucursalController(ISucursalService SucursalService)
        {
            this._SucursalService = SucursalService;

        }
        [HttpPost]
        public async Task<ActionResult<Sucursal>> PostSucursal(SucursalDto sucursalDTO)
        {
            var IngresarSucursales = await _SucursalService.PostSucursal(sucursalDTO);

            return IngresarSucursales;

        }
        [HttpGet]
        public async Task<List<SucursalDto>> GetSucursal()
        {

            var BuscarSucursales = await _SucursalService.GetSucursal();

            return BuscarSucursales;

        }

        

        [HttpDelete]
        public async Task<ActionResult> DeleteSucursal(SucursalDeleteDto sucursalDeleteDTO)
        {

            var BorrarSucursales = await _SucursalService.DeleteSucursal(sucursalDeleteDTO);

            return BorrarSucursales;

        }

        [HttpPut]
        public async Task<ActionResult> UpdateSucursal(SucursalDto sucursalDTO)
        {
            var ActualizarSucursales = await _SucursalService.UpdateSucursal(sucursalDTO);

            return ActualizarSucursales;

        }

        [HttpGet("SucursalColaborador")]
        public async Task<List<SucursalColaboradorDto>> GetSucursalColaborador()
        {

            var BuscarSucursalesColaboradores = await _SucursalService.GetSucursalColaborador();

            return BuscarSucursalesColaboradores;

        }

    }
}
