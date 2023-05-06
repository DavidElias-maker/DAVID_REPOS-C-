using AutoMapper;
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

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISucursalService _SucursalService;

        public SucursalController(ISucursalService SucursalService, ApplicationDbContext context, IMapper mapper)
        {
            this._SucursalService = SucursalService;
            this._context = context;
            this._mapper = mapper;

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
        [HttpGet("SucursalColaboradorById")]
        public async Task<List<SucursalGetColaboradores>> GetSucursalColaboradorById(int sucursalid)
        {


            var collist = await (from p in _context.sucursal_colaborador
                                 join ps in _context.sucursal_colaborador on p.Id equals ps.Id
                                 join pm in _context.sucursal on p.SucursalId equals pm.Id
                                 join pd in _context.colaborador on p.ColaboradorId equals pd.Id
                                 where p.SucursalId == sucursalid
                                 select new SucursalGetColaboradores()
                                 {

                                     Id = ps.Id,
                                     Nombre = pm.Nombre,
                                     NombreCompleto = pd.PrimerNombre + " " + pd.PrimerApellido,
                                     DistanciaKm = ps.DistanciaKm

                                 }).ToListAsync();

            return collist;

        }
       

    }
}
