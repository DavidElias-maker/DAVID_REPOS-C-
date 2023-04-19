using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/ViajeDetalle")]
    public class ViajeDetalleController : ControllerBase
    {
        private readonly IColaboradorService _ColaboradorService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public ViajeDetalleController(IColaboradorService ColaboradorService, ApplicationDbContext context, IMapper mapper)
        {
            this._ColaboradorService = ColaboradorService;
            this._context = context;
            this._mapper = mapper;


        }
        [HttpPost]
        public async Task<ActionResult<Viaje_Detalle>> PostViajeDetalle(Viaje_Detalle viaje_detalle)
        {
            var ID = _context.viaje_detalle.MaxAsync(x => x.Id);

              viaje_detalle.Id = Convert.ToInt32(ID) + 1;

            _context.Add(viaje_detalle);
            await _context.SaveChangesAsync();
            return Ok();

            
        }
    }
}