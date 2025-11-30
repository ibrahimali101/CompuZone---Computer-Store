using System.Collections.Generic;

namespace CompuZone.Application.Wapper
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public Response()
        {
            Succeeded = true;
        }

        // Constructor for Success
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        // Constructor for Failure
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
        }
    }
}