using AutoMapper;
using FileConverter.Service.Models;

namespace FileConverter.Service
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonAddress, Person>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NAME))
                .ForPath(dest => dest.Address.Line1, opt => opt.MapFrom(src => src.ADDRESS_LINE1))
                .ForPath(dest => dest.Address.Line2, opt => opt.MapFrom(src => src.ADDRESS_LINE2));

        }
    }
}