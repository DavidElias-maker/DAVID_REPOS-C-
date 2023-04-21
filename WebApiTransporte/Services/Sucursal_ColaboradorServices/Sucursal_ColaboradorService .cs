using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalColaboradorDtos;
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
                                         PrimerNombre = pd.PrimerNombre,
                                         PrimerApellido = pd.PrimerApellido

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
    }
}
