namespace jh_payment_service.Model.Entity
{
    public class Transaction
    {
        /// <summary>
        /// Represents the unique identifier for the transaction.
        /// </summary>
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Represents the unique identifier for the transaction.
        /// </summary>
        public Guid PaymentId { get; set; }


        /// <summary>
        /// Represents the user email ID of the sender in the transaction.
        /// </summary>
        public string FromUserId { get; set; }

        /// <summary>
        /// Represents the user email ID of the receiver in the transaction.
        /// </summary>
        public string ToUserId { get; set; }

        /// <summary>
        /// Represents the amount involved in the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Represents the Product ID associated with the transaction, if applicable.
        /// </summary>
        public string? ProductId { get; set; } = null;

        public PaymentStatus TrasactionStatus { get; set; } = PaymentStatus.Success;

        public PaymentMethodType Type { get; set; }

        /// <summary>
        /// Represents the type of transaction (Credit or Debit).
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
