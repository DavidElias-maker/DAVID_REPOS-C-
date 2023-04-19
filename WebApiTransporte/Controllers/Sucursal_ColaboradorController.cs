using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalColaboradorDtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.Sucursal_ColaboradorServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/SucursalColaborador")]
    public class Sucursal_ColaboradorController : ControllerBase
    {
        private readonly ISucursal_ColaboradorService _Sucursal_ColaboradorService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public Sucursal_ColaboradorController(ISucursal_ColaboradorService sucursal_colaboradorService, ApplicationDbContext context, IMapper mapper)
        {
            this._Sucursal_ColaboradorService = sucursal_colaboradorService;
            this._context = context;
            this._mapper = mapper;


        }

        [HttpGet]
        public async Task<List<SucursalColaboradorIInformation>> GetSucursalColaborador()
        {
            var BuscarSucursales_Colaboradores = await _Sucursal_ColaboradorService.GetSucursalColaborador();

            return BuscarSucursales_Colaboradores;
        }

        [HttpPost]
        public async Task<ActionResult<Sucursal_Colaborador>> PostSucursalColaborador(Sucursal_ColaboradorDto SucursalColaboradorDTO)
        {
             var IngresarSucursalColaborador = await _Sucursal_ColaboradorService.PostSucursalColaborador(SucursalColaboradorDTO);

             return IngresarSucursalColaborador;

           

        }



    }
}
