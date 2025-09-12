using jh_payment_service.Model.Payments;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This interface defines a contract for validating payment requests.
    /// </summary>
    public interface IPaymentValidator
    {
        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        bool ValidatePaymentRequest(InitialPaymentModel paymentRequest, out string errorMessage);
    }
}

