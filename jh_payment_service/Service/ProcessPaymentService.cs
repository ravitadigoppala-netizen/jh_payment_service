using AutoMapper;
using jh_payment_service.Constants;
using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using jh_payment_service.Model.Payments;
using jh_payment_service.Validators;

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
        private readonly IMapper _mapper;
        public ProcessPaymentService(ILogger<ProcessPaymentService> logger, IHttpClientService httpClientService,
            IValidator validator, IMapper mapper)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _validator = validator;
            _mapper = mapper;
        }

        /// <summary>
        /// Credit the user's account
        /// </summary>
        /// <param name="PaymentRequest"></param>
        public async Task<ResponseModel> CreditUserAccount(CreditPaymentRequest creditPaymentRequest)
        {
            // Logic to credit the user's account
            _logger.LogInformation($"Crediting {creditPaymentRequest.Amount} to user {creditPaymentRequest.UserId}");
            string errorMessage;
            if (!_validator.ValidateCreditPaymentRequest(creditPaymentRequest, out errorMessage))
            {
                _logger.LogError("Invalid payment request: "+ errorMessage);
                return ResponseModel.BadRequest(PaymentErrorConstants.InvalidPaymentRequest+" Error : "+ errorMessage, PaymentErrorConstants.InvalidPaymentRequestCode);
            }

            var user = await GetUserData(creditPaymentRequest.UserId);
            if (user != null)
            {
                var paymentRequest = _mapper.Map<PaymentRequest>(creditPaymentRequest);

                var response = await CreditFund(paymentRequest);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Credited balance for user {user.UserId}");
                    // Simulate success
                    _logger.LogInformation(PaymentErrorConstants.TransactionSuccess);
                    return ResponseModel.Created(response.ResponseBody, PaymentErrorConstants.TransactionSuccess);
                }
                else
                {
                    _logger.LogError("Failed to credit user's account");
                    return ResponseModel.InternalServerError(PaymentErrorConstants.FailedToCreditAccount, PaymentErrorConstants.FailedToCreditAccountCode);
                }
            }
            else
            {
                _logger.LogError(PaymentErrorConstants.UserNotFound);
                return ResponseModel.BadRequest(PaymentErrorConstants.UserNotFound, PaymentErrorConstants.UserNotFoundCode);
            }
        }

        /// <summary>
        /// Debit the user's account
        /// </summary>
        /// <param name="PaymentRequest"></param>
        public async Task<ResponseModel> DebitUserAccount(DebitPaymentRequest debitPaymentRequest)
        {
            // Logic to debit the user's account
            _logger.LogInformation($"Debiting {debitPaymentRequest.Amount} from user {debitPaymentRequest.UserId}");

            string errorMessage;
            if (!_validator.ValidateDebitPaymentRequest(debitPaymentRequest, out errorMessage))
            {
                _logger.LogError("Invalid payment request: " + errorMessage);
                return ResponseModel.BadRequest(PaymentErrorConstants.InvalidPaymentRequest + " Error : " + errorMessage, PaymentErrorConstants.InvalidPaymentRequestCode);
            }

            var user = await GetUserData(debitPaymentRequest.UserId);
            if (user != null)
            {
                var account = await GetUserAccount(debitPaymentRequest.UserId);
                if (account == null)
                {
                    _logger.LogError("User account not found");
                    return ResponseModel.BadRequest(PaymentErrorConstants.UserAccountNotFound,PaymentErrorConstants.UserAccountNotFoundCode);
                }

                if (account.Balance < debitPaymentRequest.Amount)
                {
                    _logger.LogError("Insufficient balance to process transaction");
                    return ResponseModel.BadRequest(PaymentErrorConstants.InsufficientFunds, PaymentErrorConstants.InsufficientFundsCode);
                }

                var paymentRequest = _mapper.Map<PaymentDebitRequest>(debitPaymentRequest);

                var response = await DebitFund(paymentRequest);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Debited balance for user {user.UserId}");
                    // Simulate success
                    _logger.LogInformation(PaymentErrorConstants.TransactionSuccess);
                    return ResponseModel.Created(response.ResponseBody, PaymentErrorConstants.TransactionSuccess);
                }
                else
                {
                    _logger.LogError("Failed to debit user's account");
                    return ResponseModel.InternalServerError(PaymentErrorConstants.FailedToDebitAccount, PaymentErrorConstants.FailedToDebitAccountCode);
                }                
            }
            else
            {
                _logger.LogError("User not found");
                return ResponseModel.BadRequest(PaymentErrorConstants.UserNotFound, PaymentErrorConstants.UserNotFoundCode);
            }
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

            if (userAccount == null)
            {
                _logger.LogError("User account not found");
                return ResponseModel.BadRequest(PaymentErrorConstants.UserAccountNotFound, PaymentErrorConstants.UserAccountNotFoundCode);
            }

            CheckBalanceModel checkBalance = new CheckBalanceModel
            {
                UserId = userAccount.UserId,
                FullName = userAccount.FullName,
                Balance = userAccount.Balance
            };

            return ResponseModel.Ok(checkBalance, "Your account balance fetched successfully");
        }

        /// <summary>
        /// GetAllTransactions method to get all transactions for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ResponseModel> GetAllTransactions(long userId, PageRequestModel pageRequestModel)
        {
            if(!string.IsNullOrWhiteSpace(pageRequestModel.SortBy) && !_validator.IsValidFieldForModel<Transaction>(pageRequestModel.SortBy))
            {
                return ResponseModel.BadRequest(PaymentErrorConstants.InvalidSortByField +": " + pageRequestModel.SortBy, PaymentErrorConstants.InvalidSortByFieldCode);
            }

            var transactions = await _httpClientService.GetAsync<List<Transaction>>($"v1/perops/Payment/transaction/{userId}");

            if (transactions == null)
            {
                _logger.LogError("Transactions not found");
                return ResponseModel.InternalServerError(PaymentErrorConstants.UserTransactionsNotFound, PaymentErrorConstants.UserTransactionsNotFoundCode);
            }

            return ResponseModel.Ok(transactions, "Transaction fetched successfully for the user");
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
        private async Task<ResponseModel> DebitFund(PaymentDebitRequest paymentRequest)
        {
            try
            {
                return await _httpClientService.PostAsync<PaymentDebitRequest, ResponseModel>("v1/perops/Payment/debit/"+paymentRequest.SenderUserId, paymentRequest);
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
