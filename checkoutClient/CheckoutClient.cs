using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Interfaces;

namespace checkoutClient
{
    public class CheckoutClient : ICheckoutClient
    {
        public IOrdersService OrdersService { get; set; }

        public CheckoutClient(IOrdersService ordersService)
        {
            OrdersService = ordersService;
        }
    }
}
