using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Error;
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
                var TransportistaExistente = await _context.transportista.FirstOrDefaultAsync(x => x.PrimerNombre == transportistaDeleteDTO.PrimerNombre && x.Activo == true);
                if (TransportistaExistente == null)
                {
                    return BadRequest(TransportistaErrorMessages.TNE);
                }
                TransportistaExistente.Activo = false;

                var transportista = _mapper.Map<TransportistaDeleteDto, Transportista>(transportistaDeleteDTO, TransportistaExistente);

                _context.Update(transportista);
                await _context.SaveChangesAsync();

                return Ok(TransportistaErrorMessages.TEDFE);
            }
            catch
            {
                return NotFound(TransportistaErrorMessages.SPUEC);
            }
        }

        public async Task<List<TransportistaDto>> GetTransportista()
        {
            List<TransportistaDto> resp = new List<TransportistaDto>();

            var transportista = await _context.transportista.Where(x => x.Activo == true).ToListAsync();
            if (transportista != null)
            {
                resp = _mapper.Map<List<Transportista>, List<TransportistaDto>>(transportista);
            }
            return resp;
        }

        async Task<ActionResult<Transportista>> ITransportistaService.PostTransportista(TransportistaDto transportistaDTO)
        {
           
                try
                {
                    var existeColaboradorConElmismoNombre = await _context.transportista.AnyAsync(x => x.PrimerNombre == transportistaDTO.PrimerNombre && x.Activo == true);

                    if (existeColaboradorConElmismoNombre)
                    {
                        return BadRequest(TransportistaErrorMessages.ETYE);
                    }
                    var transportista = _mapper.Map<Transportista>(transportistaDTO);

                    _context.Add(transportista);
                    await _context.SaveChangesAsync();
                    return transportista;
                }
                catch
                {
                    return NotFound(TransportistaErrorMessages.SPUEC);
                }
            
            
        }

       async Task<ActionResult> ITransportistaService.UpdateTransportista(TransportistaDto TransportistaDTO)
        {
                try
                {
                    var TransportistaExistente = await _context.transportista.FirstOrDefaultAsync(x => x.PrimerNombre == TransportistaDTO.PrimerNombre && x.Activo == true);
                    if (TransportistaExistente == null)
                    {
                        return NotFound(TransportistaErrorMessages.TNE);
                    }

                    var transportista = _mapper.Map<TransportistaDto, Transportista>(TransportistaDTO, TransportistaExistente);

                    _context.Update(transportista);
                    await _context.SaveChangesAsync();

                    return Ok(TransportistaErrorMessages.ADFE);

                }
                catch
                {
                    return NotFound(TransportistaErrorMessages.SPUEC);
                }
            }

        public async Task<List<TransportistaViajeDto>> GetTransportistaViaje()
        {
            List<TransportistaViajeDto> resp = new List<TransportistaViajeDto>();

            var transportista = await _context.transportista.Where(x => x.Activo == true).ToListAsync();
            if (transportista != null)
            {
                resp = _mapper.Map<List<Transportista>, List<TransportistaViajeDto>>(transportista);
            }
            return resp;
        }
    }
}
