using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class BaseApiResponse
    {
        public string Message { get; set; }

        public BaseApiResponse(string message)
        {
            Message = message;
        }
    }

    
}
