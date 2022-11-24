using System;

namespace dotbook_api.Models
{
    public class ResponceData<T>
    {
        public T Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public static ResponceData<T> Success(T data)
        {
            return new ResponceData<T>()
            {
                Data = data,
                Message = string.Empty
            };
        }
        public static ResponceData<T> Error(Exception ex)
        {
            return new ResponceData<T>()
            {
                Data = default(T),
                Message = ex.Message
            };
        }
    }
}
