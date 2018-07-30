using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Responses
{
    public class AddItemToOrderResponse : BaseResponse<Order>
    {
        public AddItemToOrderResponse(Order order, string message) : base(message)
        {
            Entity = order;
        }
    }
}
