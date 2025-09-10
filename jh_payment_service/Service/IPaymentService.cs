using jh_payment_service.Model;
using jh_payment_service.Model.Payments;

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
        Task<ResponseModel> ProcessPaymentAsync(InitialPaymentModel request);
    }
}
