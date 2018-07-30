using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Responses
{
    public class UpdateItemQuantityResponse : BaseResponse<Order>
    {
        public UpdateItemQuantityResponse(Order order, string message) : base(message)
        {
            Entity = order;
        }
    }
}
