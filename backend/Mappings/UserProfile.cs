using AutoMapper;
using CrudApplication.DTOs;
using CrudApplication.Models;

namespace CrudApplication.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserDto>();
            CreateMap<CreateUserDto, Users>()
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => true));

            CreateMap<UpdateUserDto, Users>();
        }
    }
}
