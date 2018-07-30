using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using checkoutClient.Models;
using checkoutClient.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace checkoutClient
{
    public static class HttpResponseMessageExtensions
    {
        public static Order ToOrder<T>(this HttpResponseMessage httpResponseMessage) where T : EntityApiResponse<Order>
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
