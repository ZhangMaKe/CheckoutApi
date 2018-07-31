using System;
using System.Net.Http;
using System.Threading.Tasks;
using checkoutClient.Models;

namespace checkoutClient.Interfaces
{
    public interface IOrdersService
    {
        IHttpRequestService HttpRequestService { get; set; }
        Task<HttpResponseMessage> CreateOrder(Order order);
        Task<HttpResponseMessage> AddItemToOrder(Guid orderId, Item item);
        Task<HttpResponseMessage> RemoveItemFromOrder(Guid orderId, Guid itemId);
        Task<HttpResponseMessage> UpdateItemQuantity(int quantity, Guid orderId, Guid itemId);
        Task<HttpResponseMessage> ClearItems(Guid orderId);
    }
}
