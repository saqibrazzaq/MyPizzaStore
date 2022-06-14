using auth.Dtos.User;
using auth.Entities.Database;
using AutoMapper;

namespace auth
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<AppIdentityUser, UserDto>();
        }
    }
}
