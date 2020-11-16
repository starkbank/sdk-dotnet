using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;


namespace StarkBank.Utils
{
    internal static class Api
    {
        internal delegate Resource ResourceMaker(dynamic json);

        internal static Dictionary<string, object> ApiJson(Resource entity)
        {
            return CastJsonToApiFormat(entity.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(entity)));
        }

        internal static Dictionary<string, object> ApiJson(Dictionary<string, object> entity)
        {
            return CastJsonToApiFormat(entity);
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

                string key = Case.PascalToCamel(entry.Key);
                if (key.EndsWith("ID"))
                {
                    key = key.Substring(0, key.Length - 2) + "Id";
                }

                dynamic value = entry.Value;
                if (value is DateTime)
                {
                    DateTime data = value;
                    value = DateToString(data);
                }
                if (value is IList) {
                    bool nested = false;
                    List<object> casted = new List<object>();
                    foreach (object nestedEntry in value) {
                        if(nestedEntry is Dictionary<string, object>) {
                            Dictionary<string, object> castedNestedEntry = nestedEntry as Dictionary<string, object>;
                            casted.Add(CastJsonToApiFormat(castedNestedEntry));
                            nested = true;
                        }
                    }
                    if (nested) {
                        value = casted;
                    }
                }
                if (value is Resource) {
                    value = ApiJson(value);
                }

                apiJson.Add(
                    key,
                    value
                );
            }
            return apiJson;
        }

        internal static object DateToString(DateTime dateTime)
        {
            if (dateTime == dateTime.Date) {
                return dateTime.ToString("yyyy-MM-dd");
            }
            return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz");
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
            string lastName = LastName(resourceName);
            if (lastName.EndsWith("s")) {
                return lastName;
            }
            if (lastName.EndsWith("y") && !lastName.EndsWith("ey"))
            {
                return $"{lastName.Remove(lastName.Length - 1)}ies";
            }
            return $"{lastName}s";
        }

        internal static string LastName(string resourceName)
        {
            string[] names = Case.CamelOrPascalToKebab(resourceName).Split(new string[] { "-" }, StringSplitOptions.None);
            return names.Last();
        }
    }
}
