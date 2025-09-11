using jh_payment_service.Model;
using jh_payment_service.Model.Payments;
using jh_payment_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace jh_payment_service.Controllers
{
    /// <summary>
    /// Provides API endpoints for payment related operations, such as initiate payment.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment-service/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        ///Provides API endpoints for payment related operations, such as initiate payment.
        /// </summary>
        [HttpPost("wallet/transfer/initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] InitialPaymentModel request)
        {
            try
            {
                var response = await _paymentService.ProcessPaymentAsync(request);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseModel.InternalServerError("An error occured while processing credit payment"));
            }
        }
    }
}
