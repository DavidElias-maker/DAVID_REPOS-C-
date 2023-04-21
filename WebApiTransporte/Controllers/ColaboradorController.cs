using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Runtime.CompilerServices;
using System.Web.Http.Description;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.SucursalServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/Colaborador")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _ColaboradorService;
        



        public ColaboradorController(IColaboradorService ColaboradorService)
        {
            this._ColaboradorService = ColaboradorService;
            


        }

        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(ColaboradorDto colaboradorDTO)
        {
            var IngresarColaboradores = await _ColaboradorService.PostColaborador(colaboradorDTO);

            return IngresarColaboradores;
           
        }

        [HttpGet]
        public async Task<List<ColaboradorDto>> GetColaborador()
        {

            var BuscarColaboradores = await _ColaboradorService.GetColaborador();

            return BuscarColaboradores;

        }


        [HttpPut]
        public async Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO)
        {
            var ActualizarColaboradores = await _ColaboradorService.UpdateColaborador(colaboradorDTO);

            return ActualizarColaboradores;

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteColaborador(ColaboradorDeleteDto colaboradorDeleteDTO)
        {

            var BorrarColaboradores = await _ColaboradorService.DeleteColaborador(colaboradorDeleteDTO);

            return BorrarColaboradores;

        }

        [HttpGet("ColaboradorSucursal")]
        public async Task<List<ColaboradorSucursalDto>> GetColaboradorSucursal()
        {

            var BuscarColaboradoresSucursales = await _ColaboradorService.GetColaboradorSucursal();

            return BuscarColaboradoresSucursales;

        }

    }

}