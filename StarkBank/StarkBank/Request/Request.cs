using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;
using StarkCore.Utils;

namespace StarkBank
{
	public partial class Request
	{

		private const string prefix = "Joker";

        /// <summary>
        /// Retrieve any StarkBank resource
        /// <br/>
        /// Receive a json of resources previously created in StarkBank's API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>path [string]: StarkBank resource's route. ex: "/invoice/"</item>
        /// </list>
        /// Parameters (optional):
        /// <list>                                                      
        ///     <item>query [Dictionary<string, object>, default None]: Query parameters. ex: new Dictionary(){ { "limit", 1 }, { "status", "paid" } }</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>Dictionary of StarkBank objects with updated attributes</item>
        /// </list>
        /// </summary>


        public static Response Get(string path, Dictionary<string, object> query = null, User user = null)
		{
			return Utils.Rest.GetRaw(
					path: path,
					query: query,
					user: user,
					prefix: prefix,
					raiseException: false
				);
		}

        /// <summary>
        /// Create any StarkBank resource
        /// <br/>
        /// Send a list of jsons and create any StarkBank resource objects
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>path [string]: StarkBank resource's route. ex: "/invoice/"</item>                                                          
        ///     <item>body [Dictionary<string, object>]: request parameters. ex: new List<string, Dictionary<string, object>>() { "invoices", new Dictionary<string, object>() { { "amount", 100 }, { "name", "Iron Bank S.A." }, { "taxId", "20.018.183/0001-80" } } }</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>query [Dictionary<string, object>, default None]: Query parameters. ex: new Dictionary(){ { "limit", 1 }, { "status", "paid" } }</item>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>list of resources jsons with updated attributes</item>
        /// </list>
        /// </summary>

        public static Response Post(string path, Dictionary<string, object> payload = null, Dictionary<string, object> query = null, User user = null)
        {
            return Utils.Rest.PostRaw(
                    path: path,
                    query: query,
                    user: user,
                    payload: payload,
                    prefix: prefix,
                    raiseException: false
                );
        }

        /// <summary>
        /// Update any StarkBank resource
        /// <br/>
        /// Send a json with parameters of a single StarkBank resource object and update it
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>path [string]: StarkBank resource's route. ex: "/invoice/"</item>
        ///     <item>body [Dictionary<string, object>]: request parameters. ex: new List<string, Dictionary<string, object>>() { "invoices", new Dictionary<string, object>() { { "amount", 100 }, { "name", "Iron Bank S.A." }, { "taxId", "20.018.183/0001-80" } } }</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>json of the resource with updated attributes</item>
        /// </list>
        /// </summary>
        ///

        public static Response Patch(string path, Dictionary<string, object> payload = null, User user = null)
        {
            return Utils.Rest.PatchRaw(
                    path: path,
                    user: user,
                    query: null,
                    payload: payload,
                    prefix: prefix,
                    raiseException: false
                );
        }

        /// <summary>
        /// Put any StarkBank resource
        /// <br/>
        /// Send a json with parameters of a single StarkBank resource object and create it, if the resource alredy exists, you will update it.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>path [string]: StarkBank resource's route. ex: "/invoice/"</item>
        ///     <item>body [Dictionary<string, object>]: request parameters. ex: new List<string, Dictionary<string, object>>() { "invoices", new Dictionary<string, object>() { { "amount", 100 }, { "name", "Iron Bank S.A." }, { "taxId", "20.018.183/0001-80" } } }</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>json of the resource with updated attributes</item>
        /// </list>
        /// </summary>
        ///

        public static Response Put(string path, Dictionary<string, object> payload = null, Dictionary<string, object> query = null, User user = null)
        {
            return Utils.Rest.PutRaw(
                    path: path,
                    query: query,
                    user: user,
                    payload: payload,
                    prefix: prefix,
                    raiseException: false
                );
        }

        /// <summary>
        /// Delete any StarkBank resource
        /// <br/>
        /// Send a json with parameters of a single StarkBank resource object and delete it you will update it.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>path [string]: StarkBank resource's route. ex: "/invoice/"</item>
        ///     <item>body [Dictionary<string, object>]: request parameters. ex: new List<string, Dictionary<string, object>>() { "invoices", new Dictionary<string, object>() { { "amount", 100 }, { "name", "Iron Bank S.A." }, { "taxId", "20.018.183/0001-80" } } }</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// Return:
        /// <list>
        ///     <item>json of the resource with updated attributes</item>
        /// </list>
        /// </summary>
        ///

        public static Response Delete(string path, Dictionary<string, object> payload = null, Dictionary<string, object> query = null, User user = null)
        {
            return Utils.Rest.DeleteRaw(
                    path: path,
                    query: query,
                    user: user,
                    payload: payload,
                    prefix: prefix,
                    raiseException: false
                );
        }

    }
}

