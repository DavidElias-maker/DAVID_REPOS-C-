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
        private readonly ApplicationDbContext _context;


        public ColaboradorController(IColaboradorService SucursalService, IMapper mapper, ApplicationDbContext context)
        {
            this._ColaboradorService = SucursalService;
            this._mapper = mapper;
            this._context = context;
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

    }

}