using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using checkout.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace checkout.Responses
{
    public class CreateOrderResponse : BaseResponse<Order>
    {
        public CreateOrderResponse(Order order, string message)
            : base(message)
        {
            Entity = order;
        }

    }
}
