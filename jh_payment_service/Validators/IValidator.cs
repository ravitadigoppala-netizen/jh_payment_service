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
        /// <param name="creditPaymentRequest"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool ValidateCreditPaymentRequest(CreditPaymentRequest creditPaymentRequest, out string errorMessage);

        /// <summary>
        /// Validates the given debit payment request.
        /// </summary>
        /// <param name="debitPaymentRequest"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        bool ValidateDebitPaymentRequest(DebitPaymentRequest debitPaymentRequest, out string errorMessage);

        bool IsValidFieldForModel<T>(string fieldName);
    }
}
