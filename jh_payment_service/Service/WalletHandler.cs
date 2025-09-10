
using jh_payment_service.Model;
using jh_payment_service.Model.Entity;

namespace jh_payment_service.Service
{
    /// <summary>
    ///  Implements payment-related operations.
    /// </summary>
    public class WalletHandler : IPaymentHandler
    {
        private readonly IHttpClientService _httpClientService;

        public WalletHandler(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }


        /// <summary>
        /// payment Initiate process, including validation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel> InitiatePaymentAsync(PaymentRequest request)
        {
            try
            {
                var sender = await _httpClientService.GetAsync<User>($"v1/perops/user/getuser/{request.SenderUserId}");
            }
            catch
            {
                return ErrorResponseModel.Fail($"User with id: {request.SenderUserId} not found", "AUTH001");
            }

            try
            {
                var receiver = await _httpClientService.GetAsync<User>($"v1/perops/user/getuser/{request.ReceiverUserId}");
            }
            catch
            {
                return ErrorResponseModel.Fail("User not found", "AUTH001");
            }

            var paymentResponse = new PaymentResponse
            {
                Status = PaymentStatus.Success,
                Message = "Wallet payment processed",
                PaymentId = Guid.NewGuid()
            };

            return ResponseModel.Ok(paymentResponse.ToString(), "working");
        }
    }
}
