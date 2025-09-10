using jh_payment_service.Model;

namespace jh_payment_service.Service
{
    public interface IPaymentHandler
    {
        Task<ResponseModel> InitiatePaymentAsync(PaymentRequest request);
    }
}
