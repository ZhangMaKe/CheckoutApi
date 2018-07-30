using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Responses
{
    public class RemoveItemFromOrderResponse : BaseResponse<Order>
    {
        public RemoveItemFromOrderResponse(Order order, string message) : base(message)
        {
            Entity = order;
        }
    }
}
