using System.Net.Http;

namespace checkoutClient.Requests
{
    public class DeleteOrderRequest : BaseRequest
    {
        public DeleteOrderRequest()
        {
            HttpMethod = HttpMethod.Delete;
        }
    }
}
