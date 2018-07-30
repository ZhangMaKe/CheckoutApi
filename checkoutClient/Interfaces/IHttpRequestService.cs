using System.Net.Http;
using System.Threading.Tasks;
using checkoutClient.Requests;

namespace checkoutClient.Interfaces
{
    public interface IHttpRequestService
    {
        Task<HttpResponseMessage> PostAsync(BaseRequest request);
        Task<HttpResponseMessage> GetAsync(BaseRequest request);
        Task<HttpResponseMessage> PatchAsync(BaseRequest request);
        Task<HttpResponseMessage> DeleteAsync(BaseRequest request);
    }
}
