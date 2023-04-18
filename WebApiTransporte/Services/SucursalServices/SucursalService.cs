using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Models;
using WebApiTransporte.Dtos.SucursalDtos;

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
                var SucursalesExistente = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre == sucursalDeleteDTO.Nombre && x.Activo == true);
                if (SucursalesExistente == null)
                {
                    return BadRequest("Sucursal no encontrada");
                }

                SucursalesExistente.Activo = false;

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

        public async Task<List<SucursalColaboradorDto>> GetSucursalColaborador()
        {
            List<SucursalColaboradorDto> resp = new List<SucursalColaboradorDto>();

            var Sucursal = await _context.sucursal.Where(x => x.Activo == true).ToListAsync();
            if (Sucursal != null)
            {
                resp = _mapper.Map<List<Sucursal>, List<SucursalColaboradorDto>>(Sucursal);
            }
            return resp;
        }

        public async Task<List<SucursalDto>> GetSucursal()
        {
            List<SucursalDto> resp = new List<SucursalDto>();

            var sucursal = await _context.sucursal.Where(x => x.Activo == true).ToListAsync();
            if (sucursal != null)
            {
                resp = _mapper.Map<List<Sucursal>, List<SucursalDto>>(sucursal);
            }
            return resp;
        }

       

        public async Task<ActionResult<Sucursal>> PostSucursal(SucursalDto sucursalDTO)
        {
            try
            {
                var existeColaboradorConElmismoNombre = await _context.sucursal.AnyAsync(x => x.Nombre == sucursalDTO.Nombre && x.Activo == true);

                if (existeColaboradorConElmismoNombre)
                {
                    return BadRequest("La sucursal ya existe");
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
                    var SucursalExistente = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre == sucursalDTO.Nombre && x.Activo == true);
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