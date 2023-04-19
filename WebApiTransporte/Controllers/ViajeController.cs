using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/Viaje")]
    public class ViajeController : ControllerBase
    {
        private readonly IColaboradorService _ColaboradorService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public ViajeController(IColaboradorService ColaboradorService, ApplicationDbContext context, IMapper mapper)
        {
            this._ColaboradorService = ColaboradorService;
            this._context = context;
            this._mapper = mapper;


        }

        [HttpPost]
        [Route("api/Viaje/{SucursalId}/{ColaboradorId}")]
        public async Task<ActionResult<Sucursal_Colaborador>> PostViaje(int colaboradorid , int sucursalid)
        {
            var SucursalExistente = await _context.sucursal_colaborador.FirstOrDefaultAsync(x => x.ColaboradorId == colaboradorid && x.SucursalId == sucursalid);

            return SucursalExistente;
        }

    }
}
