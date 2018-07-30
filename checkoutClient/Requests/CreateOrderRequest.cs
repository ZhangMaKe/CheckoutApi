using System.Net.Http;

namespace checkoutClient.Requests
{
    public class CreateOrderRequest : BaseRequest
    {
        public CreateOrderRequest()
        {
            HttpMethod = HttpMethod.Post;
        }
    }
}
