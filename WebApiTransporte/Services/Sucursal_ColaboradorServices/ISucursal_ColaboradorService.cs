using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.SucursalColaboradorDtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.Sucursal_ColaboradorServices
{
    public interface ISucursal_ColaboradorService
    {
        public Task<ActionResult<Sucursal_Colaborador>> PostSucursalColaborador(Sucursal_ColaboradorDto SucursalColaboradorDTO);

        public  Task<List<SucursalColaboradorIInformation>> GetSucursalColaborador();

        public Task<ActionResult> UpdateSucursalColaborador(Sucursal_ColaboradorDto sucursalcolaboradorDTO);
    }
}
