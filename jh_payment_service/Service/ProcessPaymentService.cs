using jh_payment_service.Model;
using jh_payment_service.Model.Entity;

namespace jh_payment_service.Service
{
    /// <summary>
    /// This service handles processing of payments including crediting and debiting user accounts.
    /// </summary>
    public class ProcessPaymentService : IProcessPaymentService
    {
        private readonly ILogger<ProcessPaymentService> _logger;
        private readonly IHttpClientService _httpClientService;
        public ProcessPaymentService(ILogger<ProcessPaymentService> logger, IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        /// <summary>
        /// Credit the user's account
        /// </summary>
        /// <param name="transaction"></param>
        public async Task CreditUserAccount(Transaction transaction)
        {
            // Logic to credit the user's account
            _logger.LogInformation($"Crediting {transaction.Amount} to user {transaction.ToUserId}");
            var user = await GetUserData();
            if (user != null)
            {
                if (transaction.Amount <= 0)
                {
                    _logger.LogError("Invalid transaction amount");
                    return;
                }

                user.Balance += transaction.Amount;
                _logger.LogInformation($"New balance for user {user.UserId} is {user.Balance}");
            }
            else
            {
                _logger.LogError("User not found");
                return;
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
        }

        /// <summary>
        /// Debit the user's account
        /// </summary>
        /// <param name="transaction"></param>
        public async Task DebitUserAccount(Transaction transaction)
        {
            // Logic to debit the user's account
            _logger.LogInformation($"Debiting {transaction.Amount} from user {transaction.FromUserId}");
            var user = await GetUserData();
            if (user != null)
            {
                if (user.Balance < transaction.Amount)
                {
                    _logger.LogError("Insufficient balance to process transaction");
                    return;
                }

                user.Balance -= transaction.Amount;
                _logger.LogInformation($"New balance for user {user.UserId} is {user.Balance}");
            }
            else
            {
                _logger.LogError("User not found");
                return;
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
        }

        private async Task<UserAccount> GetUserData()
        {
            var user = await _httpClientService.GetAsync<User>("v1/perops/user/getuser/1");
            // var response = await _httpClientService.PostAsync<User, string>("v1/perops/user/adduser", new User() { });

            return await Task.FromResult(new UserAccount
            {
                UserId = 1,
                FullName = "John Doe",
                Email = "john@abc.com",
                Balance = 1000,
                MobileNumber = "1234567890"
            });
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
            if(userAccount == null)
            {
                throw new Exception("Fail to get user account detail");
            }

            return ResponseModel.Ok(userAccount, "Your account balance");
        }
    }
}
