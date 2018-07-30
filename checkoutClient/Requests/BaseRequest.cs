using System;
using System.Net.Http;

namespace checkoutClient.Requests
{
    public abstract class BaseRequest
    {
        public HttpMethod HttpMethod { get; set; }
        public Uri Uri { get; set; }
        public HttpContent HttpContent { get; set; }
    }
}
