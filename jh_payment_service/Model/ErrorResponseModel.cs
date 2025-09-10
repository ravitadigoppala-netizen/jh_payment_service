using Microsoft.AspNetCore.Http;
using System.Net;

namespace jh_payment_service.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorResponseModel : ResponseModel
    {
        /// <summary>
        /// Response status code indicating the result of the API request.
        /// </summary>
        public string ErrorCode { set; get; }

        /// <summary>
        /// Response message providing additional information about the response.
        /// </summary>
        public string ErrorMessage { set; get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ErrorResponseModel Fail(string errorMessage, string errorCode)
        {
            return new ErrorResponseModel
            {
                ResponseBody = "",
                ErrorCode = errorCode,
                ErrorMessage = errorMessage ?? "Bad Request",
            };
        }
    }
}
