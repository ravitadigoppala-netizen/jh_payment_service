namespace jh_payment_service.Model
{
    /// <summary>
    /// This class represents the details of Card Payment.
    /// </summary>
    public class CardPaymentRequest: PaymentRequest
    {
        public string ReceiverCardNumber { get; set; }

    }
}
