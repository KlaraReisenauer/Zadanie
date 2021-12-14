using AutoMapper;
using ZadanieAPI.Database.Models;

namespace ZadanieAPI.Models.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<PositionDTO, Position>()
                    .ReverseMap();
        }
    }
}
