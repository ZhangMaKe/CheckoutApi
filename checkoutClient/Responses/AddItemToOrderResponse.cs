using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class AddItemToOrderResponse : EntityApiResponse<Order>
    {
        public AddItemToOrderResponse(Order entity, string message) : base(entity, message)
        {
        }
    }
}
