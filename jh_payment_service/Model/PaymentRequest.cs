using jh_payment_service.Model.Payments;

namespace jh_payment_service.Model
{
    public class PaymentRequest
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }

        // Optional payment details based on selected method
        public CardDetails? CardDetails { get; set; }
        public UpiDetails? UpiDetails { get; set; }
        public NetBankingDetails? NetBankingDetails { get; set; }
        public WalletDetails? WalletDetails { get; set; }
    }
}
