namespace jh_payment_service.Model.UserEntity
{
    /// <summary>
    /// This class represents a user account in the payment service.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// Represents the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Represents the full name of the user.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Represents the email address of the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Represents the mobile number of the user.
        /// </summary>
        public string MobileNumber { get; set; } = string.Empty;

        /// <summary>
        /// Represents the current balance of the user's account.
        /// </summary>
        public decimal Balance { get; set; }
    }
}
