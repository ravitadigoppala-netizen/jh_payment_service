using jh_payment_service.Model;
using jh_payment_service.Model.Entity;
using jh_payment_service.Service;
using jh_payment_service.Validators;
using Microsoft.AspNetCore.Mvc;

namespace jh_payment_service.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment/[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        private readonly ILogger<ProcessPaymentController> _logger;
        private readonly IProcessPaymentService _processPaymentService;
        private readonly IValidator _validator;
        public ProcessPaymentController(ILogger<ProcessPaymentController> logger,
            IProcessPaymentService processPaymentService, IValidator validator)
        {
            _logger = logger;
            _processPaymentService = processPaymentService;
            _validator = validator;
        }

        /// <summary>
        /// This endpoint processes a credit payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost("credit-payment")]
        public ResponseModel CreditPayment([FromBody]PaymentRequest paymentRequest)
        {
            try
            {
                if (!_validator.ValidatePaymentRequest(paymentRequest))
                {
                    _logger.LogError("Invalid payment request");
                    return ResponseModel.BadRequest("Invalid payment request");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    FromUserId = paymentRequest.SenderUserId,
                    ToUserId = paymentRequest.ReceiverUserId,
                    Amount = paymentRequest.Amount,
                    CreatedAt = DateTime.UtcNow,
                    Type = TransactionType.Credit
                };

                _processPaymentService.CreditUserAccount(transaction);
                _logger.LogInformation($"Credit Payment processed successfully for transaction {transaction.TransactionId}");
                return ResponseModel.Ok(transaction, "Credit Payment processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing credit payment");
                return ResponseModel.InternalServerError("An error occured while processing credit payment");
            }
        }

        /// <summary>
        /// This endpoint processes a debit payment request.
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        [HttpPost("debit-payment")]
        public ResponseModel DebitPayment([FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                _logger.LogInformation("Debit payment request received");
                if (!_validator.ValidatePaymentRequest(paymentRequest))
                {
                    _logger.LogError("Invalid payment request");
                    return ResponseModel.BadRequest("Invalid payment request");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    FromUserId = paymentRequest.SenderUserId,
                    ToUserId = paymentRequest.ReceiverUserId,
                    Amount = paymentRequest.Amount,
                    CreatedAt = DateTime.UtcNow,
                    Type = TransactionType.Debit
                };

                _processPaymentService.DebitUserAccount(transaction);
                _logger.LogInformation($"Debit Payment processed successfully for transaction {transaction.TransactionId}");
                return ResponseModel.Ok(transaction, "Debit Payment processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing debit payment");
                return ResponseModel.InternalServerError("An error occured while processing debit payment");
            }
        }
    }
}
