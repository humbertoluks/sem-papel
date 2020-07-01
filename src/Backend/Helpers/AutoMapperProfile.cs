using AutoMapper;

using Domain.Dtos;
using Domain.Models;
using Domain.ValueObjects;

namespace Backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GuiaDto, Guia>()
            .ForMember(dest => dest.Prestador, opts => opts.MapFrom(src => new Prestador()))
            .ForPath(dest => dest.Prestador.Codigo, opts => opts.MapFrom(src => src.CodigoPrestador))
            .ForMember(dest => dest.Unidade, opts => opts.MapFrom(src => new Unidade()))
            .ForPath(dest => dest.Unidade.Id, opts => opts.MapFrom(src => src.IdUnidade))
            .ForMember(dest => dest.GuiaNumero, opts => opts.MapFrom(src => new GuiaNumero()))
            .ForMember(dest => dest.PushId, opts => opts.MapFrom(src => src.PushId))
            .ForMember(dest => dest.TokenId, opts => opts.MapFrom(src => src.TokenId))
            .ForMember(dest => dest.Beneficiario, opts => opts.MapFrom(src => new Beneficiario()))
            .ForPath(dest => dest.Beneficiario.Cartao, opts => opts.MapFrom(src => src.CarteirinhaBeneficiario))
            .ForMember(dest => dest.GuiaXML, opts => opts.MapFrom(src => src.GuiaXML))
            .ForMember(dest => dest.Valor, opts => opts.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.GuiaOrigemFK, opts => opts.MapFrom(src => src.GuiaOrigemId))
            .ForMember(dest => dest.GuiaStatusFK, opts => opts.MapFrom(src => src.GuiaStatusId))
            .ForMember(dest => dest.GuiaTipoFK, opts => opts.MapFrom(src => src.GuiaTipoId))
            .ForMember(dest => dest.StatusCheckInFK, opts => opts.MapFrom(src => src.StatusCheckInId));
        }
    }
}