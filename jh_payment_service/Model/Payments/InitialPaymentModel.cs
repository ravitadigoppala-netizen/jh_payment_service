namespace jh_payment_service.Model.Payments
{
    public class InitialPaymentModel
    {
        /// <summary>
        /// Represents the unique identifier of the user sending the payment.
        /// </summary>
        public long SenderUserId { get; set; }

        /// <summary>
        /// Represents the unique identifier of the user receiving the payment.
        /// </summary>
        public long ReceiverUserId { get; set; }

        /// <summary>
        /// Represents the amount to be paid.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
