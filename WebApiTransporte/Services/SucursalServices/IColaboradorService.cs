using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.SucursalServices
{
    public interface IColaboradorService
    {
        public Task<ActionResult<ColaboradorDto>> GetColaborador(string PrimerNombre, string PrimerApellido);

        public Task<ActionResult<Colaborador>> PostColaborador(ColaboradorDto colaboradorDTO);

        public Task<ActionResult> UpdateColaborador(ColaboradorDto colaboradorDTO);
    }
}
