using Finflow.Hlopov.Application.Models;
using Finflow.Hlopov.Core.Entities;
using AutoMapper;
using System;
using System.Linq;

namespace Finflow.Hlopov.Application.Mapper
{
    // https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<DtoMapper>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Client, ClientModel>().ReverseMap();

            CreateMap<Remittance, RemmittanceModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(
                    src => src.Statuses.FirstOrDefault(
                        s => s.RemittanceStatusId == src.Statuses.Max(s => s.RemittanceStatusId))
                            .RemittanceStatus.Status.Value))
                .ForMember(dest => dest.Funds, opt => opt.MapFrom(
                    src => new FundModel
                    {
                        Rate = src.Rate,
                        ReceiveAmount = src.ReceiveAmount,
                        ReceiveCurrency = src.ReceiveCurrencyId,
                        SendAmount = src.SendAmount,
                        SendCurrency = src.SendCurrencyId,
                    }));

            CreateMap<RemmittanceModel, Remittance>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Funds.Rate))
                .ForMember(dest => dest.ReceiveAmount, opt => opt.MapFrom(src => src.Funds.ReceiveAmount))
                .ForMember(dest => dest.ReceiveCurrencyId, opt => opt.MapFrom(src => src.Funds.ReceiveCurrency))
                .ForMember(dest => dest.SendAmount, opt => opt.MapFrom(src => src.Funds.SendAmount))
                .ForMember(dest => dest.SendCurrencyId, opt => opt.MapFrom(src => src.Funds.SendCurrency));
        }
    }
}