

using jh_payment_service.Model;
using jh_payment_service.Model.Payments;
using jh_payment_service.Model.UserEntity;
using Microsoft.Extensions.Logging;

namespace jh_payment_service.Service
{
    /// <summary>
    /// This service handles processing of payments including crediting and debiting user accounts.
    /// </summary>
    public class ProcessPaymentService : IProcessPaymentService
    {
        private readonly ILogger<ProcessPaymentService> _logger;
        public ProcessPaymentService(ILogger<ProcessPaymentService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Credit the user's account
        /// </summary>
        /// <param name="transaction"></param>
        public void CreditUserAccount(Transaction transaction)
        {
            // Logic to credit the user's account
            _logger.LogInformation($"Crediting {transaction.Amount} to user {transaction.ToUserId}");
            var user = GetUserData();
            if (user != null)
            {
                if (transaction.Amount <= 0)
                {
                    _logger.LogError("Invalid transaction amount");
                    transaction.Status = PaymentStatus.Failed;
                    return;
                }

                user.Balance += transaction.Amount;
                _logger.LogInformation($"New balance for user {user.UserId} is {user.Balance}");
            }
            else
            {
                _logger.LogError("User not found");
                transaction.Status = PaymentStatus.Failed;
                return;
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
            transaction.Status = PaymentStatus.Success;
            transaction.CompletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Debit the user's account
        /// </summary>
        /// <param name="transaction"></param>
        public void DebitUserAccount(Transaction transaction)
        {
            // Logic to debit the user's account
            _logger.LogInformation($"Debiting {transaction.Amount} from user {transaction.FromUserId}");
            var user = GetUserData();
            if (user != null)
            {
                if (user.Balance < transaction.Amount)
                {
                    _logger.LogError("Insufficient balance to process transaction");
                    transaction.Status = PaymentStatus.Failed;
                    return;
                }

                user.Balance -= transaction.Amount;
                _logger.LogInformation($"New balance for user {user.UserId} is {user.Balance}");
            }
            else
            {
                _logger.LogError("User not found");
                transaction.Status = PaymentStatus.Failed;
                return;
            }
            // Simulate success
            _logger.LogInformation("Transaction completed successfully");
            transaction.Status = PaymentStatus.Success;
            transaction.CompletedAt = DateTime.UtcNow;
        }

        UserAccount GetUserData()
        {
            return new UserAccount
            {
                UserId = Guid.NewGuid(),
                FullName = "John Doe",
                Email = "john@abc.com",
                Balance = 1000,
                MobileNumber = "1234567890"
            };
        }
    }
}
