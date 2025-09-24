using jh_payment_service.Model;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Provides services for managing refunds.
    /// </summary>
    public class RefundService
    {
        private readonly List<RefundResponse> _refunds = new();

        private readonly string _version = "1"; // example, can be read from config
        private readonly int _userId;           // set from outside
        private readonly int _transactionId;    // set from outside
        private readonly string _apiBase = "/api/v{0}/perops/Payment/refund"; // {0} = version
        private RefundRequest _refundRequest;
        private readonly IHttpClientService _httpClientService;

        public RefundService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        /// <summary>
        /// Processes a new refund request for a specific user and transaction.
        /// </summary>
        public async Task<ResponseModel> ProcessRefund(string userEmail, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                return ErrorResponseModel.Fail("Invalid refund request", "REF004");
            }

            Guid.TryParse(transactionId, out Guid transactinGuid);

            // Build API URL using class-level fields
            var apiUrl = "v1/perops/Payment/refund" + $"/{userEmail}/{transactinGuid}";

            // Send request
            var response = await _httpClientService.GetAsync<string>(apiUrl);

            if (response == null)
                return ErrorResponseModel.Fail("Transaction failed", "REF005");

            return ResponseModel.Ok(response, "Success");
        }

        /// <summary>
        /// Processes a new partial refund request for a specific user and transaction.
        /// </summary>
        public async Task<ResponseModel> ProcessPartialRefund(string userEmail, string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                return ResponseModel.BadRequest("Invalid partial refund request", "REF004");
            }

            Guid.TryParse(transactionId, out Guid transactinGuid);

            // Build API URL using class-level fields
            var apiUrl = "v1/perops/Payment/partial-refund" + $"/{userEmail}/{transactinGuid}";

            // Send request
            var response = await _httpClientService.GetAsync<string>(apiUrl);

            if (response == null)
                return ResponseModel.BadRequest("Transaction failed", "REF005");

            return ResponseModel.Ok(response, "Success");
        }


        ///// <summary>
        ///// Retrieves all refund transactions for a specific user.
        ///// </summary>
        //public async Task<IEnumerable<RefundResponse>> GetRefunds(string version, int userId, int transactionId)
        //{
        //    var apiUrl = string.Format(_apiBase, version) + $"/{userId}/{transactionId}";

        //    // Response variable
        //    var refundList = await _httpClient.GetFromJsonAsync<List<RefundResponse>>(apiUrl);

        //    if (refundList != null)
        //    {
        //        _refunds.Clear();
        //        _refunds.AddRange(refundList);
        //        return refundList;
        //    }

        //    return Enumerable.Empty<RefundResponse>();
        //}

        ///// <summary>
        ///// Retrieves a specific refund by transaction for a given user.
        ///// </summary>
        //public async Task<RefundResponse?> GetRefundById(string version, int userId, int transactionId)
        //{
        //    var apiUrl = string.Format(_apiBase, version) + $"/{userId}/{transactionId}";

        //    // Response variable
        //    var refundResponse = await _httpClient.GetFromJsonAsync<RefundResponse>(apiUrl);
        //    return refundResponse;
        //}
    }
}
