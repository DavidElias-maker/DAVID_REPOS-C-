using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Error;
using Microsoft.Identity.Client;

namespace WebApiTransporte.Services.ColaboradorServices

{
    public class ColaboradorService : ControllerBase, IColaboradorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public ColaboradorService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }


        async Task<ActionResult<Colaborador>> IColaboradorService.PostColaborador(ColaboradorDto colaboradorDTO)
        {
            try
            {
                var existeColaboradorConElmismoNombre = await _context.colaborador.AnyAsync(x => x.PrimerNombre == colaboradorDTO.PrimerNombre && x.PrimerApellido == colaboradorDTO.PrimerApellido && x.Activo == true);

                if (existeColaboradorConElmismoNombre)
                {
                    return BadRequest(ColaboradorErrorMessages.ECYE);
                }
                var colaborador = _mapper.Map<Colaborador>(colaboradorDTO);

                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return colaborador;
            }
            catch
            {
                return NotFound(ColaboradorErrorMessages.SPUEC);
            }
        }

        public async Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.Id == colaboradorDTO.Id && x.Activo == true);
                if (ColaboradoresExistente == null)
                {
                    return NotFound(ColaboradorErrorMessages.CNE);
                }

                var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(colaboradorDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok(ColaboradorErrorMessages.ADFE);

            }
            catch
            {
                return NotFound(ColaboradorErrorMessages.SPUEC);
            }


        }

        public async Task<ActionResult> DeleteColaborador(ColaboradorDeleteDto colaboradorDeleteDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre == colaboradorDeleteDTO.PrimerNombre && x.PrimerApellido == colaboradorDeleteDTO.PrimerApellido && x.Activo == true);
                if (ColaboradoresExistente == null)
                {
                    return BadRequest(ColaboradorErrorMessages.CNE);
                }

                ColaboradoresExistente.Activo = false;

                var colaborador = _mapper.Map<ColaboradorDeleteDto, Colaborador>(colaboradorDeleteDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok(ColaboradorErrorMessages.CEDFE);
            }
            catch
            {
                return NotFound(ColaboradorErrorMessages.SPUEC);
            }
        }

        public async Task<List<ColaboradorDto>> GetColaborador()
        {

            List<ColaboradorDto> resp = new List<ColaboradorDto>();

            var colaborador = await _context.colaborador.Where(x => x.Activo == true).ToListAsync();
            if (colaborador != null)
            {
                resp = _mapper.Map<List<Colaborador>, List<ColaboradorDto>>(colaborador);
            }
            return resp;

        }

        public async Task<List<ColaboradorSucursalDto>> GetColaboradorSucursal()
        {

            List<ColaboradorSucursalDto> resp = new List<ColaboradorSucursalDto>();

            var colaborador = await _context.colaborador.Where(x => x.Activo == true).ToListAsync();
            if (colaborador != null)
            {
                resp = _mapper.Map<List<Colaborador>, List<ColaboradorSucursalDto>>(colaborador);
            }
            return resp;


        }
    }
}