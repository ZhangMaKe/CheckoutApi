using System.Net.Http;

namespace checkoutClient.Requests
{
    public class AddItemToOrderRequest : BaseRequest
    {
        public AddItemToOrderRequest()
        {
            HttpMethod = HttpMethod.Post;
        }
    }
}
