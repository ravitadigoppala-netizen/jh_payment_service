namespace jh_payment_service.Model.Payments
{
    /// <summary>
    /// This class represents a financial transaction between users.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Represents the unique identifier for the transaction.
        /// </summary>
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Represents the user ID of the sender in the transaction.
        /// </summary>
        public Guid FromUserId { get; set; }

        /// <summary>
        /// Represents the user ID of the receiver in the transaction.
        /// </summary>
        public Guid ToUserId { get; set; }

        /// <summary>
        /// Represents the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Represents the type of transaction (Credit or Debit).
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Represents the payment method used in the transaction. Options include Card, UPI, NetBanking, and Wallet.
        /// </summary>
        public PaymentMethodType PaymentMethod { get; set; }

        /// <summary>
        /// Represents the current status of the transaction. Options include Pending, Success, and Failed.
        /// </summary>
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        /// <summary>
        /// Represents the timestamp when the transaction was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Represents the timestamp when the transaction was completed. Null if the transaction is still pending.
        /// </summary>
        public DateTime? CompletedAt { get; set; }
    }
}
