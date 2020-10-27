using System;

namespace SolarCoffee.Services.Product
{
    public class ServiceResponse<T>
    {
        public bool isSuccess{get;set;}
        public string Message { get; set; }
        public DateTime Time {get;set;}
        public T Data{get;set;}
    }
}