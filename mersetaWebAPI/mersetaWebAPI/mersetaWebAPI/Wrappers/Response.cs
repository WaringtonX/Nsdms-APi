﻿namespace mersetaWebAPI.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(string message,string error)
        {
            Succeeded = false;
            Message = message;
            Errors = error;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Errors { get; set; }
        public T Data { get; set; }
    }
}
