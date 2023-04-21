using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.TransportistaDtos;
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
        



        public ViajeController(IViajeService ViajeService)
        {
            this._ViajeService = ViajeService;
            


        }

        [HttpPost("Añadir Colaborador")]

        public async Task<ActionResult<Viaje_Detalle>> PostAñadirColaboradorViaje(int sucursal, int colaborador, Viaje_Detalle request)
        {

            var AñadirColaborador = await _ViajeService.PostAñadirColaboradorViaje(sucursal,colaborador,request);

            return AñadirColaborador;

        }


        [HttpPost("ViajeNuevo")]
        public async Task<ActionResult<Viaje>> PostNuevoViaje(Viaje viaje)
        {

            var AñadirNuevoViaje = await _ViajeService.PostNuevoViaje(viaje);

            return AñadirNuevoViaje;

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
    }
}