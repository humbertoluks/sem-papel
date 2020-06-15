using AutoMapper;
using Domain.Models;
using Domain.Dtos;

namespace Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Document, opts => opts.MapFrom(src => src.Document))
            .ForPath(dest => dest.Document.Number, opts => opts.MapFrom(src => src.Document.Number))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForPath(dest => dest.Email.Address, opts => opts.MapFrom(src => src.Email.Address))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForPath(dest => dest.Name.Firstname, opts => opts.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForPath(dest => dest.Name.Lastname, opts => opts.MapFrom(src => src.Name.Lastname))
            
            .ReverseMap();
        }
    }
}