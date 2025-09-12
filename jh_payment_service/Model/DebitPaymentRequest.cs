using jh_payment_service.Model.Payments;

namespace jh_payment_service.Model
{
    /// <summary>
    /// This class represents a debit payment request made by a user.
    /// </summary>
    public class DebitPaymentRequest : CreditPaymentRequest
    {
        /// <summary>
        /// Represents the unique identifier of the product.
        /// </summary>
        public string ProductId { get; set; }
    }
}
