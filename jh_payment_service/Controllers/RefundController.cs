using jh_payment_service.Model;
using jh_payment_service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment-service/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly RefundService _refundService;

        public RefundController(RefundService refundService)
        {
            _refundService = refundService;
        }

        /// <summary>
        /// Process a new refund request for a user and transaction.
        /// </summary>
        [HttpPost("refund/{userId}/{transactionId}")]
        public async Task<ResponseModel> ProcessRefund([FromRoute]long userId, [FromRoute] string transactionId)
        {
            return await _refundService.ProcessRefund(userId, transactionId);
        }

        ///// <summary>
        ///// Get all refund transactions for a user and transaction.
        ///// </summary>
        //[HttpGet("refunds/{version}/{userId}/{transactionId}")]
        //public async Task<IActionResult> GetRefunds(string version, int userId, int transactionId)
        //{
        //    var refundList = await _refundService.GetRefunds(version, userId, transactionId);
        //    return Ok(refundList);
        //}

        ///// <summary>
        ///// Get refund details for a specific user and transaction.
        ///// </summary>
        //[HttpGet("refund/{version}/{userId}/{transactionId}")]
        //public async Task<IActionResult> GetRefundById(string version, int userId, int transactionId)
        //{
        //    var refundResponse = await _refundService.GetRefundById(version, userId, transactionId);

        //    if (refundResponse == null)
        //        return NotFound("Refund not found");

        //    return Ok(refundResponse);
        //}
    }
}
