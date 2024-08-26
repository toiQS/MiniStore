using System.Collections.Specialized;

namespace MiniStore.API.Models
{
    public class ServiceResult<T>
    {
        public string Messenger { get; set; } = string.Empty;
        public bool Success { get; set; }
        public T? Values { get; set; }
        public int StatusCode { get; set; }

        public ServiceResult() { }

        public ServiceResult(T? value)
        {
            Values = value;
            Messenger = "This action is successed.";
            Success = true;
        }

        public static ServiceResult<T> SuccessResult(T? value)
        {
            return new ServiceResult<T>(value);
        }

        public ServiceResult(string messager)
        {
            Messenger = messager;
            Success = false;
            

        }

        public static ServiceResult<T> FailedResult(string messager)
        {
            return new ServiceResult<T>(messager);
        }
    }
}
