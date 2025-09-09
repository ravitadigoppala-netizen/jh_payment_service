namespace jh_payment_service.Model
{
    /// <summary>
    /// This class represents the response after processing a payment.
    /// </summary>
    public class PaymentResponse
    {
        /// <summary>
        /// Represents the unique identifier for the payment transaction.
        /// </summary>
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Represents the status of the payment transaction.
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Represents any message associated with the payment response.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Represents the timestamp when the payment response was generated.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
