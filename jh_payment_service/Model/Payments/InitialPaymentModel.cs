namespace jh_payment_service.Model.Payments
{
    public class InitialPaymentModel
    {
        /// <summary>
        /// Represents the email of the user sending the payment.
        /// </summary>
        public string SenderUserId { get; set; }

        /// <summary>
        /// Represents the email of the user receiving the payment.
        /// </summary>
        public string ReceiverUserId { get; set; }

        /// <summary>
        /// Represents the amount to be paid.
        /// </summary>
        public decimal Amount { get; set; }
    }
}
