using checkoutClient.Interfaces;

namespace checkoutClient
{
    public interface ICheckoutClient
    {
        IOrdersService OrdersService { get; set; }
    }
}
