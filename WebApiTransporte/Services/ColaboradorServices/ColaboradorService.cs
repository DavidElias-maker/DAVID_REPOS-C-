using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;
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
                    return null;
                }
                var colaborador = _mapper.Map<Colaborador>(colaboradorDTO);

                _context.Add(colaborador);
                await _context.SaveChangesAsync();
                return colaborador;
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre == colaboradorDTO.PrimerNombre && x.PrimerApellido == colaboradorDTO.PrimerApellido && x.Activo == true);
                if (ColaboradoresExistente == null)
                {
                    return NotFound("Colaborador no encontrado");
                }

                var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(colaboradorDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok("Actualizado de forma exitosa");

            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }


        }

        public async Task<ActionResult> DeleteColaborador(ColaboradorDeleteDto colaboradorDeleteDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre == colaboradorDeleteDTO.PrimerNombre && x.PrimerApellido == colaboradorDeleteDTO.PrimerApellido && x.Activo == true);
                if (ColaboradoresExistente == null)
                {
                    return BadRequest("Colaborador no encontrado");
                }

                ColaboradoresExistente.Activo = false;

                var colaborador = _mapper.Map<ColaboradorDeleteDto, Colaborador>(colaboradorDeleteDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok("colaborador eliminado de forma exitosa");
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult<ColaboradorDto>> GetColaborador()
        {
            try
            {

                // return Ok(_context.colaborador.Select(x => _mapper.Map<ColaboradorDto>(x)).Where(x => x.Activo == colaborador.Activo = true);

               var colaborador = await _context.colaborador.Where(x=>x.Activo == true).ToListAsync();

               return _mapper.Map<ColaboradorDto>(colaborador);





            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }
    }
}
