using jh_payment_service.Model;
using jh_payment_service.Services;

namespace jh_payment_service.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing payment endpoints.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBalanceService _balanceService;
        public PaymentController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        /// <summary>
        /// Provides API endpoints for checking balance of the account using accountId.
        /// </summary>

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccountBalance(int accountId)
        {
            var balance = await _balanceService.GetBalanceAsync(accountId);
            var paymentResponse = new PaymentResponse();
            if (balance == null)
            {
                return ResponseModel.BadRequest(paymentResponse, "Account with ID {accountId} not found.");
            }

            return ResponseModel.Ok(paymentResponse, "Working");
        }
    }
}
