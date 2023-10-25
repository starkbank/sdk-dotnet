using System;
using System.Collections.Generic;
using System.Text;
using StarkCore;
using System.Linq;
using StarkCore.Utils;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace StarkBank.Utils
{
    public class Rest
    {

        static string host = StarkHost.bank;
        static string apiVersion = "v2";
        static string sdkVersion = "2.14.0";

        public static IEnumerable<SubResource> GetList(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> query = null)
        {
            return StarkCore.Utils.Rest.GetList(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: query,
                user: user
            );
        }

        public static IEnumerable<SubResource> Post(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, IEnumerable<SubResource> entities = null, Dictionary<string, object> query = null)
        {
            return StarkCore.Utils.Rest.Post(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: query,
                entities: entities,
                user: user
            );
        }

        public static IEnumerable<SubResource> Post(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, IEnumerable<Dictionary<string, object>> entities = null, Dictionary<string, object> query = null)
        {
            return StarkCore.Utils.Rest.Post(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: query,
                entities: entities,
                user: user
            );
        }

        public static Resource GetId(string id, User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> query = null)
        {
            return StarkCore.Utils.Rest.GetId(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                id: id,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: query,
                user: user
            );
        }

        public static (List<SubResource> entities, string cursor) GetPage(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> query = null)
        {
             (List<SubResource> page, string pageCursor) = StarkCore.Utils.Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                query: query,
                user: user
            );

            return (page, pageCursor);
        }

        public static SubResource PatchId(string id, User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> payload = null)
        {
            return StarkCore.Utils.Rest.PatchId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                id: id,
                payload: payload,
                user: user
            );
        }

        public static byte[] GetContent(string id, User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> options = null, string subResourceName = null)
        {
            return StarkCore.Utils.Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                subResourceName: subResourceName,
                options: options,
                id: id,
                user: user
            );
        }

        public static SubResource GetSubResource(string id, Dictionary<string, object> payload = null, string resourceName = null, User user = null, string subResourceName = null, Api.ResourceMaker subResourceMaker = null)
        {
            return StarkCore.Utils.Rest.GetSubResource(
                resourceName: resourceName,
                subResourceMaker: subResourceMaker,
                subResourceName: subResourceName,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                id: id,
                payload: payload,
                user: user
            );
        }

        public static SubResource DeleteId(string id, User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null)
        {
            return StarkCore.Utils.Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                id: id,
                user: user
            );
        }

        static public JObject PostRaw(Dictionary<string, object> payload, string path, Dictionary<string, object> query, User user)
        {
            return StarkCore.Utils.Rest.PostRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                payload: payload,
                query: query,
                user: user
            );
        }

        public static SubResource PostSingle(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, SubResource entity = null)
        {
            return StarkCore.Utils.Rest.PostSingle(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: entity,
                user: user
            );
        }

        public static SubResource PostSingle(User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> entity = null)
        {
            return StarkCore.Utils.Rest.PostSingle(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entity: entity,
                user: user
            );
        }

    }
}
