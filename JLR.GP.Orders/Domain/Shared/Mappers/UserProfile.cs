using AutoMapper;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Models;

namespace ApiPortal_DataLake.Domain.Shared.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Usuario, UsuarioResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.FirstOrDefault(new RolUsuario(){ Rol = null }).Rol));
            CreateMap<Rol, RolResponse>();
        }
    }
}
