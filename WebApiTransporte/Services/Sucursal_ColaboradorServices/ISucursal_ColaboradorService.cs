using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.Sucursal_ColaboradorServices
{
    public interface ISucursal_ColaboradorService
    {
        public Task<ActionResult<Colaborador>> PostColaborador(ColaboradorDto colaboradorDTO);

        public  Task<List<SucursalColaboradorIInformation>> GetSucursalColaborador();
    }
}
