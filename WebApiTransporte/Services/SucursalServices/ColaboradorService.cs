using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;
namespace WebApiTransporte.Services.SucursalServices

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
        async Task<ActionResult<ColaboradorDto>> IColaboradorService.GetColaborador(string PrimerNombre, string PrimerApellido)
        {
            try
            {
                var obtenercolaborador = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre.Contains(PrimerNombre) && x.PrimerApellido.Contains(PrimerApellido) && x.Activo.Contains("si"));
                if (obtenercolaborador == null)
                {

                    return BadRequest($"El nombre y apellido: {PrimerNombre} {PrimerApellido} no ha sido encontrado en la lista de colaboradores");
                }
                return _mapper.Map<ColaboradorDto>(obtenercolaborador);
            }
            catch
            {
                return BadRequest("Se produjo un error de conexion");
            }
        }

         async Task<ActionResult<Colaborador>> IColaboradorService.PostColaborador(ColaboradorDto colaboradorDTO)
        {
            try
            {
                var existeColaboradorConElmismoNombre = await _context.colaborador.AnyAsync(x => x.PrimerNombre == colaboradorDTO.PrimerNombre && x.PrimerApellido == colaboradorDTO.PrimerApellido && x.Activo == "si");

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
                return BadRequest("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre == colaboradorDTO.PrimerNombre && x.PrimerApellido == colaboradorDTO.PrimerApellido && x.Activo == "si");
                if (ColaboradoresExistente == null)
                {
                    return BadRequest("Colaborador no encontrado");
                }

                var colaborador = _mapper.Map<ColaboradorDto, Colaborador>(colaboradorDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok("Actualizado de forma exitosa");

            }
            catch
            {
                return BadRequest("Se produjo un error de conexion");
            }


        }

        public async Task<ActionResult> DeleteColaborador(ColaboradorDeleteDto colaboradorDeleteDTO)
        {
            try
            {
                var ColaboradoresExistente = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre == colaboradorDeleteDTO.PrimerNombre && x.PrimerApellido == colaboradorDeleteDTO.PrimerApellido && x.Activo == "si");
                if (ColaboradoresExistente == null)
                {
                    return BadRequest("Colaborador no encontrado");
                }

                ColaboradoresExistente.Activo = "no";

                var colaborador = _mapper.Map<ColaboradorDeleteDto, Colaborador>(colaboradorDeleteDTO, ColaboradoresExistente);

                _context.Update(colaborador);
                await _context.SaveChangesAsync();

                return Ok("colaborador eliminado de forma exitosa");
            }
            catch
            {
                return BadRequest("Se produjo un error de conexion");
            }
        }
    }
}
