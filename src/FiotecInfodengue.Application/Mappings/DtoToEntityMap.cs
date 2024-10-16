using AutoMapper;
using FiotecInfodengue.Application.Dtos;
using FiotecInfodengue.Domain.Entities;

namespace FiotecInfodengue.Application.Mappings;

public class DtoToEntityMap : Profile
{
    public DtoToEntityMap()
    {
        CreateMap<UsuarioDto, Usuario>().ReverseMap();
        CreateMap<CriarUsuarioDto, Usuario>().ReverseMap();
        CreateMap<AtualizarUsuarioDto, Usuario>().ReverseMap();
        CreateMap<LoginDto, Usuario>().ReverseMap();
    }
}