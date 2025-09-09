namespace jh_payment_service.Model.UserEntity
{
    public class UserAccount
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
