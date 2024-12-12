using AutoMapper;
using Domain.Entities;

namespace Application.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organization, Organization>();
            CreateMap<User, User>();
        }
    }
}
