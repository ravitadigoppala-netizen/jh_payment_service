using jh_payment_service.Constants;
using jh_payment_service.Model;
using jh_payment_service.Model.Payments;
using System.Globalization;
using System.Reflection;
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
        public bool ValidateCreditPaymentRequest(CreditPaymentRequest paymentRequest, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Basic validation logic

            if (string.IsNullOrWhiteSpace(paymentRequest.UserEmail))
            {
                errorMessage = PaymentErrorConstants.InvalidUserId;
                return false;
            }

            if (paymentRequest.Amount <= 0)
            {
                errorMessage = PaymentErrorConstants.AmountMustBeGreaterThanZero;
                return false;
            }

            if (paymentRequest.PaymentMethod == PaymentMethodType.Card)
            {
                if (!ValidateCardDetails(paymentRequest.CardDetails, ref errorMessage))
                    return false;
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.UPI)
            {
                if (!ValidateUpiDetails(paymentRequest.UpiDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.NetBanking)
            {
                if(!ValidateNetbankingDetails(paymentRequest.NetBankingDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.Wallet)
            {
                if(!ValidateWalletDetails(paymentRequest.WalletDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else
            {
                errorMessage = PaymentErrorConstants.InvalidPaymentMethod;
                return false; // Invalid payment method
            }
            return true;
        }

        public bool ValidateDebitPaymentRequest(DebitPaymentRequest paymentRequest, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Basic validation logic

            if (string.IsNullOrWhiteSpace(paymentRequest.UserEmail))
            {
                errorMessage = PaymentErrorConstants.InvalidUserId;
                return false;
            }

            if (paymentRequest.Amount <= 0)
            {
                errorMessage = PaymentErrorConstants.AmountMustBeGreaterThanZero;
                return false;
            }

            if (string.IsNullOrWhiteSpace(paymentRequest.ProductId))
            {
                errorMessage = PaymentErrorConstants.ProductIdRequired;
                return false;
            }

            if (paymentRequest.PaymentMethod == PaymentMethodType.Card)
            {
                if (!ValidateCardDetails(paymentRequest.CardDetails, ref errorMessage))
                    return false;
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.UPI)
            {
                if (!ValidateUpiDetails(paymentRequest.UpiDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.NetBanking)
            {
                if (!ValidateNetbankingDetails(paymentRequest.NetBankingDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else if (paymentRequest.PaymentMethod == PaymentMethodType.Wallet)
            {
                if (!ValidateWalletDetails(paymentRequest.WalletDetails, ref errorMessage))
                {
                    return false;
                }
            }
            else
            {
                errorMessage = PaymentErrorConstants.InvalidPaymentMethod;
                return false; // Invalid payment method
            }
            return true;
        }

        private bool ValidateWalletDetails(WalletDetails? walletDetails, ref string errorMessage)
        {
            if (walletDetails == null ||
                    string.IsNullOrEmpty(walletDetails.WalletId) ||
                    string.IsNullOrEmpty(walletDetails.WalletProvider))
            {
                errorMessage = PaymentErrorConstants.InvalidWalletDetails;
                return false;
            }
            return true;
        }

        private bool ValidateNetbankingDetails(NetBankingDetails? netBankingDetails, ref string errorMessage)
        {
            if (netBankingDetails == null ||
                    string.IsNullOrEmpty(netBankingDetails.BankName) ||
                    string.IsNullOrEmpty(netBankingDetails.AccountNumber) || !Regex.IsMatch(netBankingDetails.AccountNumber, @"^\d{10,12}$") ||
                    string.IsNullOrEmpty(netBankingDetails.IFSCCode))
            {
                errorMessage = PaymentErrorConstants.InvalidNetBankingDetails;
                return false;
            }
            return true;
        }

        private static bool ValidateUpiDetails(UpiDetails upiDetails, ref string errorMessage)
        {
            if (upiDetails == null ||
                                string.IsNullOrEmpty(upiDetails.Vpa) || !upiDetails.Vpa.Contains("@"))
            {
                errorMessage = PaymentErrorConstants.InvalidUpiDetails;
                return false;
            }

            return true;
        }

        private static bool ValidateCardDetails(CardDetails cardDetails, ref string errorMessage)
        {
            if (cardDetails == null ||
                                string.IsNullOrEmpty(cardDetails.CardNumber) ||
                                string.IsNullOrEmpty(cardDetails.CardHolderName) ||
                                int.Parse(cardDetails.ExpiryMonth) < 1 || int.Parse(cardDetails.ExpiryMonth) > 12 ||
                                int.Parse(cardDetails.ExpiryYear) < DateTime.Now.Year ||
                                string.IsNullOrEmpty(cardDetails.CVV) || cardDetails.CVV.Length != 3)
            {
                errorMessage = PaymentErrorConstants.InvalidCardDetails;
                return false;
            }
            return true;
        }

        public bool IsValidFieldForModel<T>(string fieldName)
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Any(p => p.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        // Method to get the property value dynamically
        public static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance)
                       ?.GetValue(obj);
        }
    }
}
