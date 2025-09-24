

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
        /// <param name="creditPaymentRequest"></param>
        Task<ResponseModel> CreditUserAccount(CreditPaymentRequest creditPaymentRequest);

        /// <summary>
        /// Debits the user's account with the specified paymentRequest details.
        /// </summary>
        /// <param name="debitPaymentRequest"></param>
        Task<ResponseModel> DebitUserAccount(DebitPaymentRequest debitPaymentRequest);

        /// <summary>
        /// GetAccountBalance
        /// </summary>
        /// <param name="userEmail"></param>
        Task<ResponseModel> GetAccountBalance(string userEmail);

        /// <summary>
        /// Get All Transactions for User
        /// </summary>
        /// <param name="userEmail"></param>
        Task<ResponseModel> GetAllTransactions(string userEmail, PageRequestModel pageRequestModel);
    }
}
