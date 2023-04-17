using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Services.SucursalServices
{
    public class SucursalService : ControllerBase, ISucursalService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SucursalService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<ActionResult> DeleteSucursal(SucursalDeleteDto sucursalDeleteDTO)
        {
            try
            {
                var SucursalesExistente = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre == sucursalDeleteDTO.Nombre &&  x.Activo == "si");
                if (SucursalesExistente == null)
                {
                    return BadRequest("Sucursal no encontrada");
                }

                SucursalesExistente.Activo = "no";

                var sucursal = _mapper.Map<SucursalDeleteDto, Sucursal>(sucursalDeleteDTO, SucursalesExistente);

                _context.Update(sucursal);
                await _context.SaveChangesAsync();

                return Ok("sucursal eliminado de forma exitosa");
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult<SucursalDto>> GetSucursal(string Nombre)
        {
            try
            {
                var obtenersucursal = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre.Contains(Nombre) && x.Activo.Contains("si"));
                if (obtenersucursal == null)
                {

                    return BadRequest($"El nombre {Nombre} no ha sido encontrado en la lista de sucursales");
                }
                return _mapper.Map<SucursalDto>(obtenersucursal);
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult<Sucursal>> PostSucursal(SucursalDto sucursalDTO)
        {
            try
            {
                var existeSucursalConElmismoNombre = await _context.sucursal.AnyAsync(x => x.Nombre == sucursalDTO.Nombre && x.Activo.Contains("si"));

                if (existeSucursalConElmismoNombre)
                {
                    return null;
                }
                var sucursal = _mapper.Map<Sucursal>(sucursalDTO);

                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return sucursal;
            }
            catch
            {
                return NotFound("Se produjo un error de conexion");
            }
        }

        public async Task<ActionResult> UpdateSucursal(SucursalDto sucursalDTO)
        {
                try
                {
                    var SucursalExistente = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre == sucursalDTO.Nombre && x.Activo == "si");
                    if (SucursalExistente == null)
                    {
                        return NotFound("Sucursal no encontrado");
                    }

                    var sucursal = _mapper.Map<SucursalDto, Sucursal>(sucursalDTO, SucursalExistente);

                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();

                    return Ok("Actualizado de forma exitosa");

                }
                catch
                {
                    return NotFound("Se produjo un error de conexion");
                }


            
        }
    }
}