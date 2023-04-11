using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace StarkBank.Utils
{
    static internal class Rest
    {
        static internal (List<SubResource> entities, string cursor) GetPage(string resourceName, Api.ResourceMaker resourceMaker, Dictionary<string, object> query, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Get,
                path: Api.Endpoint(resourceName),
                query: query
            ).Json();
            
            List<SubResource> entities = new List<SubResource>();
            foreach (dynamic entityJson in json[Api.LastNamePlural(resourceName)])
            {
                entities.Add(Api.FromApiJson(resourceMaker, entityJson));
            }
            string cursor = json["cursor"];
            return (entities, cursor);
        }

        internal static IEnumerable<SubResource> GetList(string resourceName, Api.ResourceMaker resourceMaker, Dictionary<string, object> query, User user)
        {
            query.TryGetValue("limit", out object rawLimit);
            query["limit"] = rawLimit;
            int limit = 0;
            bool limited = false;
            if (rawLimit != null)
            {
                limited = true;
                limit = (int)rawLimit;
                query["limit"] = Math.Min(limit, 100);
            }

            string cursor;

            do
            {
                dynamic json = Request.Fetch(
                    user: user,
                    method: Request.Get,
                    path: Api.Endpoint(resourceName),
                    query: query
                ).Json();

                foreach (dynamic entityJson in json[Api.LastNamePlural(resourceName)])
                {
                    yield return Api.FromApiJson(resourceMaker, entityJson);
                }

                if (limited)
                {
                    limit -= 100;
                    query["limit"] = Math.Min(limit, 100);
                }

                cursor = json["cursor"];
                query["cursor"] = cursor;
            } while (cursor != null && cursor.Length > 0 && (!limited || limit > 0));
        }

        internal static Resource GetId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user, Dictionary<string, object> query = null)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}",
                query: query
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal byte[] GetContent(string resourceName, Api.ResourceMaker resourceMaker, string id,
                                          Dictionary<string, object> options = null, string subResourceName = null, User user = null)
        {
            return Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}/{subResourceName}",
                query: options
            ).ByteContent;
        }

        static internal IEnumerable<SubResource> Post(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<SubResource> entities, User user, Dictionary<string, object> query = null)
        {
            List<Dictionary<string, object>> jsons = new List<Dictionary<string, object>>();
            foreach (SubResource entity in entities)
            {
                jsons.Add(Api.ApiJson(entity));
            }
            return PrivatePost(resourceName, resourceMaker, jsons, user, query);
        }

        static internal IEnumerable<SubResource> Post(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<Dictionary<string, object>> entities, User user, Dictionary<string, object> query = null)
        {
            List<Dictionary<string, object>> jsons = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> entity in entities)
            {
                jsons.Add(Api.ApiJson(entity));
            }
            return PrivatePost(resourceName, resourceMaker, jsons, user, query);
        }

        static private IEnumerable<SubResource> PrivatePost(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<Dictionary<string, object>> entities, User user, Dictionary<string, object> query = null)
        {
            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                {Api.LastNamePlural(resourceName), entities}
            };

            dynamic fetchedJsons = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                query: query,
                payload: payload
            ).Json()[Api.LastNamePlural(resourceName)];

            List<SubResource> returnedEntities = new List<SubResource>();
            foreach (dynamic json in fetchedJsons)
            {
                returnedEntities.Add(Api.FromApiJson(resourceMaker, json));
            }
            return returnedEntities;
        }

        static internal SubResource PostSingle(string resourceName, Api.ResourceMaker resourceMaker, SubResource entity, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: Api.ApiJson(entity)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }
        
        static internal SubResource PostSingle(string resourceName, Api.ResourceMaker resourceMaker, Dictionary<string, object> entity, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: Api.ApiJson(entity)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal JObject PostRaw(string path, User user, Dictionary<string, object> payload = null, Dictionary<string, object> query = null)
        {
            return Request.Fetch(
                user: user,
                method: Request.Post,
                path: path,
                query: query,
                payload: Api.ApiJson(payload)
            ).Json();
        }

        static internal SubResource DeleteId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Delete,
                path: $"{Api.Endpoint(resourceName)}/{id}"
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal SubResource PatchId(string resourceName, Api.ResourceMaker resourceMaker, string id, Dictionary<string, object> payload, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Patch,
                path: $"{Api.Endpoint(resourceName)}/{id}",
                payload: Api.CastJsonToApiFormat(payload)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal SubResource GetSubResource(string resourceName, Api.ResourceMaker subResourceMaker, string subResourceName,
                                                   string id, User user, Dictionary<string, object> payload = null)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}/{Api.Endpoint(subResourceName)}",
                payload: payload
            ).Json()[Api.LastName(subResourceName)];
            return Api.FromApiJson(subResourceMaker, json);
        }
    }
}
