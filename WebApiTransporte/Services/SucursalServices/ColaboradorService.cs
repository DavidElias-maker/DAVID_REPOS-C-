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
            var obtenercolaborador = await _context.colaborador.FirstOrDefaultAsync(x => x.PrimerNombre.Contains(PrimerNombre) && x.PrimerApellido.Contains(PrimerApellido));
            if (obtenercolaborador == null)
            {

                return BadRequest($"El nombre y apellido: {PrimerNombre} {PrimerApellido} no ha sido encontrado en la lista de colaboradores");
            }
            return _mapper.Map<ColaboradorDto>(obtenercolaborador);
        }

         async Task<ActionResult<Colaborador>> IColaboradorService.PostColaborador(ColaboradorDto colaboradorDTO)
        {
            var existeColaboradorConElmismoNombre = await _context.colaborador.AnyAsync(x => x.PrimerNombre == colaboradorDTO.PrimerNombre && x.PrimerApellido == colaboradorDTO.PrimerApellido);

            if (existeColaboradorConElmismoNombre)
            {
                return null;
            }
            var colaborador = _mapper.Map<Colaborador>(colaboradorDTO);

            _context.Add(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }
    }
}
