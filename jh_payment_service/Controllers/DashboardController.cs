using jh_payment_service.Service;
using Microsoft.AspNetCore.Mvc;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment-service/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IProcessPaymentService _processPaymentService;
        
        public DashboardController(ILogger<DashboardController> logger,
            IProcessPaymentService processPaymentService)
        {
            _logger = logger;
            _processPaymentService = processPaymentService;
        }

        /// <summary>
        /// This endpoint processes a debit payment request.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("load/{userId}")]
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
    }
}
