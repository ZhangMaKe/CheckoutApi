using System;
using System.Net.Http;
using System.Threading.Tasks;
using checkoutClient.Models;
using checkoutClient.Responses;

namespace checkoutClient.Interfaces
{
    public interface IOrdersService
    {
        Task<HttpResponseMessage> CreateOrder();
        Task<HttpResponseMessage> AddItemToOrder(Guid orderId, Item item);
        Task<HttpResponseMessage> RemoveItemFromOrder(Guid orderId, Guid itemId);
        Task<HttpResponseMessage> UpdateItemQuantity(int quantity, Guid orderId, Guid itemId);
        Task<HttpResponseMessage> DeleteOrder(Guid orderId);
    }
}
