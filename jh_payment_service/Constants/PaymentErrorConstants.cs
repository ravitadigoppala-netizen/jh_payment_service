namespace jh_payment_service.Constants
{
    public class PaymentErrorConstants
    {
        public const string ErrorProcessingCreditPaymentCode = "PAY_001";
        public const string ErrorProcessingCreditPayment = "An error occurred while processing credit payment.";

        public const string ErrorProcessingDebitPaymentCode = "PAY_002";
        public const string ErrorProcessingDebitPayment = "An error occurred while processing debit payment.";

        public const string UserNotFoundCode = "PAY_003";
        public const string UserNotFound = "User not found.";

        public const string InsufficientFundsCode = "PAY_004";
        public const string InsufficientFunds = "Insufficient funds to process the payment.";

        public const string InvalidPaymentRequestCode = "COM_001";
        public const string InvalidPaymentRequest = "Invalid payment request.";

        public const string InvalidUserId = "Invalid user ID.";
        public const string AmountMustBeGreaterThanZero = "Amount must be greater than zero.";
        public const string ProductIdRequired = "ProductId is required.";


        public const string InvalidCardDetails = "Invalid card details.";
        public const string InvalidUpiDetails = "Invalid UPI details.";
        public const string InvalidNetBankingDetails = "Invalid net banking details.";
        public const string InvalidWalletDetails = "Invalid wallet details.";
        public const string InvalidPaymentMethod = "Invalid payment method.";

        public const string FailedToCreditAccountCode = "PAY_005";
        public const string FailedToCreditAccount = "Failed to credit user's account.";

        public const string FailedToDebitAccountCode = "PAY_006";
        public const string FailedToDebitAccount = "Failed to debit user's account.";

        public const string UserAccountNotFoundCode = "PAY_007";
        public const string UserAccountNotFound = "User account not found.";

        public const string UserTransactionsNotFoundCode = "PAY_008";
        public const string UserTransactionsNotFound = "Error while getting user transactions not found.";

        public const string InvalidSortByFieldCode = "PAY_009";
        public const string InvalidSortByField = "Invalid sort by field";

        public const string TransactionSuccess = "Transaction completed successfully";
        
    }
}
