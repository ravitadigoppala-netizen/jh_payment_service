namespace jh_payment_service.Model.Payments
{
    public class CardDetails
    {
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string ExpiryMonth { get; set; } = string.Empty; // MM
        public string ExpiryYear { get; set; } = string.Empty;  // YYYY
        public string CVV { get; set; } = string.Empty;
    }
}
