using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Requests
{
    public abstract class BaseRequest
    {
        public HttpMethod HttpMethod { get; set; }
        public Uri Uri { get; set; }
        public HttpContent HttpContent { get; set; }
    }
}
