using Microsoft.AspNetCore.Mvc;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.SucursalServices
{
    public interface ISucursalService
    {
        public Task<List<SucursalDto>> GetSucursal();
        public Task<ActionResult<Sucursal>> PostSucursal(SucursalDto sucursalDTO);
        public Task<ActionResult> UpdateSucursal(SucursalDto sucursalDTO);
        public Task<ActionResult> DeleteSucursal(SucursalDeleteDto sucursalDeleteDTO);
        public Task<List<SucursalColaboradorDto>> GetSucursalColaborador();
    }
}
