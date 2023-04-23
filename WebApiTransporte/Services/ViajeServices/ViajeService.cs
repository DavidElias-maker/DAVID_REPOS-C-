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

        public async Task<ActionResult<Viaje_Detalle>> PostAñadirColaboradorViaje(int sucursal, int colaborador, Viaje_Detalle request)
        {
            try
            {
                var sucursalcolaboradorid = (from f in _context.sucursal_colaborador
                                             where f.SucursalId == sucursal && f.ColaboradorId == colaborador
                                             orderby f.Id
                                             select f.Id).FirstOrDefault();


                request.SucursalColaboradoresId = sucursalcolaboradorid;
                request.ViajeId = _context.viaje.OrderByDescending(p => p.Id).FirstOrDefault().Id;



                _context.Add(request);
                await _context.SaveChangesAsync();

                return request;
            }
            catch
            {
                return NotFound(ViajeErrorMessages.SPUEC);
            }
        }

        public ActionResult<Viaje> PostCalcularViaje(Viaje viaje_detalle, int sucursalcolaboradorid, int transportista)
        {
            try
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


                var NuevoValor = UltimoValor + total;


                var CampoActualizar = _context.viaje.OrderBy(x => x.Id).LastOrDefault();
                CampoActualizar.Total = NuevoValor;
                _context.SaveChanges();

                return Ok(CampoActualizar);
            }
            catch
            {
                return NotFound(ViajeErrorMessages.SPUEC);
            }
        }

       
    }
}
