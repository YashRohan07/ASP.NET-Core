using AutoMapper;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            // Uncomment the next line if you want to map DTO back to entity:
            // CreateMap<UserDto, ApplicationUser>();
        }
    }
}
