using AutoMapper;
using GBLAC.Models.DTOs;

namespace GBLAC.Models.AutoMapper
{
    public class MappingSetup : Profile
    {
        public MappingSetup()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<RegisterationDTO, AppUser>();
        }
    }
}
