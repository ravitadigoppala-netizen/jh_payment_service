using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using jh_payment_service.Validators;
using System.ComponentModel.DataAnnotations;

namespace jh_payment_service.Service
{
    /// <summary>
    /// This service handles processing of payments including crediting and debiting user accounts.
    /// </summary>
    public class ProcessPaymentService : IProcessPaymentService
    {
        private readonly ILogger<ProcessPaymentService> _logger;
        private readonly IHttpClientService _httpClientService;
        private readonly IValidator _validator;
        public ProcessPaymentService(ILogger<ProcessPaymentService> logger, IHttpClientService httpClientService,
            IValidator validator)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _validator = validator;
        }

        /// <summary>
        /// Credit the user's account
        /// </summary>
        /// <param name="PaymentRequest"></param>
        public async Task<ResponseModel> CreditUserAccount(PaymentRequest paymentRequest)
        {
            // Logic to credit the user's account
            _logger.LogInformation($"Crediting {paymentRequest.Amount} to user {paymentRequest.SenderUserId}");
            string errorMessage;
            if (!_validator.ValidatePaymentRequest(paymentRequest, out errorMessage))
            {
                _logger.LogError("Invalid payment request: "+ errorMessage);
                return ResponseModel.BadRequest("Invalid payment request: "+ errorMessage);
            }

            var user = await GetUserData(paymentRequest.SenderUserId);
            if (user != null)
            {
                var response = await CreditFund(paymentRequest);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Credited balance for user {user.UserId}");
                }
                else
                {
                    _logger.LogError("Failed to credit user's account");
                    return ResponseModel.InternalServerError("Failed to credit user's account");
                }
            }
            else
            {
                _logger.LogError("User not found");
                return ResponseModel.BadRequest("User not found");
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
            return ResponseModel.Ok(paymentRequest, "Transaction completed successfully");
        }

        /// <summary>
        /// Debit the user's account
        /// </summary>
        /// <param name="PaymentRequest"></param>
        public async Task<ResponseModel> DebitUserAccount(PaymentRequest paymentRequest)
        {
            // Logic to debit the user's account
            _logger.LogInformation($"Debiting {paymentRequest.Amount} from user {paymentRequest.SenderUserId}");

            string errorMessage;
            if (!_validator.ValidatePaymentRequest(paymentRequest, out errorMessage))
            {
                _logger.LogError("Invalid payment request: " + errorMessage);
                return ResponseModel.BadRequest("Invalid payment request: " + errorMessage);
            }

            var user = await GetUserData(paymentRequest.SenderUserId);
            if (user != null)
            {
                var account = await GetUserAccount(paymentRequest.SenderUserId);
                if (account == null)
                {
                    _logger.LogError("User account not found");
                    return ResponseModel.BadRequest("User account not found");
                }

                if (account.Balance < paymentRequest.Amount)
                {
                    _logger.LogError("Insufficient balance to process transaction");
                    return ResponseModel.BadRequest("Insufficient balance to process transaction");
                }

                var response = await DebitFund(paymentRequest);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Debited balance for user {user.UserId}");
                }
                else
                {
                    _logger.LogError("Failed to debit user's account");
                    return ResponseModel.InternalServerError("Failed to debit user's account");
                }                
            }
            else
            {
                _logger.LogError("User not found");
                return ResponseModel.BadRequest("User not found");
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
            return ResponseModel.Ok(paymentRequest, "Transaction completed successfully");
        }

        /// <summary>
        /// GetAccountBalance method to check user account balance
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ResponseModel> GetAccountBalance(long userId)
        {
            var userAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{userId}");
            // var response = await _httpClientService.PostAsync<User, string>($"v1/perops/user/adduser", new User { });

            if (userAccount == null)
            {
                throw new Exception("Fail to get user account detail");
            }

            return ResponseModel.Ok(userAccount, "Your account balance");
        }

        /// <summary>
        /// Get user data from DB
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<User> GetUserData(long userId)
        {
            try
            {
                return await _httpClientService.GetAsync<User>("v1/perops/user/getuser/" + userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User not found");
            }
            return null;
        }

        /// <summary>
        /// Credit fund to user account
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        private async Task<ResponseModel> CreditFund(PaymentRequest paymentRequest)
        {
            try
            {
                return await _httpClientService.PostAsync<PaymentRequest, ResponseModel>("v1/perops/Payment/credit", paymentRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to credit");
            }
            return null;
            
        }

        /// <summary>
        /// Debit fund from user account
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        private async Task<ResponseModel> DebitFund(PaymentRequest paymentRequest)
        {
            try
            {
                return await _httpClientService.PutAsync<PaymentRequest, ResponseModel>("v1/perops/Payment/debit/"+paymentRequest.SenderUserId, paymentRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to debit");
            }
            return null;
        }

        /// <summary>
        /// Get user account details from DB
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<UserAccount> GetUserAccount(long userId)
        {
            try
            {
                return await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{userId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get user account");
            }
            return null;
                       
        }
    }
}
