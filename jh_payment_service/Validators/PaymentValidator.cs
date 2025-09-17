using jh_payment_service.Model;
using jh_payment_service.Model.Payments;

namespace jh_payment_service.Validators
{
    /// <summary>
    /// This class implements validation logic for payment requests.
    /// </summary>
    public class PaymentValidator : IPaymentValidator
    {
        /// <summary>
        /// Validates the given card to card payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ValidateCardToCardPaymentRequest(CardPaymentRequest request, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (request.Amount <= 0)
            {
                errorMessage = "Amount must be greater than zero.";
                return false;
            }
            if (request.CardDetails.CardNumber == request.ReceiverCardNumber)
            {
                errorMessage = "Sender and receiver cardNumber cannot be the same";
                return false;
            }
            if (request.PaymentMethod != PaymentMethodType.Card)
            {
                errorMessage = "Payment method should be card";
                return false;
            }
            if (request.PaymentMethod == PaymentMethodType.Card)
            {
                if (!ValidateCardDetails(request.CardDetails, ref errorMessage))
                    return false;
            }
            
            if (string.IsNullOrEmpty(request.ReceiverCardNumber))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the given payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ValidatePaymentRequest(InitialPaymentModel paymentRequest, out string errorMessage)
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
                errorMessage = "Invalid card details.";
                return false;
            }
            return true;
        }
    }
}
