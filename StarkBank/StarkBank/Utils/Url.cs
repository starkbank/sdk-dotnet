using System.Collections;
using System.Collections.Generic;


namespace StarkBank.Utils
{
    internal static class Url
    {
        internal static string Encode(Dictionary<string, object> query)
        {
            List<string> queryStringList = new List<string>();

            foreach (KeyValuePair<string, object> entry in query)
            {
                if (entry.Value == null)
                {
                    continue;
                }

                string key = Case.PascalToCamel(entry.Key);

                string value = "";
                if (IsList(entry.Value))
                {
                    value = string.Join(",", entry.Value);
                } else
                {
                    value = entry.Value.ToString();
                }

                queryStringList.Add(key + "=" + value);
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
