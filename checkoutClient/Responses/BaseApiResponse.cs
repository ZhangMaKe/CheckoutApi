using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public abstract class BaseApiResponse
    {
        public string Message { get; set; }

        protected BaseApiResponse(string message)
        {
            Message = message;
        }
    }

    
}
