using jh_payment_service.Model;
using jh_payment_service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/payment-service/[controller]")]
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
        /// <param name="creditPaymentRequest"></param>
        /// <returns></returns>
        [HttpPost("credit")]
        public async Task<IActionResult> CreditPayment([FromBody] CreditPaymentRequest creditPaymentRequest)
        {
            try
            {
                var response = await _processPaymentService.CreditUserAccount(creditPaymentRequest);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing credit payment");
                throw;
            }
        }

        /// <summary>
        /// This endpoint processes a debit payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost("debit")]
        public async Task<IActionResult> DebitPayment([FromBody] DebitPaymentRequest debitPaymentRequest)
        {
            try
            {
                _logger.LogInformation("Debit payment request received");

                var response = await _processPaymentService.DebitUserAccount(debitPaymentRequest);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing debit payment");
                throw;
            }
        }

        /// <summary>
        /// This endpoint processes a check balance request.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("check-balance/{userId}")]
        public async Task<IActionResult> CheckBalance([FromRoute] long userId)
        {
            try
            {
                _logger.LogInformation("Check balance request received");
                var response = await _processPaymentService.GetAccountBalance(userId);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking account balance");
                throw;
            }
        }

        /// <summary>
        /// This endpoint processes a check balance request.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("transaction/{userId}")]
        public async Task<IActionResult> GetAllTransactions([FromRoute] long userId, [FromQuery] PageRequestModel pageRequestModel)
        {
            try
            {
                _logger.LogInformation("Get all transaction request received");
                var response = await _processPaymentService.GetAllTransactions(userId, pageRequestModel);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking account balance");
                throw;
            }
        }
    }
}
