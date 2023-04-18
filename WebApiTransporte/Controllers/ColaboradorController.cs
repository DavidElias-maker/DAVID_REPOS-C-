using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Runtime.CompilerServices;
using System.Web.Http.Description;
using WebApiTransporte.Dtos;
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
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public ColaboradorController(IColaboradorService ColaboradorService, ApplicationDbContext context, IMapper mapper)
        {
            this._ColaboradorService = ColaboradorService;
            this._context = context;
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
            return Ok("ingresado de manera exitosa");
           
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

    }

}