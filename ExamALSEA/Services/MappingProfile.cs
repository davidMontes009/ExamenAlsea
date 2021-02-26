using ALSEA.Entities;
using AutoMapper;
using ExamALSEA.ViewModels;

namespace ExamALSEA.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, UserDTO>().ReverseMap();
            CreateMap<CityByUserViewModel, CitysDTO>().ReverseMap();
           

        }
    }
}
