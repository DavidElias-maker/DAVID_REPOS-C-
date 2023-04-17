using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.ColaboradorServices
{
    public interface IColaboradorService
    {
        public Task<ActionResult<ColaboradorDto>> GetColaborador();

        public Task<ActionResult<Colaborador>> PostColaborador(ColaboradorDto colaboradorDTO);

        public Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO);
        public Task<ActionResult> DeleteColaborador(ColaboradorDeleteDto colaboradorDeleteDTO);
        
    }
}
