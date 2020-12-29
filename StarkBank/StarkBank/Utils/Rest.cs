using System.Collections.Generic;
using System;


namespace StarkBank.Utils
{
    static internal class Rest
    {
        internal static IEnumerable<Resource> GetList(string resourceName, Api.ResourceMaker resourceMaker, Dictionary<string, object> query, User user)
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

        static internal Resource GetId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}"
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal byte[] GetPdf(string resourceName, Api.ResourceMaker resourceMaker, string id, Dictionary<string, object> options = null, User user = null)
        {
            return Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}/pdf",
                query: options
            ).ByteContent;
        }

        static internal byte[] GetQrcode(string resourceName, Api.ResourceMaker resourceMaker, string id, Dictionary<string, object> options = null, User user = null)
        {
            return Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}/qrcode",
                query: options
            ).ByteContent;
        }

        static internal IEnumerable<Resource> Post(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<Resource> entities, User user)
        {
            List<Dictionary<string, object>> jsons = new List<Dictionary<string, object>>();
            foreach (Resource entity in entities)
            {
                jsons.Add(Api.ApiJson(entity));
            }
            return PrivatePost(resourceName, resourceMaker, jsons, user);
        }

        static internal IEnumerable<Resource> Post(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<Dictionary<string, object>> entities, User user)
        {
            List<Dictionary<string, object>> jsons = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> entity in entities)
            {
                jsons.Add(Api.ApiJson(entity));
            }
            return PrivatePost(resourceName, resourceMaker, jsons, user);
        }

        static private IEnumerable<Resource> PrivatePost(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<Dictionary<string, object>> entities, User user)
        {
            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                {Api.LastNamePlural(resourceName), entities}
            };

            dynamic fetchedJsons = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: payload
            ).Json()[Api.LastNamePlural(resourceName)];

            List<Resource> returnedEntities = new List<Resource>();
            foreach (dynamic json in fetchedJsons)
            {
                returnedEntities.Add(Api.FromApiJson(resourceMaker, json));
            }
            return returnedEntities;
        }

        static internal Resource PostSingle(string resourceName, Api.ResourceMaker resourceMaker, Resource entity, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: Api.ApiJson(entity)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal Resource DeleteId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Delete,
                path: $"{Api.Endpoint(resourceName)}/{id}"
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal Resource PatchId(string resourceName, Api.ResourceMaker resourceMaker, string id, Dictionary<string, object> payload, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Patch,
                path: $"{Api.Endpoint(resourceName)}/{id}",
                payload: Api.CastJsonToApiFormat(payload)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }
    }
}
