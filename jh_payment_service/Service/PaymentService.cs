using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using jh_payment_service.Model.Payments;
using jh_payment_service.Validators;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Implements payment-related operations.
    /// </summary>
    public class PaymentService : IPaymentService
    {

        private readonly IHttpClientService _httpClientService;
        private readonly ILogger<PaymentService> _logger;
        private readonly IPaymentValidator _validator;

        public PaymentService(IHttpClientService httpClientService, ILogger<PaymentService> logger, IPaymentValidator paymentValidator)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _validator = paymentValidator;
        }

        /// <summary>
        /// card payment Initiate process, including validation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>s
        public async Task<ResponseModel> ProcessCardPaymentAsync(CardPaymentRequest request)
        {
            string errorMessage;
            if (!_validator.ValidateCardToCardPaymentRequest(request, out errorMessage))
            {
                _logger.LogError("Invalid payment request: " + errorMessage);
                return ResponseModel.BadRequest("Invalid payment request: " + errorMessage);
            }

            var senderAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{request.SenderUserId}");

            if (senderAccount.Balance < request.Amount)
            {
                return ErrorResponseModel.Fail("Insufficient balance", "PAY001 ");
            }

            var response = await _httpClientService.PostAsync<CardPaymentRequest, ResponseModel>($"v1/perops/Payment/transfer/card", new CardPaymentRequest
            {
                SenderUserId = request.SenderUserId,
                ReceiverUserId = request.ReceiverUserId,
                CardDetails = request.CardDetails,
                ReceiverCardNumber = request.ReceiverCardNumber,
                Amount = request.Amount,
                PaymentMethod = PaymentMethodType.Card,
            });

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation($"Credited balance for user {request.ReceiverUserId}");
            }
            else
            {
                _logger.LogError("Failed to credit user's account");
                return ResponseModel.InternalServerError("Failed to credit user's account");
            }

            return ResponseModel.Ok(request, "Transaction completed successfully");
        }

        /// <summary>
        /// payment Initiate process, including validation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel> ProcessPaymentAsync(InitialPaymentModel request)
        {
            string errorMessage;
            if (!_validator.ValidatePaymentRequest(request, out errorMessage))
            {
                _logger.LogError("Invalid payment request: " + errorMessage);
                return ResponseModel.BadRequest("Invalid payment request: " + errorMessage);
            }
            var sender = await _httpClientService.GetAsync<User>($"v1/perops/user/getuser/{request.SenderUserId}");

            if (sender == null)
            {
                return ErrorResponseModel.Fail($"Sender with id: {request.SenderUserId} not found", "NOTIF001 ");
            }

            var receiver = await _httpClientService.GetAsync<User>($"v1/perops/user/getuser/{request.ReceiverUserId}");
            if (receiver == null)
            {
                return ErrorResponseModel.Fail($"Receiver with id: {request.ReceiverUserId} not found", "NOTIF001 ");
            }

            var senderAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{request.SenderUserId}");
            var receiverAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{request.ReceiverUserId}");

            if (senderAccount.Balance < request.Amount)
            {
                return ErrorResponseModel.Fail("Insufficient balance", "PAY001 ");
            }

            var response = await _httpClientService.PostAsync<PaymentRequest, ResponseModel>($"v1/perops/Payment/transfer", new PaymentRequest
            {
                SenderUserId = request.SenderUserId,
                ReceiverUserId = request.ReceiverUserId,
                Amount = request.Amount,
            });

            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                _logger.LogInformation($"Credited balance for user {request.ReceiverUserId}");
            }
            else
            {
                _logger.LogError("Failed to credit user's account");
                return ResponseModel.InternalServerError("Failed to credit user's account");
            }

            return ResponseModel.Ok(request, "Transaction completed successfully");
        }
    }
}
