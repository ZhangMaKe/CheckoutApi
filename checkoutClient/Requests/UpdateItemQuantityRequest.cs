using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace checkoutClient.Requests
{
    public class UpdateItemQuantityRequest : BaseRequest
    {
        public UpdateItemQuantityRequest()
        {
            HttpMethod = new HttpMethod("PATCH");
        }
    }
}
