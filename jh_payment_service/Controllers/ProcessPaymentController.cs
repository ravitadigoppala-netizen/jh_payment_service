using jh_payment_service.Model;
using jh_payment_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ps/[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        private readonly ILogger<ProcessPaymentController> _logger;
        private readonly IProcessPaymentService _processPaymentService;

        public ProcessPaymentController(ILogger<ProcessPaymentController> logger,
            IProcessPaymentService processPaymentService)
        {
            _logger = logger;
            _processPaymentService = processPaymentService;
        }

        /// <summary>
        /// This endpoint processes a credit payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost("credit-payment")]
        public async Task<IActionResult> CreditPayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var response = await _processPaymentService.CreditUserAccount(paymentRequest);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing credit payment");
                return StatusCode(500, ResponseModel.InternalServerError("An error occured while processing credit payment"));
            }
        }

        /// <summary>
        /// This endpoint processes a debit payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost("debit-payment")]
        public async Task<IActionResult> DebitPayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                _logger.LogInformation("Debit payment request received");

                var response = await _processPaymentService.DebitUserAccount(paymentRequest);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing debit payment");
                return StatusCode(500, ResponseModel.InternalServerError("An error occured while processing debit payment"));
            }
        }

        /// <summary>
        /// This endpoint processes a check balance request.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("check-balance/{userId}")]
        public async Task<ResponseModel> CheckBalance([FromRoute] long userId)
        {
            _logger.LogInformation("Check Balance request received");
            return await _processPaymentService.GetAccountBalance(userId);
        }
    }
}
