using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace StarkBank.Utils
{
    internal static class Json
    {
        internal static string Encode(object payload)
        {
            return JsonConvert.SerializeObject(payload);
        }

        internal static JObject Decode(string content)
        {
            return JObject.Parse(content) as JObject;
        }
    }
}
