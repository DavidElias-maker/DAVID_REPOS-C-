using AutoMapper;
using Azure.Core;
using Castle.Core.Resource;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Dtos.ViajeDtos;
using WebApiTransporte.Error;
using WebApiTransporte.Models;
using WebApiTransporte.Services.ColaboradorServices;
using WebApiTransporte.Services.ViajeServices;

namespace WebApiTransporte.Controllers
{
    [ApiController]
    [Route("api/Viaje")]
    public class ViajeController : ControllerBase
    {
        private readonly IViajeService _ViajeService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;




        public ViajeController(IViajeService ViajeService, ApplicationDbContext context, IMapper mapper)
        {
            this._ViajeService = ViajeService;
            this._context = context;
            this._mapper = mapper;



        }






        [HttpGet("Reporte")]

        public async Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid)
        {
            var Reporte = await _ViajeService.GetTotalViaje(fechaInicial, fechafinal, transportistaid);

            return Reporte;

        }




        [HttpPost]
        public async Task<ActionResult<ViajeInsert>> PostCicloFor(int transportistaId, [FromBody] List<ViajeInsert> viajeinsert)
        {
            var IngresarViaje = await _ViajeService.PostCicloFor(transportistaId, viajeinsert);

            return IngresarViaje;

        }
    }
}