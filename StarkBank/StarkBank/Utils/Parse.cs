using System;
using StarkCore;
using EllipticCurve;
using StarkCore.Utils;

namespace StarkBank.Utils
{
	public class Parse
    {

        static string host = StarkHost.bank;
        static string apiVersion = "v2";
        static string sdkVersion = "2.19.0";

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
                key
            );

        }
	}
}
