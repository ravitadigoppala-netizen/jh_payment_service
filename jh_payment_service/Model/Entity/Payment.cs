namespace jh_payment_service.Model.Entity
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string SenderUserId { get; set; }
        public string ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethodType Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
