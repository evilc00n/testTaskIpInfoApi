namespace IpInfo.Domain.Result
{

    /// <summary>
    /// Нужен для удобного представления данных из одной части приложения в другую
    /// </summary>
    public class BaseResult
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess => ErrorMessage == null;

        public int ErrorCode { get; set; }

    }


    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }


        public BaseResult(string errorMessage, int errorCode, T data)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            Data = data;
        }

        public BaseResult() { }
    }
}
