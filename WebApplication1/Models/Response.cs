using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Response<T>
    {
        public bool status { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
    }
}