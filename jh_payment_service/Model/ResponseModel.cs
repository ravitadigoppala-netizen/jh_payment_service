using System.Net;

namespace jh_payment_service.Model
{
    /// <summary>
    /// This class represents a standard response model for API responses.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Response body containing the actual data or content of the response.
        /// </summary>
        public object ResponseBody { set; get; }

        /// <summary>
        /// Response status code indicating the result of the API request.
        /// </summary>
        public HttpStatusCode StatusCode { set; get; }

        /// <summary>
        /// Response message providing additional information about the response.
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// Response error code for identifying specific errors.
        /// </summary>
        public string ErrorCode { set; get; }

        /// <summary>
        /// This method creates a successful response model with status code 200 (OK).
        /// </summary>
        /// <param name="reponse"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResponseModel Ok(dynamic reponse, string message = "No content")
        {
            return new ResponseModel
            {
                ResponseBody = reponse,
                StatusCode = HttpStatusCode.OK,
                Message = message ?? "Successfully completed"
            };
        }

        /// <summary>
        /// This method creates a successful response model with status code 200 (OK).
        /// </summary>
        /// <param name="reponse"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ResponseModel Created(dynamic reponse, string message = "No content")
        {
            return new ResponseModel
            {
                ResponseBody = reponse,
                StatusCode = HttpStatusCode.Created,
                Message = message ?? "Successfully completed"
            };
        }

        /// <summary>
        /// This method creates a response model for bad requests with status code 400 (Bad Request).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ResponseModel BadRequest(string message, string errorCode = null)
        {
            return new ResponseModel
            {
                ResponseBody = "No content",
                StatusCode = HttpStatusCode.BadRequest,
                Message = message ?? "Bad Request",
                ErrorCode = errorCode,
            };
        }

        /// <summary>
        /// This method creates an internal server error response model with status code 500 (Internal Server Error).
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ResponseModel InternalServerError(string message, string errorCode = null)
        {
            return new ResponseModel
            {
                ResponseBody = "No content",
                StatusCode = HttpStatusCode.InternalServerError,
                Message = message ?? "Internal Server Error",
                ErrorCode = errorCode,
            };
        }
    }
}
