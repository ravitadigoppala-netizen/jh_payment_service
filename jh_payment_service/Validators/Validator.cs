using jh_payment_service.Model;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This class implements validation logic for payment requests.
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ValidatePaymentRequest(PaymentRequest paymentRequest, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Basic validation logic
            if (paymentRequest.SenderUserId == paymentRequest.ReceiverUserId)
            {
                errorMessage = "Sender and receiver cannot be the same user.";
                return false;
            }

            if (paymentRequest.Amount <= 0)
            {
                errorMessage = "Amount must be greater than zero.";
                return false;
            }

            if (paymentRequest.PaymentMethod == PaymentMethodType.Card)
            {
                if (paymentRequest.CardDetails == null ||
                    string.IsNullOrEmpty(paymentRequest.CardDetails.CardNumber) ||
                    string.IsNullOrEmpty(paymentRequest.CardDetails.CardHolderName) ||
                    int.Parse(paymentRequest.CardDetails.ExpiryMonth) < 1 || int.Parse(paymentRequest.CardDetails.ExpiryMonth) > 12 ||
                    int.Parse(paymentRequest.CardDetails.ExpiryYear) < DateTime.Now.Year || 
                    string.IsNullOrEmpty(paymentRequest.CardDetails.CVV) || paymentRequest.CardDetails.CVV.Length != 3)
                {
                    errorMessage = "Invalid card details.";
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.UPI)
            {
                if (paymentRequest.UpiDetails == null ||
                    string.IsNullOrEmpty(paymentRequest.UpiDetails.Vpa) || !paymentRequest.UpiDetails.Vpa.Contains("@"))
                {
                    errorMessage = "Invalid UPI details.";
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.NetBanking)
            {
                if (paymentRequest.NetBankingDetails == null ||
                    string.IsNullOrEmpty(paymentRequest.NetBankingDetails.BankName) ||
                    string.IsNullOrEmpty(paymentRequest.NetBankingDetails.AccountNumber) || !Regex.IsMatch(paymentRequest.NetBankingDetails.AccountNumber, @"^\d{10,12}$") ||
                    string.IsNullOrEmpty(paymentRequest.NetBankingDetails.IFSCCode))
                {
                    errorMessage = "Invalid net banking details.";
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.Wallet)
            {
                if (paymentRequest.WalletDetails == null ||
                    string.IsNullOrEmpty(paymentRequest.WalletDetails.WalletId) ||
                    string.IsNullOrEmpty(paymentRequest.WalletDetails.WalletProvider))
                {
                    errorMessage = "Invalid wallet details.";
                    return false;
                }
            }
            else
            {
                errorMessage = "Invalid payment method.";
                return false; // Invalid payment method
            }
            return true;
        }
    }
}
