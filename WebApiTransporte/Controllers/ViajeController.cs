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

        public async Task<ActionResult<Viaje_Detalle>> PostAñadirColaboradorViaje(int sucursal, int colaborador, int transportista, Viaje_Detalle request)
        {


            var sucursalcolaboradorid = (from f in _context.sucursal_colaborador
                                         where f.SucursalId == sucursal && f.ColaboradorId == colaborador
                                         orderby f.Id
                                         select f.Id).FirstOrDefault();


            request.SucursalColaboradoresId = sucursalcolaboradorid;
            request.ViajeId = _context.viaje.OrderByDescending(p => p.Id).FirstOrDefault().Id;






            //var colaboradorsucur = await _context.viaje_detalle.Where(x => x.SucursalColaboradoresId == sucursalcolaboradorid && x.ViajeId == request.Id).ToListAsync();
            //if (colaboradorsucur != null)
            //{
            //    return BadRequest("este colaborador ya existe en este viaje");
            //}

            _context.Add(request);
            await _context.SaveChangesAsync();

            return request;




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
        [HttpPost("Calcularviaje")]
        public async Task<ActionResult<Viaje>> PostCalcularViaje(Viaje viaje_detalle, int sucursalcolaboradorid, int transportista)
        {



            var DistanciaKm = (from f in _context.sucursal_colaborador
                               where f.Id == sucursalcolaboradorid
                               orderby f.Id
                               select f.DistanciaKm).FirstOrDefault();


            var Tarifa = (from f in _context.transportista
                          where f.Id == transportista
                          orderby f.Id
                          select f.Tarifa).FirstOrDefault();

            var total = Convert.ToDecimal(DistanciaKm) * Convert.ToDecimal(Tarifa);



            var UltimoValor = _context.viaje 
                .OrderByDescending(x => x.Id)
                .Select(x => x.Total)
                    .FirstOrDefault();


            var newValue = UltimoValor + total;


            var rowToUpdate = _context.viaje.OrderBy(x => x.Id).LastOrDefault();
            rowToUpdate.Total = newValue;
            _context.SaveChanges();

            return Ok(rowToUpdate);





        }
        [HttpGet]

        public async Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid)

        {
            var obtenertransportistapago = await _context.viaje
                          .Where(v => v.TransportistaId == transportistaid && v.Fecha >= fechaInicial && v.Fecha <= fechafinal)
                          .SumAsync(v => v.Total);

            return Ok(obtenertransportistapago);

        }
    }
}