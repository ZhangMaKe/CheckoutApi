using System.Net.Http;

namespace checkoutClient.Requests
{
    public class ClearItemsRequest : BaseRequest
    {
        public ClearItemsRequest()
        {
            HttpMethod = new HttpMethod("PATCH");
        }
    }
}
