using jh_payment_service.Model;
using jh_payment_service.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly RefundService _refundService;

        public RefundController(RefundService refundService)
        {
            _refundService = refundService;
        }

        /// <summary>
        /// Creates a refund for a given transaction.
        /// </summary>
        /// <param name="request">Refund request details.</param>
        /// <returns>Details of the processed refund.</returns>
        [HttpPost]
        public ActionResult<RefundResponse> CreateRefund([FromBody] RefundRequest request)
        {
            var refund = _refundService.ProcessRefund(request);
            return Ok(refund);
        }

        /// <summary>
        /// Retrieves all refunds.
        /// </summary>
        /// <returns>List of refund responses.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<RefundResponse>> GetRefunds()
        {
            return Ok(_refundService.GetRefunds());
        }

        /// <summary>
        /// Retrieves details of a refund by refund ID.
        /// </summary>
        /// <param name="id">The refund identifier.</param>
        /// <returns>Refund details if found, otherwise NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<RefundResponse> GetRefundById(string id)
        {
            var refund = _refundService.GetRefundById(id);
            if (refund == null)
                return NotFound();

            return Ok(refund);
        }
    }
}
