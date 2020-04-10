﻿using System.Collections.Generic;
using System;


namespace StarkBank.Utils
{
    static internal class Rest
    {
        internal static IEnumerable<IResource> GetList(string resourceName, Api.ResourceMaker resourceMaker, Dictionary<string, object> query, User user)
        {
            object rawLimit = query.GetValueOrDefault("limit");
            query["limit"] = rawLimit;
            int limit = 0;
            bool limited = false;
            if (rawLimit != null)
            {
                limited = true;
                limit = (int) rawLimit;
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

                foreach(dynamic entityJson in json[Api.LastNamePlural(resourceName)])
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
            } while (cursor != null && (!limited || limit > 0));
        }

        static internal IResource GetId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}"
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal string GetPdf(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            return Request.Fetch(
                user: user,
                method: Request.Get,
                path: $"{Api.Endpoint(resourceName)}/{id}/pdf"
            ).Content;
        }

        static internal IEnumerable<IResource> Post(string resourceName, Api.ResourceMaker resourceMaker, IEnumerable<IResource> entities, User user)
        {
            List<object> jsons = new List<object>();
            foreach (IResource entity in entities)
            {
                jsons.Add(Api.ApiJson(entity));
            }
            Dictionary<string, object> payload = new Dictionary<string, object>
            {
                {Api.LastNamePlural(resourceName), jsons}
            };

            dynamic fetchedJsons = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: payload
            ).Json()[Api.LastNamePlural(resourceName)];

            List<IResource> returnedEntities = new List<IResource>();
            foreach (dynamic json in fetchedJsons)
            {
                returnedEntities.Add(Api.FromApiJson(resourceMaker, json));
            }
            return returnedEntities;
        }

        static internal IResource PostSingle(string resourceName, Api.ResourceMaker resourceMaker, IResource entity, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Post,
                path: Api.Endpoint(resourceName),
                payload: Api.ApiJson(entity)
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal IResource DeleteId(string resourceName, Api.ResourceMaker resourceMaker, string id, User user)
        {
            dynamic json = Request.Fetch(
                user: user,
                method: Request.Delete,
                path: $"{Api.Endpoint(resourceName)}/{id}"
            ).Json()[Api.LastName(resourceName)];
            return Api.FromApiJson(resourceMaker, json);
        }

        static internal IResource PatchId(string resourceName, Api.ResourceMaker resourceMaker, string id, Dictionary<string, object> payload, User user)
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
