using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class UpdateItemQuantityResponse : EntityApiResponse<Order>
    {
        public UpdateItemQuantityResponse(Order order, string message) : base(order, message)
        {
            Entity = order;
        }
    }
}
