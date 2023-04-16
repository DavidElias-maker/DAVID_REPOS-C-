using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Runtime.CompilerServices;
using System.Web.Http.Description;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.SucursalServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/Transporte")]
    public class ColaboradorController : ControllerBase
    {
        private readonly IColaboradorService _ColaboradorService;
        private readonly IMapper _mapper;





        public ColaboradorController(IColaboradorService SucursalService, IMapper mapper)
        {
            this._ColaboradorService = SucursalService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostColaborador(ColaboradorDto colaboradorDTO)
        {
            var IngresarColaboradores = await _ColaboradorService.PostColaborador(colaboradorDTO);
            if (IngresarColaboradores is null)
            {
                return BadRequest($"Ya existe un colaborador con el mismo nombre");
            }
            return Ok();
           
        }
        [HttpGet]
        [Route("api/Transporte/{PrimerNombre}/{PrimerApellido}")]
        public async Task<ActionResult<ColaboradorDto>> GetColaborador(string PrimerNombre, string PrimerApellido)
        {
            var ObtenerColaboradores = await _ColaboradorService.GetColaborador(PrimerNombre, PrimerApellido);

            return ObtenerColaboradores;


        }


    }

}