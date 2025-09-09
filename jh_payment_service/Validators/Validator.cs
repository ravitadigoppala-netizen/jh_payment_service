using jh_payment_service.Model;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This class implements validation logic for payment requests.
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ValidatePaymentRequest(PaymentRequest paymentRequest)
        {
            // Basic validation logic
            if (paymentRequest.Amount <= 0)
                return false;
            if (paymentRequest.SenderUserId == Guid.Empty || paymentRequest.ReceiverUserId == Guid.Empty)
                return false;
            if (paymentRequest.SenderUserId == paymentRequest.ReceiverUserId)
                return false;
            // Additional validations based on payment method can be added here
            return true;
        }
    }
}
