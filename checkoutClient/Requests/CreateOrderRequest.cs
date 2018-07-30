using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace checkoutClient.Requests
{
    public class CreateOrderRequest : BaseRequest
    {
        public CreateOrderRequest()
        {
            HttpMethod = HttpMethod.Post;
        }
    }
}
