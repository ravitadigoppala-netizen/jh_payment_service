using jh_payment_service.Model;


namespace jh_payment_service.Service
{
    /// <summary>
    ///  Implements payment-related operations.
    /// </summary>
    public class UPIPaymentHandler : IPaymentHandler
    {
        /// <summary>
        /// payment Initiate process, including validation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseModel> InitiatePaymentAsync(PaymentRequest request)
        {
            var paymentResponse = new PaymentResponse
            {
                Status = PaymentStatus.Success,
                Message = "UPI payment processed",
                PaymentId = Guid.NewGuid()
            };
            return ResponseModel.Ok(paymentResponse.ToString(), "working");
        }
    }
}
