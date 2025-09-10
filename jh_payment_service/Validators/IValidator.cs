using jh_payment_service.Model;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This interface defines a contract for validating payment requests.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        bool ValidatePaymentRequest(PaymentRequest paymentRequest, out string errorMessage);
    }
}
