using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalColaboradorDtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Error;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;

namespace WebApiTransporte.Services.Sucursal_ColaboradorServices
{
    public class Sucursal_ColaboradorService : ControllerBase, ISucursal_ColaboradorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public Sucursal_ColaboradorService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<List<SucursalColaboradorIInformation>> GetSucursalColaborador()
        {
            

                var collist = await (from p in _context.sucursal_colaborador
                                     join ps in _context.sucursal_colaborador on p.Id equals ps.Id
                                     join pm in _context.sucursal on p.SucursalId equals pm.Id
                                     join pd in _context.colaborador on p.ColaboradorId equals pd.Id
                                     
                                     select new SucursalColaboradorIInformation()
                                     {
                                         Id = ps.Id,
                                         Nombre = pm.Nombre,
                                         NombreCompleto = pd.PrimerNombre + " " + pd.PrimerApellido,
                                        DistanciaKm = ps.DistanciaKm,
                                        colaboradorId = pd.Id,
                                        sucursalId = pm.Id

                                     }).ToListAsync();

                return collist;
            
        }

        public async Task<ActionResult<Sucursal_Colaborador>> PostSucursalColaborador(Sucursal_ColaboradorDto SucursalColaboradorDTO)
        {
            try
            {
                var existeSucursal_ColaboradorConElmismoNombre = await _context.sucursal_colaborador.AnyAsync(x => x.SucursalId == SucursalColaboradorDTO.SucursalId && x.ColaboradorId == SucursalColaboradorDTO.ColaboradorId);

                if (existeSucursal_ColaboradorConElmismoNombre)
                {
                    return BadRequest(Sucursal_ColaboradorErrorMessages.YEUCEES);
                }

                var colaborador = _mapper.Map<Sucursal_Colaborador>(SucursalColaboradorDTO);

                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound(Sucursal_ColaboradorErrorMessages.SPUEC);
            }
        }

        public async Task<ActionResult> UpdateSucursalColaborador(Sucursal_ColaboradorDto sucursalcolaboradorDTO)
        {
            try
            {
                var SucursalColaboradorExistente = await _context.sucursal_colaborador.FirstOrDefaultAsync(x => x.Id == sucursalcolaboradorDTO.Id && x.Activo == true);
                if (SucursalColaboradorExistente == null)
                {
                    return NotFound(SucursalErrorMessages.LSNFE);
                }

                var sucursalcolaborador = _mapper.Map<Sucursal_ColaboradorDto, Sucursal_Colaborador>(sucursalcolaboradorDTO, SucursalColaboradorExistente);

                _context.Update(sucursalcolaborador);
                await _context.SaveChangesAsync();

                return Ok(SucursalErrorMessages.ADFE);

            }
            catch
            {
                return NotFound(SucursalErrorMessages.SPUEC);
            }



        }

        public async Task<ActionResult> DeleteSucursalColaborador(Sucursal_ColaboradorDeleteDto sucursalcolaboradorDeleteDTO)
        {
            try
            {
                var Sucursal_ColaboradoresExistente = await _context.sucursal_colaborador.FirstOrDefaultAsync(x => x.Id == sucursalcolaboradorDeleteDTO.Id && x.Activo == true);
                if (Sucursal_ColaboradoresExistente == null)
                {
                    return BadRequest(ColaboradorErrorMessages.CNE);
                }

                Sucursal_ColaboradoresExistente.Activo = false;

                var sucursal_colaborador = _mapper.Map<Sucursal_ColaboradorDeleteDto, Sucursal_Colaborador>(sucursalcolaboradorDeleteDTO, Sucursal_ColaboradoresExistente);

                _context.Update(sucursal_colaborador);
                await _context.SaveChangesAsync();

                return Ok(ColaboradorErrorMessages.CEDFE);
            }
            catch
            {
                return NotFound(ColaboradorErrorMessages.SPUEC);
            }
        }

    }
}
