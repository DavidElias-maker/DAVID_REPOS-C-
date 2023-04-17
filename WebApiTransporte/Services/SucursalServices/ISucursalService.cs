using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.SucursalServices
{
    public interface ISucursalService
    {
        public Task<ActionResult<SucursalDto>> GetSucursal(string Nombre);

        public Task<ActionResult<Sucursal>> PostSucursal(SucursalDto sucursalDTO);

        public Task<ActionResult> UpdateSucursal(SucursalDto sucursalDTO);
        public Task<ActionResult> DeleteSucursal(SucursalDeleteDto sucursalDeleteDTO);
    }
}
