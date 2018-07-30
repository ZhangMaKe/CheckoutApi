using System.Net.Http;

namespace checkoutClient.Requests
{
    public class RemoveItemFromOrderRequest : BaseRequest
    {
        public RemoveItemFromOrderRequest()
        {
            HttpMethod = HttpMethod.Delete;
        }
    }
}
