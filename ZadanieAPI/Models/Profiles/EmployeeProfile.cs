using AutoMapper;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Models.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>().ReverseMap();
        }
    }
}
