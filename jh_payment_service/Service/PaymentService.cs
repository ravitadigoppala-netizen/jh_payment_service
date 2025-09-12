using jh_payment_service.Model;
using jh_payment_service.Model.Payments;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Implements payment-related operations.
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly Dictionary<PaymentMethodType, IPaymentHandler> _handlers;

        public PaymentService(Dictionary<PaymentMethodType, IPaymentHandler> handlers)
        {
            _handlers = handlers;
        }

        /// <summary>
        /// payment Initiation process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ProcessPaymentAsync(InitialPaymentModel request)
        {
            var handler = _handlers[PaymentMethodType.Wallet];
            return await handler.InitiatePaymentAsync(request);
        }
    }
}
