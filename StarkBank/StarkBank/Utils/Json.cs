using System.IO;
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
            using (var reader = new JsonTextReader(new StringReader(content)) { DateParseHandling = DateParseHandling.None })
                return JObject.Load(reader);
        }
    }
}
