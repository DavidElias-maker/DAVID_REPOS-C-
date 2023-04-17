using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.SucursalServices;

namespace WebApiTransporte.Services.TransportistaServices
{
    public class TransportistaService : ControllerBase, ITransportistaService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TransportistaService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
         async Task<ActionResult> ITransportistaService.DeleteTransportista(TransportistaDeleteDto transportistaDeleteDTO)
        {
            try
            {
                var TransportistaExistente = await _context.transportista.FirstOrDefaultAsync(x => x.PrimerNombre == transportistaDeleteDTO.PrimerNombre && x.Activo == "si");
                if (TransportistaExistente == null)
                {
                    return BadRequest("Transportista no encontrado");
                }
                TransportistaExistente.Activo = "no";

                var transportista = _mapper.Map<TransportistaDeleteDto, Transportista>(transportistaDeleteDTO, TransportistaExistente);

                _context.Update(transportista);
                await _context.SaveChangesAsync();

                return Ok("transportista eliminado de forma exitosa");
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        async Task<ActionResult<TransportistaDto>> ITransportistaService.GetTransportista(string PrimerNombre)
        {
            try
            {
                var obtenertransportista = await _context.transportista.FirstOrDefaultAsync(x => x.PrimerNombre.Contains(PrimerNombre) && x.Activo.Contains("si"));
                if (obtenertransportista == null)
                {

                    return BadRequest($"El nombre {PrimerNombre} no ha sido encontrado en la lista de Transportistas");
                }
                return _mapper.Map<TransportistaDto>(obtenertransportista);
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        async Task<ActionResult<Transportista>> ITransportistaService.PostTransportista(TransportistaDto transportistaDTO)
        {
            try
            {
                var existeTransportistaConElmismoNombre = await _context.transportista.AnyAsync(x => x.PrimerNombre == transportistaDTO.PrimerNombre && x.Activo.Contains("si"));

                if (existeTransportistaConElmismoNombre)
                {
                    return null;
                }
                var transportista = _mapper.Map<Transportista>(transportistaDTO);

                _context.Add(transportista);
                await _context.SaveChangesAsync();
                return transportista;
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

       async Task<ActionResult> ITransportistaService.UpdateTransportista(TransportistaDto TransportistaDTO)
        {
                try
                {
                    var TransportistaExistente = await _context.transportista.FirstOrDefaultAsync(x => x.PrimerNombre == TransportistaDTO.PrimerNombre && x.Activo == "si");
                    if (TransportistaExistente == null)
                    {
                        return NotFound("Transportista no encontrado");
                    }

                    var transportista = _mapper.Map<TransportistaDto, Transportista>(TransportistaDTO, TransportistaExistente);

                    _context.Update(transportista);
                    await _context.SaveChangesAsync();

                    return Ok("Actualizado de forma exitosa");

                }
                catch
                {
                    return NotFound("Se produjo un error de conexion");
                }
            }
    }
}
