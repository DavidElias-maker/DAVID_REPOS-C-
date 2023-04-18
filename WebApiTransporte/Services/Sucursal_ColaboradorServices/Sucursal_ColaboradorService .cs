using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiTransporte.Dtos;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;

namespace WebApiTransporte.Services.Sucursal_ColaboradorServices
{
    public class Sucursal_ColaboradorService : ControllerBase, ISucursal_ColaboradorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;



        public Sucursal_ColaboradorService(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<List<SucursalColaboradorIInformation>> GetSucursalColaborador()
        {
            var collist = await(from p in _context.sucursal_colaborador
                                join pm in _context.sucursal on p.SucursalId equals pm.Id
                                join pd in _context.colaborador on p.ColaboradorId equals pd.Id
                                select new SucursalColaboradorIInformation()
                                {
                                    Nombre = pm.Nombre,
                                    PrimerNombre = pd.PrimerNombre,
                                    PrimerApellido = pd.PrimerApellido

                                }).ToListAsync();

            return collist;
        }

        public Task<ActionResult<Colaborador>> PostColaborador(ColaboradorDto colaboradorDTO)
        {
            throw new NotImplementedException();
        }
    }
}
