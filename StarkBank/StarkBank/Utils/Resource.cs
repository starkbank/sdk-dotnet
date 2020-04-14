﻿using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


namespace StarkBank.Utils
{
    public abstract class Resource
    {
        public string ID { get; }

        public Resource(string id)
        {
            ID = id;
        }

        public override string ToString()
        {
            Dictionary<string, object> dict = GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(this));

            string spacer = "  ";
            string newLine = Environment.NewLine;
            List<string> fields = new List<string>();
            foreach (KeyValuePair<string, object> entry in dict)
            {
                string stringValue;
                if (entry.Value is null)
                {
                    stringValue = "null";
                }
                else if (entry.Value is List<string> stringList)
                {
                    stringValue = "{ " + string.Join(", ", stringList) + " }";
                }
                else if (entry.Value is List<Dictionary<string, object>> dictList)
                {
                    List<string> stringDictList = new List<string>();
                    foreach (Dictionary<string, object> subDict in dictList)
                    {
                        List<string> stringDictSubList = new List<string>();
                        foreach (KeyValuePair<string, object> subEntry in subDict)
                        {
                            stringDictSubList.Add("{ " + subEntry.Key + ", " + subEntry.Value.ToString() + " }");
                        }
                        stringDictList.Add("{ " + string.Join(", ", stringDictSubList) + " }");
                    }
                    stringValue = "{ " + string.Join(", ", stringDictList) + " }";
                }
                else
                {
                    stringValue = entry.Value.ToString();
                }
                string[] splitValue = stringValue.Split(
                    new[] { newLine },
                    StringSplitOptions.None
                );
                fields.Add(entry.Key + ": " + string.Join(newLine + spacer, splitValue));
            }
            string core = string.Join("," + newLine + spacer, fields);

            return GetType().Name + "(" + newLine + spacer + core + newLine + ")";
        }
    }
}
