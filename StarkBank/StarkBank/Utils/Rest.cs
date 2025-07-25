using System;
using StarkCore;
using System.Text;
using System.Linq;
using StarkCore.Utils;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace StarkBank.Utils
{
    public class Rest
    {

        static string host = StarkHost.bank;
        static string apiVersion = "v2";
        static string sdkVersion = "2.16.1";

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

        public static StarkCore.Utils.Resource GetId(string id, User user = null, string resourceName = null, Api.ResourceMaker resourceMaker = null, Dictionary<string, object> query = null)
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

        public static SubResource PostSubResource(string id, SubResource entity = null, string resourceName = null, User user = null, string subResourceName = null, Api.ResourceMaker subResourceMaker = null)
        {
            return StarkCore.Utils.Rest.PostSubResource(
                resourceName: resourceName,
                subResourceMaker: subResourceMaker,
                subResourceName: subResourceName,
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                id: id,
                entity: entity,
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

        static public Response GetRaw(string path, Dictionary<string, object> query, User user, string prefix = null, bool raiseException = true)
        {
            return StarkCore.Utils.Rest.GetRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                query: query,
                user: user,
                prefix: prefix,
                raiseException: raiseException
            );
        }

        static public Response PostRaw(Dictionary<string, object> payload, string path, Dictionary<string, object> query, User user, string prefix = null, bool raiseException = true)
        {
            return StarkCore.Utils.Rest.PostRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                payload: payload,
                query: query,
                user: user,
                prefix: prefix,
                raiseException: raiseException
            );
        }

        static public Response PatchRaw(Dictionary<string, object> payload, string path, Dictionary<string, object> query, User user, string prefix = null, bool raiseException = true)
        {
            return StarkCore.Utils.Rest.PatchRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                payload: payload,
                query: query,
                user: user,
                prefix: prefix,
                raiseException: raiseException
            );
        }

        static public Response PutRaw(Dictionary<string, object> payload, string path, Dictionary<string, object> query, User user, string prefix = null, bool raiseException = true)
        {
            return StarkCore.Utils.Rest.PutRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                payload: payload,
                query: query,
                user: user,
                prefix: prefix,
                raiseException: raiseException
            );
        }

        static public Response DeleteRaw(Dictionary<string, object> payload, string path, Dictionary<string, object> query, User user, string prefix = null, bool raiseException = true)
        {
            return StarkCore.Utils.Rest.DeleteRaw(
                host: host,
                apiVersion: apiVersion,
                sdkVersion: sdkVersion,
                path: path,
                payload: payload,
                query: query,
                user: user,
                prefix: prefix,
                raiseException: raiseException
            );
        }
    }
}
