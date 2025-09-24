using jh_payment_service.Model.Payments;

namespace jh_payment_service.Model
{
    /// <summary>
    /// This class represents a credit payment request made by a user.
    /// </summary>
    public class CreditPaymentRequest
    {
        /// <summary>
        /// Represents the ID of the user making the credit payment request.
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Represents the amount to be paid.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Represents the payment method chosen by the user.
        /// </summary>
        public PaymentMethodType PaymentMethod { get; set; }

        /// <summary>
        /// Represents the details of the card if the payment method is Card.
        /// </summary>
        public CardDetails? CardDetails { get; set; }

        /// <summary>
        /// Represents the details of the UPI if the payment method is UPI.
        /// </summary>
        public UpiDetails? UpiDetails { get; set; }

        /// <summary>
        /// Represents the details of the net banking if the payment method is Net Banking.
        /// </summary>
        public NetBankingDetails? NetBankingDetails { get; set; }

        /// <summary>
        /// Represents the details of the wallet if the payment method is Wallet.
        /// </summary>
        public WalletDetails? WalletDetails { get; set; }
    }
}
