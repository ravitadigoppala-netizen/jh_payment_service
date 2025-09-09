

using jh_payment_service.Model.Payments;

namespace jh_payment_service.Service
{
    /// <summary>
    /// This interface defines the contract for processing payments, including methods for crediting and debiting user accounts.
    /// </summary>
    public interface IProcessPaymentService
    {
        /// <summary>
        /// Credits the user's account with the specified transaction details.
        /// </summary>
        /// <param name="transaction"></param>
        void CreditUserAccount(Transaction transaction);

        /// <summary>
        /// Debits the user's account with the specified transaction details.
        /// </summary>
        /// <param name="transaction"></param>
        void DebitUserAccount(Transaction transaction);
    }
}
