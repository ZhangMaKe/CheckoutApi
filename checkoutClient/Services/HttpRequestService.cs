using System.Net.Http;
using System.Threading.Tasks;
using checkoutClient.Interfaces;
using checkoutClient.Requests;

namespace checkoutClient.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        public async Task<HttpResponseMessage> PostAsync(BaseRequest request)
        {
            var httpRequest = CreateRequest(request, HttpMethod.Post);

            return await SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> GetAsync(BaseRequest request)
        {
            var httpRequest = CreateRequest(request, HttpMethod.Get);

            return await SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> PatchAsync(BaseRequest request)
        {
            var httpRequest = CreateRequest(request, new HttpMethod("PATCH"));

            return await SendAsync(httpRequest);
        }

        public async Task<HttpResponseMessage> DeleteAsync(BaseRequest request)
        {
            var httpRequest = CreateRequest(request, HttpMethod.Delete);

            return await SendAsync(httpRequest);
        }

        private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.SendAsync(request);
            }
        }

        private HttpRequestMessage CreateRequest(BaseRequest request, HttpMethod httpMethod)
        {
            return new HttpRequestMessage
            {
                Content = request.HttpContent,
                Method = httpMethod,
                RequestUri = request.Uri
            };
        }
    }
}
