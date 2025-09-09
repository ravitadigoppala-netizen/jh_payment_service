namespace jh_payment_service.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IAccountRepository _accountRepository; // Or your DbContext

        public BalanceService(IAccountRepository accountRepository) // Inject your data access layer
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal?> GetBalanceAsync(int accountId)
        {
            // Example: Retrieve balance from a repository or directly from DbContext
            var account = await _accountRepository.GetByIdAsync(accountId);

            if (account == null)
            {
                return null; // Account not found
            }

            return account.Balance;
        }
    }
}
