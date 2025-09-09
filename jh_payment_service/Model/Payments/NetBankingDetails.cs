namespace jh_payment_service.Model.Payments
{
    public class NetBankingDetails
    {
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
    }
}
