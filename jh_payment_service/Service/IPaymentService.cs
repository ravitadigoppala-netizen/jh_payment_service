using jh_payment_service.Model;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Defines contract for payment related operations, such as initiate.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// payment Initiate process.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseModel> ProcessPaymentAsync(PaymentRequest request);
    }
}
