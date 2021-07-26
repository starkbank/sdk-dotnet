﻿using System;
using System.Collections.Generic;
using StarkBank.Utils;

namespace StarkBank.Institution
{
    public class Institution : SubResource
    {

        public string DisplayName { get; }
        public string Name { get; }
        public string SpiCode { get; }
        public string StrCode { get; }

        public Institution(string displayName, string name, string spiCode, string strCode)
        {
            DisplayName = displayName;
            Name = name;
            SpiCode = spiCode;
            StrCode = strCode;
        }

        /// <summary>
        /// Retrieve DictKeys
        /// <br/>
        /// Receive a list of Institution objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>search [string, default null]: part of the institution name to be searched. ex: "stark"</item>
        ///     <item>spiCodes [list of strings, default null]: list of SPI (Pix) codes to be searched. ex: ["20018183"]</item>
        ///     <item>strCodes [list of strings, default null]: list of STR (TED) codes to be searched. ex: ["260"]</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of Institution objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<Institution> Query(int? limit = null, string search = null,
            List<string> spiCodes = null, List<string> strCodes = null, User user = null)
        {
            (string resourceName, Utils.Api.ResourceMaker resourceMaker) = SubResource();
            (List<SubResource> page, string pageCursor) = Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "search", search },
                    { "spiCodes", spiCodes },
                    { "strCodes", strCodes }
                },
                user: user
            );
            List<Institution> institutions = new List<Institution>();
            foreach (SubResource subResource in page)
            {
                institutions.Add(subResource as Institution);
            }
            return institutions;
        }

        internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) SubResource()
        {
            return (resourceName: "Institution", resourceMaker: ResourceMaker);
        }

        public static Utils.SubResource ResourceMaker(dynamic json)
        {
            string displayName = json.displayName;
            string name = json.name;
            string spiCode = json.spiCode;
            string strCode = json.strCode;
            return new Institution(
                displayName: displayName, name: name, spiCode: spiCode, strCode: strCode
            );
        }
    }
}
