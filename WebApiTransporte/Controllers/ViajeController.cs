﻿using AutoMapper;
using Azure.Core;
using Castle.Core.Resource;
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

        [HttpPost("Añadir Colaborador")]

        public async Task<ActionResult<Viaje_Detalle>> PostAñadirColaboradorViaje(int sucursal, int colaborador, Viaje_Detalle request)
        {

            var AñadirColaborador = await _ViajeService.PostAñadirColaboradorViaje(sucursal,colaborador,request);

            return AñadirColaborador;

        }


        [HttpPost("ViajeNuevo")]
        public async Task<ActionResult<ViajeDto>> PostNuevoViaje(ViajeInformationDto viajeInformationDto)
        {
            

            var viaje = _mapper.Map<Viaje>(viajeInformationDto);

            viaje.TransportistaId = viajeInformationDto.viajedto.TransportistaId;
            viaje.Fecha = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            viaje.SucursalId = viajeInformationDto.viajedto.SucursalId;

            _context.viaje.Add(viaje);
            await _context.SaveChangesAsync();

            var viajeDetalleDto = new Viaje_DetalleDto
            {
                ViajeId = viaje.Id,
                
            };
            var viajeDetalle = _mapper.Map<Viaje_Detalle>(viajeDetalleDto);
            _context.viaje_detalle.Add(viajeDetalle);
            await _context.SaveChangesAsync();

            var viajeDto = _mapper.Map<ViajeDto>(viaje);
            return Ok(viajeDto);



        }
        [HttpPost("Calcularviaje")]
        public ActionResult<Viaje> PostCalcularViaje(Viaje viaje_detalle, int sucursalcolaboradorid, int transportista)
        {

            var CalcularViaje =  _ViajeService.PostCalcularViaje(viaje_detalle, sucursalcolaboradorid, transportista);

            return CalcularViaje;


        }
        [HttpGet("Reporte")]

        public async Task<ActionResult<Viaje>> GetTotalViaje(DateTime fechaInicial, DateTime fechafinal, int transportistaid)
        {
            var Reporte = await _ViajeService.GetTotalViaje(fechaInicial, fechafinal, transportistaid);

            return Reporte;

        }

        [HttpPost("pruebaingresarviaje")]




        public async Task<ActionResult<ViajeDto>> PostAñadirColaboradorPrueba(ViajeInformationDto viajeInformationDto)
        {
            var existeColaboradorConelmismoid = await _context.viaje_detalle.AnyAsync(x => x.SucursalColaboradoresId == viajeInformationDto.viaje_detalledto.SucursalColaboradoresId && x.ViajeId == viajeInformationDto.viaje_detalledto.ViajeId);

            if (existeColaboradorConelmismoid)
            {
                return BadRequest(TransportistaErrorMessages.ETYE);
            }

            var viaje_detalle = _mapper.Map<Viaje_Detalle>(viajeInformationDto);

            viaje_detalle.SucursalColaboradoresId = viajeInformationDto.viaje_detalledto.SucursalColaboradoresId;
            viaje_detalle.ViajeId = viajeInformationDto.viaje_detalledto.ViajeId;

            

            _context.viaje_detalle.Add(viaje_detalle);
            await _context.SaveChangesAsync();


            var DistanciaKm = (from f in _context.sucursal_colaborador
                               where f.Id == viaje_detalle.SucursalColaboradoresId
                               orderby f.Id
                               select f.DistanciaKm).FirstOrDefault();


           var Tarifa = (from f in _context.transportista
                         where f.Id == viajeInformationDto.viajedto.TransportistaId
                         orderby f.Id
                         select f.Tarifa).FirstOrDefault();

            var total = Convert.ToDecimal(DistanciaKm) * Convert.ToDecimal(Tarifa);

            var UltimoValor = _context.viaje
                .OrderByDescending(x => x.Id == viaje_detalle.ViajeId)
                .Select(x => x.Total)
                    .FirstOrDefault();

            var NuevoValor = UltimoValor + total;

            var CampoActualizar = await _context.viaje.OrderBy(x => x.Id == viaje_detalle.ViajeId).LastOrDefaultAsync();
                CampoActualizar.Total = NuevoValor;

            CampoActualizar.Total = NuevoValor;


            await _context.SaveChangesAsync();

                return Ok(CampoActualizar);

        }

       

    }
}