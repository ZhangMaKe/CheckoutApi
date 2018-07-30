using System;
using System.Collections.Generic;
using System.Text;
using checkoutClient.Interfaces;

namespace checkoutClient
{
    public interface ICheckoutClient
    {
        IOrdersService OrdersService { get; set; }
    }
}
