using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class CreateOrderResponse : EntityApiResponse<Order>
    {
        //public CreateOrderResponse(Order order, string message)
        //    : base(message)
        //{
        //    Entity = order;
        //}
        public CreateOrderResponse(Order entity, string message) : base(entity, message)
        {
        }
    }
}
