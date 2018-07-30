using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using checkoutClient.Responses;

namespace checkoutClient.Requests
{
    public class DeleteOrderRequest : BaseRequest
    {
        public DeleteOrderRequest()
        {
            HttpMethod = HttpMethod.Delete;
        }
    }
}
