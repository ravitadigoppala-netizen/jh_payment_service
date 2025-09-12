namespace jh_payment_service.Model
{
    public class PaymentDebitRequest : PaymentRequest
    {
        /// <summary>
        /// Represents the unique identifier of the product.
        /// </summary>
        public string ProductId { get; set; }
    }
}
