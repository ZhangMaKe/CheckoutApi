using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class RemoveItemFromOrderResponse : EntityApiResponse<Order>
    {
        public RemoveItemFromOrderResponse(Order entity, string message) : base(entity, message)
        {
        }
    }
}
