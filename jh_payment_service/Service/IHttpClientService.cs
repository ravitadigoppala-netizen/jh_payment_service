namespace jh_payment_service.Service
{
    public interface IHttpClientService
    {
        Task<T?> GetAsync<T>(string url);
        Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data);
        Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data);
        Task<bool> DeleteAsync(string url);
    }
}
