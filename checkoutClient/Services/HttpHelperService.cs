using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace checkoutClient.Services
{
    public static class HttpHelperService
    {
        public static HttpContent CreateHttpContent(object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            var bufferBytes = Encoding.UTF8.GetBytes(serializedData);

            var content = new ByteArrayContent(bufferBytes);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return content;
        }
    }
}
