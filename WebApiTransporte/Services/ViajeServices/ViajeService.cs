using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Error;
using WebApiTransporte.Models;
using WebApiTransporte.Services.TransportistaServices;

namespace WebApiTransporte.Services.ViajeServices
{
    public class ViajeService : ControllerBase, IViajeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ViajeService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid)
        {
            try
            {
                var obtenertransportistapago = await _context.viaje
                              .Where(v => v.TransportistaId == transportistaid && v.Fecha >= fechaInicial && v.Fecha <= fechafinal)
                              .SumAsync(v => v.Total);

                return Ok(obtenertransportistapago);
            }
            catch
            {
                return NotFound(ViajeErrorMessages.SPUEC);
            }
        }

        public async Task<ActionResult<ViajeInsert>> PostCicloFor(int transportistaId, [FromBody] List<ViajeInsert> viajeinsert)
        {
            Viaje viaje = new Viaje();
            viaje.TransportistaId = transportistaId;
            viaje.Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var viajes = _mapper.Map<Viaje>(viaje);
            _context.viaje.Add(viajes);
            await _context.SaveChangesAsync();

            decimal total = 0;
            foreach (var element in viajeinsert)
            {
                Viaje_Detalle viaje_detalle = new Viaje_Detalle();
                viaje_detalle.ViajeId = viaje.Id;
                viaje_detalle.SucursalColaboradoresId = element.SucursalColaboradoresId;
                var viajes_detalles = _mapper.Map<Viaje_Detalle>(viaje_detalle);
                _context.viaje_detalle.Add(viajes_detalles);

                var DistanciaKm = (from f in _context.sucursal_colaborador
                                   where f.Id == viaje_detalle.SucursalColaboradoresId
                                   orderby f.Id
                                   select f.DistanciaKm).FirstOrDefault();

                var Tarifa = (from f in _context.transportista
                              where f.Id == viaje.TransportistaId
                              orderby f.Id
                              select f.Tarifa).FirstOrDefault();

                var sucursal = (from f in _context.sucursal_colaborador
                                where f.Id == viaje_detalle.SucursalColaboradoresId
                                orderby f.Id
                                select f.SucursalId).FirstOrDefault();

                total += DistanciaKm * Tarifa;
                viaje.SucursalId = sucursal;
            }
            viaje.Total = total;


            await _context.SaveChangesAsync();

            return Ok(total);
        }

    }

}

