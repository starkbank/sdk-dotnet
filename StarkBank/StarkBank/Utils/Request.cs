using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;
using EllipticCurve;


namespace StarkBank.Utils
{
    internal class Response
    {
        internal byte[] ByteContent { get; }
        internal int Status { get; }

        internal Response(byte[] byteContent, int status)
        {
            ByteContent = byteContent;
            Status = status;
        }

        internal string Content
        {
            get
            {
                return System.Text.Encoding.UTF8.GetString(ByteContent);
            }
        }

        internal JObject Json()
        {
            return Utils.Json.Decode(Content);
        }
    }

    internal static class Request
    {
        private static HttpClient makeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", $".NET-{Environment.Version}-SDK-0.9.0");
            return client;
        }

        private static readonly HttpClient Client = makeClient();
        internal static readonly HttpMethod Get = new HttpMethod("GET");
        internal static readonly HttpMethod Put = new HttpMethod("PUT");
        internal static readonly HttpMethod Post = new HttpMethod("POST");
        internal static readonly HttpMethod Patch = new HttpMethod("PATCH");
        internal static readonly HttpMethod Delete = new HttpMethod("DELETE");

        internal static Response Fetch(User user, HttpMethod method, string path, Dictionary<string, object> payload = null, Dictionary<string, object> query = null)
        {
            user = Checks.CheckUser(user);

            string url = "";
            if (user.Environment == "production")
            {
                url = "https://api.starkbank.com/";
            }
            if (user.Environment == "sandbox")
            {
                url = "https://sandbox.api.starkbank.com/";
            }
            url += "v2/" + path;

            if (query != null)
            {
                url += Url.Encode(query);
            }

            string accessTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString(new CultureInfo("en-US"));
            string body = "";
            if (payload != null)
            {
                body = Json.Encode(payload);
            }
            string message = user.AccessId() + ":" + accessTime + ":" + body;
            string signature = Ecdsa.sign(message, user.PrivateKey()).toBase64();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };
            if (body.Length > 0) {
                httpRequestMessage.Content = new StringContent(body);
            }

            httpRequestMessage.Headers.TryAddWithoutValidation("Access-Id", user.AccessId());
            httpRequestMessage.Headers.TryAddWithoutValidation("Access-Time", accessTime);
            httpRequestMessage.Headers.TryAddWithoutValidation("Access-Signature", signature);
            httpRequestMessage.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            httpRequestMessage.Headers.TryAddWithoutValidation("Accept-Language", Settings.Language);

            var result = Client.SendAsync(httpRequestMessage).Result;

            Response response = new Response(
                result.Content.ReadAsByteArrayAsync().Result,
                (int) result.StatusCode
            );

            if (response.Status == 500)
            {
                throw new Error.InternalServerError();
            }
            if (response.Status == 400)
            {
                throw new Error.InputErrors(response.Content);
            }
            if (response.Status != 200)
            {
                throw new Error.UnknownError(response.Content);
            }

            return response;
        }
    }
}
