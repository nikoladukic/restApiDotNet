using AutoMapper;
using RestApiTemplate.Models.Domain;
using RestApiTemplate.Models.DTO;

namespace RestApiTemplate.Models.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<AddNewFakultet, Fakultet>()
                .ForMember(dest => dest.FakultetId, opt => opt.Ignore()); // Ignorišemo FakultetId pri mapiranju
            CreateMap<UpdateFakultet, Fakultet>()
                .ForMember(dest => dest.FakultetId, opt => opt.Ignore()); // Ignorišemo FakultetId pri mapiranju
        }
    }
}
