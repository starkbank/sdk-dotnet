using System;
using EllipticCurve;
using System.Reflection.Metadata;
using StarkCore;
using StarkCore.Utils;

namespace StarkBank.Utils
{
	public class Parse
    {

        static string host = StarkHost.bank;
        static string apiVersion = "v2";
        static string sdkVersion = "0.2.0";

        public static SubResource ParseAndVerify(string content, string signature, string resourceName, Api.ResourceMaker resourceMaker, User user, string key = null)
        {

            return StarkCore.Utils.Parse.ParseAndVerify(
                content,
                signature,
                resourceName,
                resourceMaker,
                user,
                host,
                apiVersion,
                sdkVersion,
                key = key
            );

        }
	}
}
