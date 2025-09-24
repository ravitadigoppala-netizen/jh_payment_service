namespace jh_payment_service.Model.Payments
{
    /// <summary>
    /// This class represents a check account balance in the payment service.
    /// </summary>
    public class CheckBalanceModel
    {
        /// <summary>
        /// Represents the unique identifier for the user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Represents the full name of the user.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Represents the current balance of the user's account.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
