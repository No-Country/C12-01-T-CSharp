using API.dtos;
using AutoMapper;
using Core.entidades.Orden;
using Core.Entities;

namespace API.helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Libro, LibroDTO>().ReverseMap();
            CreateMap<Direccion,DireccionDTO>().ReverseMap();
        }
    }
}
