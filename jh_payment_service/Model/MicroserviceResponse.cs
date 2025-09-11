namespace jh_payment_service.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MicroserviceResponse<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public T? ResponseBody { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T? GetDefault()
        {
            return default;
        }
    }
}
