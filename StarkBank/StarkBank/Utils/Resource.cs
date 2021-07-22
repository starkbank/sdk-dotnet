using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace StarkBank.Utils
{
    public abstract class Resource : SubResource
    {
        public string ID { get; }

        public Resource(string id)
        {
            ID = id;
        }

        internal Dictionary<string, object> ToJson()
        {
            return GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this));
        }
    }
}
