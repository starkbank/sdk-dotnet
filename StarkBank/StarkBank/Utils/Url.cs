using System.Collections;
using System.Collections.Generic;


namespace StarkBank.Utils
{
    internal static class Url
    {
        internal static string Encode(Dictionary<string, object> query)
        {
            query = Api.CastJsonToApiFormat(query);
            List<string> queryStringList = new List<string>();

            foreach (KeyValuePair<string, object> entry in query)
            {
                if (entry.Value == null)
                {
                    continue;
                }

                string value;
                if (IsList(entry.Value))
                {
                    List<string> list = entry.Value as List<string>;
                    value = string.Join(",", list.ToArray());
                } else {
                    value = entry.Value.ToString();
                }

                queryStringList.Add(entry.Key + "=" + System.Web.HttpUtility.UrlEncode(value));
            }

            if (queryStringList.Count > 0)
            {
                return "?" + string.Join("&", queryStringList);
            }

            return "";
        }

        private static bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }
    }
}
