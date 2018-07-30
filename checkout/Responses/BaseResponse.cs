using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Responses
{
    public abstract class BaseResponse<T>
    {
        public string Message { get; set; }
        public T Entity { get; set; }

        public BaseResponse(string message)
        {
            Message = message;
        }
    }
}
