
using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using System.Reflection;
using System.Transactions;

namespace jh_payment_service.Service
{
    /// <summary>
    ///  Implements payment-related operations.
    /// </summary>
    public class CardPaymentHandler : IPaymentHandler
    {
        private readonly IHttpClientService _httpClientService;
        public CardPaymentHandler(IHttpClientService httpClientService)
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
                return ErrorResponseModel.Fail("User not found", "AUTH001");
            }


            try
            {
                var receiver = await _httpClientService.GetAsync<User>($"v1/perops/user/getuser/{request.ReceiverUserId}");
            }
            catch
            {
                return ErrorResponseModel.Fail("User not found", "AUTH001");
            }


            var senderAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{request.SenderUserId}");
            var receiverAccount = await _httpClientService.GetAsync<UserAccount>($"v1/perops/Payment/checkbalance/{request.ReceiverUserId}");

            if (senderAccount.Balance < request.Amount)
                return ErrorResponseModel.Fail("Insufficient balance", "AUTH001");

            var response = await _httpClientService.PutAsync<PaymentRequest, ResponseModel>($"v1/perops/Payment/transfer", new PaymentRequest
            {
                SenderUserId = request.SenderUserId,
                ReceiverUserId = request.ReceiverUserId,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod
            });

            return ResponseModel.Ok(response, "Card payment processed");
        }
    }
}
