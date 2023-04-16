using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Models;

namespace WebApiTransporte.Controllers
{

    [ApiController]
    [Route("api/Sucursal")]
    public class SucursalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SucursalController(ApplicationDbContext context)
        {
            this._context = context;

        }
        [HttpPost]
        public async Task<ActionResult> Post(Sucursal PostSucursal)
        {

            var sucursal = new Sucursal()
            {
                Id = PostSucursal.Id,
                Nombre = PostSucursal.Nombre,
                Direccion = PostSucursal.Direccion,
                Activo = PostSucursal.Activo


            };

            await _context.sucursal.AddAsync(sucursal);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("nombre")] // tambien se puede usar caracteres, solo hay que 
        public async Task<ActionResult<Sucursal>> Get(string Nombre)
        {
            var autor = await _context.sucursal.FirstOrDefaultAsync(x => x.Nombre.Contains(Nombre));
            if (autor == null)
            {
                return NotFound(); // cuando se tiene un recurso que no ha sigo encontrado se coloca esta funcion not found
            }
            return autor; // el return nos regresa un valor y es necesario
        }

    }
}
