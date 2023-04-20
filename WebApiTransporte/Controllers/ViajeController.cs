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
        public async Task<ActionResult<Viaje>> PostViaje(Viaje viaje)
        {




            //var DistanciaKm = (from f in _context.viaje
            //                   where f.ColaboradorId == viaje.Id && f.SucursalId == viaje.SucursalId
            //                   select f.DistanciaKm).FirstOrDefault();

            var sucursalcolaboradorid = (from f in _context.sucursal_colaborador
                                         where f.SucursalId == viaje.SucursalId
                                         select f.Id).FirstOrDefault();


            var Tarifa = (from f in _context.transportista
                          where f.Id == viaje.TransportistaId
                          select f.Tarifa).FirstOrDefault();

            var sucursalcolaborador = await _context.sucursal_colaborador
         .FirstOrDefaultAsync(f => f.ColaboradorId == viaje.Id && f.SucursalId == viaje.SucursalId);

            if (sucursalcolaborador != null)
            {
                var tableData = await _context.viaje
                    .Where(x => x.Fecha == viaje.Fecha)
                    .ToListAsync();

                var totalSum = tableData.Sum(x => x.Total);

                return Ok(new { TotalSum = totalSum });
            }
            else
            {
                return BadRequest("No matching SucursalColaborador found.");
            }


            //if (existecolaboradorsucursal)
            //{
            //    return BadRequest("La sucursal ya existe");
            //}
            //else
            //{

            //}



            // decimal totalcre = Convert.ToDecimal(DistanciaKm) * Convert.ToDecimal(Tarifa);


            //_context.Add(totalSum);
            //await _context.SaveChangesAsync();

            
           
        }
       
   



        [HttpPost("ViajeNuevo")]
        public async Task<ActionResult<Viaje>> PostNuevoViaje(Viaje viaje)
        {

            try
            {
                viaje.Total = 0;
                _context.Add(viaje);
                await _context.SaveChangesAsync();
                return viaje;

                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}