using AutoMapper;
using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace jh_payment_service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Maps the DTO to the domain model.
            // AutoMapper will handle nested objects (Address) automatically if property names match.
            //CreateMap<UserRegistrationRequest, User>()
            //    .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountDetails.AccountNumber))
            //    .ForMember(dest => dest.RoutingNumber, opt => opt.MapFrom(src => src.AccountDetails.RoutingNumber))
            //    .ForMember(dest => dest.CardNumber, opt => opt.MapFrom(src => src.AccountDetails.CardNumber));

            CreateMap<CreditPaymentRequest, PaymentRequest>()
                .ForMember(dest => dest.SenderUserId, opt => opt.MapFrom(src => src.UserEmail));

            CreateMap<DebitPaymentRequest, PaymentDebitRequest>()
                .ForMember(dest => dest.SenderUserId, opt => opt.MapFrom(src => src.UserEmail));
        }
    }
}
