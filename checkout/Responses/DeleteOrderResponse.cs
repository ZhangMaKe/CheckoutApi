using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Responses
{
    public class DeleteOrderResponse : BaseResponse<string>
    {
        public DeleteOrderResponse(string message) : base(message)
        {
        }
    }
}
