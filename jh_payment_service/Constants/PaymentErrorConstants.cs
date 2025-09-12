namespace jh_payment_service.Constants
{
    public class PaymentErrorConstants
    {
        public const string ErrorProcessingCreditPaymentCode = "PAY_0001";
        public const string ErrorProcessingCreditPayment = "An error occurred while processing credit payment.";

        public const string ErrorProcessingDebitPaymentCode = "PAY_0002";
        public const string ErrorProcessingDebitPayment = "An error occurred while processing debit payment.";

        public const string UserNotFoundCode = "PAY_0003";
        public const string UserNotFound = "User not found.";

        public const string InsufficientFundsCode = "PAY_0004";
        public const string InsufficientFunds = "Insufficient funds to process the payment.";

        public const string InvalidPaymentRequestCode = "PAY_0005";
        public const string InvalidPaymentRequest = "Invalid payment request.";

        public const string InvalidCardDetailsCode = "PAY_0006";
        public const string InvalidCardDetails = "Invalid card details.";

        public const string InvalidUpiDetailsCode = "PAY_0007";
        public const string InvalidUpiDetails = "Invalid UPI details.";

        public const string InvalidNetBankingDetailsCode = "PAY_0008";
        public const string InvalidNetBankingDetails = "Invalid net banking details.";

        public const string PaymentGatewayTimeoutCode = "PAY_0009";
        public const string InvalidPaymentMethod = "Invalid payment method.";

        public const string FailedToCreditAccountCode = "PAY_0010";
        public const string FailedToCreditAccount = "Failed to credit user's account.";

        public const string FailedToDebitAccountCode = "PAY_0011";
        public const string FailedToDebitAccount = "Failed to debit user's account.";

        public const string UserAccountNotFoundCode = "PAY_0012";
        public const string UserAccountNotFound = "User account not found.";


    }
}
