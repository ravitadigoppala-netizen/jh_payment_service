namespace jh_payment_service.Services
{

    public interface IBalanceService
    {
        Task<decimal?> GetBalanceAsync(int accountId);
    }

}
