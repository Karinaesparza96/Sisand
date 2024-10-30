using Api.Dtos;
using AutoMapper;
using Business.Models;

namespace Api.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<UsuarioDto, Usuario>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
