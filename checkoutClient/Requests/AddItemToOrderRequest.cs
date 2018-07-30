using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace checkoutClient.Requests
{
    public class AddItemToOrderRequest : BaseRequest
    {
        public AddItemToOrderRequest()
        {
            HttpMethod = HttpMethod.Post;
        }
    }
}
