using System;

namespace jh_payment_service.Model
{
    /// <summary>
    /// Represents a refund request for a payment transaction.
    /// </summary>
    public class RefundRequest
    {
        /// <summary>
        /// The unique transaction identifier for the original payment.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// The amount to refund.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The reason for issuing the refund.
        /// </summary>
        public string Reason { get; set; }
    }

    /// <summary>
    /// Represents a refund response object.
    /// </summary>
    public class RefundResponse
    {
        /// <summary>
        /// Unique identifier of the refund transaction.
        /// </summary>
        public string RefundId { get; set; }

        /// <summary>
        /// Original transaction identifier.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Refund amount.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Reason for the refund.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Date and time the refund was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
