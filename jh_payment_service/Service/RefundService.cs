using jh_payment_service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Provides services for managing refunds.
    /// </summary>
    public class RefundService
    {
        private readonly List<RefundResponse> _refunds = new();
        private readonly HttpClient _httpClient;
      
        private readonly string _version = "1"; // example, can be read from config
        private readonly int _userId;           // set from outside
        private readonly int _transactionId;    // set from outside
        private readonly string _apiBase = "/api/v{0}/perops/Payment/refund"; // {0} = version
        private RefundRequest _refundRequest;

        public RefundService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Processes a new refund request for a specific user and transaction.
        /// </summary>
        public async Task<RefundResponse> ProcessRefund()
        {
            // Build API URL using class-level fields
            var apiUrl = string.Format(_apiBase, _version) + $"/{_userId}/{_transactionId}";

            // Send request
            var httpResponse = await _httpClient.PostAsJsonAsync(apiUrl, _refundRequest);

            // Ensure success
            httpResponse.EnsureSuccessStatusCode();

            // Parse response
            var refundResponse = await httpResponse.Content.ReadFromJsonAsync<RefundResponse>();

            if (refundResponse != null)
            {
                _refunds.Add(refundResponse);
                return refundResponse;
            }

            throw new Exception("Refund API did not return a valid response.");
        }


        /// <summary>
        /// Retrieves all refund transactions for a specific user.
        /// </summary>
        public async Task<IEnumerable<RefundResponse>> GetRefunds(string version, int userId, int transactionId)
        {
            var apiUrl = string.Format(_apiBase, version) + $"/{userId}/{transactionId}";

            // Response variable
            var refundList = await _httpClient.GetFromJsonAsync<List<RefundResponse>>(apiUrl);

            if (refundList != null)
            {
                _refunds.Clear();
                _refunds.AddRange(refundList);
                return refundList;
            }

            return Enumerable.Empty<RefundResponse>();
        }

        /// <summary>
        /// Retrieves a specific refund by transaction for a given user.
        /// </summary>
        public async Task<RefundResponse?> GetRefundById(string version, int userId, int transactionId)
        {
            var apiUrl = string.Format(_apiBase, version) + $"/{userId}/{transactionId}";

            // Response variable
            var refundResponse = await _httpClient.GetFromJsonAsync<RefundResponse>(apiUrl);
            return refundResponse;
        }
    }
}
