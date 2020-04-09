using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using EllipticCurve;


namespace StarkBank.Utils
{
    internal class Response
    {
        internal string Content { get; }
        internal int Status { get; }

        internal Response(string content, int status)
        {
            Content = content;
            Status = status;
        }

        internal JObject Json()
        {
            return Utils.Json.Decode(Content);
        }
    }

    internal static class Request
    {
        private static readonly HttpClient Client = new HttpClient();
        internal static readonly HttpMethod Get = HttpMethod.Get;
        internal static readonly HttpMethod Put = HttpMethod.Put;
        internal static readonly HttpMethod Post = HttpMethod.Post;
        internal static readonly HttpMethod Patch = HttpMethod.Patch;
        internal static readonly HttpMethod Delete = HttpMethod.Delete;

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

            string agent = $".NET-{Environment.Version}-SDK-2.0.0";
            string accessTime = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString();
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
                RequestUri = new Uri(url),
                Content = new StringContent(body)
            };

            httpRequestMessage.Content.Headers.TryAddWithoutValidation("Access-Id", user.AccessId());
            httpRequestMessage.Content.Headers.TryAddWithoutValidation("Access-Time", accessTime);
            httpRequestMessage.Content.Headers.TryAddWithoutValidation("Access-Signature", signature);
            httpRequestMessage.Content.Headers.TryAddWithoutValidation("User-Agent", agent);
            httpRequestMessage.Content.Headers.TryAddWithoutValidation("Content-Type", "application/json");

            var result = Client.SendAsync(httpRequestMessage).Result;

            Response response = new Response(
                result.Content.ReadAsStringAsync().Result,
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
