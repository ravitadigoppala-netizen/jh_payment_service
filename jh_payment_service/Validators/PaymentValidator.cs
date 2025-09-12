using jh_payment_service.Model.Payments;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This class implements validation logic for payment requests.
    /// </summary>
    public class PaymentValidator : IPaymentValidator
    {
        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ValidatePaymentRequest(InitialPaymentModel paymentRequest, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Basic validation logic
            if (paymentRequest.SenderUserId == paymentRequest.ReceiverUserId)
            {
                errorMessage = "Sender and receiver cannot be the same user.";
                return false;
            }

            if (paymentRequest.Amount <= 0)
            {
                errorMessage = "Amount must be greater than zero.";
                return false;
            }

            return true;
        }
    }
}
