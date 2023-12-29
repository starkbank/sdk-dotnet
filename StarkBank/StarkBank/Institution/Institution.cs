using System;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank.Institution
{
    public class Institution : StarkCore.Utils.SubResource
    {
        /// <summary>
        /// Institution object
        /// <br/>
        /// This resource is used to get information on the institutions that are recognized by the Brazilian Central Bank.
        /// Besides the display name and full name, they also include the STR code (used for TEDs) and the SPI Code
        /// (used for Pix) for the institutions. Either of these codes may be empty if the institution is not registered on
        /// that Central Bank service.
        /// <br/>
        /// Properties:
        /// <list>
        ///     <item>DisplayName [string]: short version of the institution name that should be displayed to end users. ex: "Stark Bank"
        ///     <item>Name [string]: full version of the institution name. ex: "Stark Bank S.A."</item>
        ///     <item>SpiCode [string]: SPI code used to identify the institution on Pix transactions. ex: "20018183"</item>
        ///     <item>StrCode [string]: STR code used to identify the institution on TED transactions. ex: "123"</item>
        /// </list>
        /// </summary>
        public string DisplayName { get; }
        public string Name { get; }
        public string SpiCode { get; }
        public string StrCode { get; }

        /// <summary>
        /// Institution object
        /// <br/>
        /// This resource is used to get information on the institutions that are recognized by the Brazilian Central Bank.
        /// Besides the display name and full name, they also include the STR code (used for TEDs) and the SPI Code
        /// (used for Pix) for the institutions. Either of these codes may be empty if the institution is not registered on
        /// that Central Bank service.
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>displayName [string]: short version of the institution name that should be displayed to end users. ex: "Stark Bank"
        ///     <item>name [string]: full version of the institution name. ex: "Stark Bank S.A."</item>
        ///     <item>spiCode [string]: SPI code used to identify the institution on Pix transactions. ex: "20018183"</item>
        ///     <item>strCode [string]: STR code used to identify the institution on TED transactions. ex: "123"</item>
        /// </list>
        /// </summary>
        public Institution(string displayName, string name, string spiCode, string strCode)
        {
            DisplayName = displayName;
            Name = name;
            SpiCode = spiCode;
            StrCode = strCode;
        }

        /// <summary>
        /// Institution object
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
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = SubResource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
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
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                institutions.Add(subResource as Institution);
            }
            return institutions;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) SubResource()
        {
            return (resourceName: "Institution", resourceMaker: ResourceMaker);
        }

        public static StarkCore.Utils.SubResource ResourceMaker(dynamic json)
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
