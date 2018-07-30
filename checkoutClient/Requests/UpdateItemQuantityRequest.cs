using System.Net.Http;

namespace checkoutClient.Requests
{
    public class UpdateItemQuantityRequest : BaseRequest
    {
        public UpdateItemQuantityRequest()
        {
            HttpMethod = new HttpMethod("PATCH");
        }
    }
}
