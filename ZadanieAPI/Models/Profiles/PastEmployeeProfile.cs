using AutoMapper;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Models.Profiles
{
    public class PastEmployeeProfile : Profile
    {
        public PastEmployeeProfile()
        {
            CreateMap<PastEmployeeDTO, Employee>()
                .ReverseMap();
        }
    }
}
