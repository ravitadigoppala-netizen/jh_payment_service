

using jh_payment_service.Model;
using jh_payment_service.Model.Entity;

namespace jh_payment_service.Service
{
    /// <summary>
    /// This interface defines the contract for processing payments, including methods for crediting and debiting user accounts.
    /// </summary>
    public interface IProcessPaymentService
    {
        /// <summary>
        /// Credits the user's account with the specified paymentRequest details.
        /// </summary>
        /// <param name="paymentRequest"></param>
        Task<ResponseModel> CreditUserAccount(PaymentRequest paymentRequest);

        /// <summary>
        /// Debits the user's account with the specified paymentRequest details.
        /// </summary>
        /// <param name="paymentRequest"></param>
        Task<ResponseModel> DebitUserAccount(PaymentRequest paymentRequest);

        /// <summary>
        /// GetAccountBalance
        /// </summary>
        /// <param name="userId"></param>
        Task<ResponseModel> GetAccountBalance(long userId);
    }
}
