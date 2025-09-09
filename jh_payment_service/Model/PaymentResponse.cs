namespace jh_payment_service.Model
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; } = Guid.NewGuid();
        public PaymentStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
