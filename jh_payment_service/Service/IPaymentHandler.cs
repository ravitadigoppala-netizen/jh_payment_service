using jh_payment_service.Model;
using jh_payment_service.Model.Payments;

namespace jh_payment_service.Service
{
    public interface IPaymentHandler
    {
        Task<ResponseModel> InitiatePaymentAsync(InitialPaymentModel request);
    }
}
