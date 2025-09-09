using jh_payment_service.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace jh_payment_service.Service
{
    /// <summary>
    /// Provides services for managing refunds.
    /// </summary>
    public class RefundService
    {
        private readonly List<RefundResponse> _refunds = new();

        /// <summary>
        /// Processes a new refund request.
        /// </summary>
        /// <param name="request">Refund request details.</param>
        /// <returns>Refund response object with refund details.</returns>
        public RefundResponse ProcessRefund(RefundRequest request)
        {
            var refund = new RefundResponse
            {
                RefundId = Guid.NewGuid().ToString(),
                TransactionId = request.TransactionId,
                Amount = request.Amount,
                Reason = request.Reason,
                CreatedAt = DateTime.UtcNow
            };

            _refunds.Add(refund);
            return refund;
        }

        /// <summary>
        /// Retrieves all refund transactions.
        /// </summary>
        /// <returns>List of refunds.</returns>
        public IEnumerable<RefundResponse> GetRefunds()
        {
            return _refunds;
        }

        /// <summary>
        /// Retrieves a refund by its identifier.
        /// </summary>
        /// <param name="refundId">Refund identifier.</param>
        /// <returns>Refund response object or null.</returns>
        public RefundResponse GetRefundById(string refundId)
        {
            return _refunds.FirstOrDefault(r => r.RefundId == refundId);
        }
    }
}
