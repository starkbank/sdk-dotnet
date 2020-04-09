using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;


namespace StarkBank.Utils
{
    internal static class Api
    {
        internal delegate Resource ResourceMaker(dynamic json);

        internal static Dictionary<string, object> ApiJson(Resource entity)
        {
            return CastJsonToApiFormat(entity.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(entity, null)));
        }

        internal static Dictionary<string, object> CastJsonToApiFormat(Dictionary<string, object> json)
        {
            Dictionary<string, object> apiJson = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> entry in json)
            {
                if (entry.Value == null)
                {
                    continue;
                }
                apiJson.Add(
                    Case.PascalToCamel(entry.Key),
                    DateToString(entry.Value)
                );
            }
            return apiJson;
        }

        internal static object DateToString(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        internal static object DateToString(object other)
        {
            return other;
        }

        internal static Resource FromApiJson(ResourceMaker resourceMaker, dynamic json)
        {
            return resourceMaker(json);
        }

        internal static string Endpoint(string resourceName)
        {
            string kebab = Case.CamelOrPascalToKebab(resourceName);
            return kebab.Replace("-log", "/log");
        }

        internal static string LastNamePlural(string resourceName)
        {
            return $"{LastName(resourceName)}s";
        }

        internal static string LastName(string resourceName)
        {
            string[] names = Case.CamelOrPascalToKebab(resourceName).Split("-");
            return names[names.Length - 1];
        }
    }
}
