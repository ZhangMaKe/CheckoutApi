using System.Net.Http;
using checkoutClient.Models;
using checkoutClient.Responses;
using Newtonsoft.Json;

namespace checkoutClient
{
    public static class HttpResponseMessageExtensions
    {
        public static TEntity ToEntity<T, TEntity>(this HttpResponseMessage httpResponseMessage)
            where T : EntityApiResponse<TEntity> where TEntity : BaseEntity
        {
            var content = httpResponseMessage.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<T>(content);

            return response.Entity;
        }

        public static T ToResponseObject<T>(this HttpResponseMessage httpResponseMessage) where T : BaseApiResponse
        {
            var content = httpResponseMessage.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<T>(content);

            return response;
        }
    }
}
