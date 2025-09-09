namespace jh_payment_service.Model.Payments
{
    public class WalletDetails
    {
        public string WalletProvider { get; set; } = string.Empty; // e.g. Paytm, PhonePe
        public string WalletId { get; set; } = string.Empty;
    }
}
