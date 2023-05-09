using AutoMapper;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Dtos.SucursalColaboradorDtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Dtos.TransportistaDtos;
using WebApiTransporte.Dtos.ViajeDtos;
using WebApiTransporte.Models;

namespace WebApiTransporte.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ColaboradorDto, Colaborador>().ReverseMap();

            CreateMap<ColaboradorDeleteDto, Colaborador>().ReverseMap();

            CreateMap<ColaboradorSucursalDto, Colaborador>().ReverseMap();


            CreateMap<SucursalDto, Sucursal>().ReverseMap();

            CreateMap<SucursalDeleteDto, Sucursal>().ReverseMap();

            CreateMap<SucursalColaboradorDto, Sucursal>().ReverseMap();



            CreateMap<TransportistaDto, Transportista>().ReverseMap();

            CreateMap<TransportistaDeleteDto, Transportista>().ReverseMap();

            CreateMap<TransportistaViajeDto, Transportista>().ReverseMap();


            CreateMap<Sucursal_ColaboradorDto, Sucursal_Colaborador>().ReverseMap();
            CreateMap<Sucursal_ColaboradorDeleteDto, Sucursal_Colaborador>().ReverseMap();


            CreateMap<ViajeDto, Viaje>().ReverseMap();
            CreateMap<Viaje_DetalleDto, Viaje_Detalle>().ReverseMap();

            CreateMap<ViajeInformationDto, Viaje>().ReverseMap();
            CreateMap<ViajeInformationDto, Viaje_Detalle>().ReverseMap();


        }
    }
}
