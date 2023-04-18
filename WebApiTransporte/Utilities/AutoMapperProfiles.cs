using AutoMapper;
using WebApiTransporte.Dtos;
using WebApiTransporte.Dtos.ColaboradorDtos;
using WebApiTransporte.Dtos.SucursalDtos;
using WebApiTransporte.Dtos.TransportistaDtos;
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

            CreateMap<TransportistaDto, Transportista>().ReverseMap();

        }
    }
}
