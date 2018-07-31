using System;
using System.Net.Http;
using System.Threading.Tasks;
using checkoutClient.Interfaces;
using checkoutClient.Models;
using checkoutClient.Requests;

namespace checkoutClient.Services
{
    public class OrdersService : IOrdersService
    {
        private const string BaseUri = "http://checkoutapi.azurewebsites.net/api/orders/";
       // private const string BaseUri = "http://localhost:50211/api/orders/";
        
        public IHttpRequestService HttpRequestService { get; set; }

        public OrdersService(IHttpRequestService httpRequestService)
        {
            HttpRequestService = httpRequestService;
        }
       

        public async Task<HttpResponseMessage> CreateOrder(Order order)
        {
            var request = new CreateOrderRequest
            {
                Uri = new Uri(BaseUri),
                HttpMethod = HttpMethod.Post,
                HttpContent = HttpHelperService.CreateHttpContent(order)
            };

            return await HttpRequestService.PostAsync(request);
        }

        public async Task<HttpResponseMessage> AddItemToOrder(Guid orderId, Item item)
        {
            var request = new AddItemToOrderRequest
            {
                Uri = new Uri(BaseUri + orderId + "/items"),
                HttpContent = HttpHelperService.CreateHttpContent(item)
            };

            return await HttpRequestService.PostAsync(request);
        }

        public async Task<HttpResponseMessage> RemoveItemFromOrder(Guid orderId, Guid itemId)
        {
            var request = new RemoveItemFromOrderRequest
            {
                Uri = new Uri(BaseUri + orderId + "/items/" + itemId)
            };

            return await HttpRequestService.DeleteAsync(request);
        }

        public async Task<HttpResponseMessage> UpdateItemQuantity(int quantity, Guid orderId, Guid itemId)
        {
            var request = new UpdateItemQuantityRequest
            {
                Uri = new Uri(BaseUri + orderId + "/items/" + itemId + "/quantity"),
                HttpContent = HttpHelperService.CreateHttpContent(quantity)
            };

            return await HttpRequestService.PatchAsync(request);
        }

        public async Task<HttpResponseMessage> ClearItems(Guid orderId)
        {
            var request = new ClearItemsRequest
            {
                Uri = new Uri(BaseUri + orderId)
            };

            return await HttpRequestService.PatchAsync(request);
        }

    }
}
