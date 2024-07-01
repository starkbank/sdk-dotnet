using Xunit;
using StarkBank;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace StarkBankTests
{
	public class RequestTest
	{

        public readonly User user = TestUser.SetDefaultProject();

        [Fact]
        public void Get()
		{

            string path = "/invoice";
            Dictionary<string, object> query = new Dictionary<string, object>() { { "limit", 10 } };

            JObject request = Request.Get(
                    path: path,
                    query: query,
                    user: user
                ).Json();

            Assert.NotNull(request["invoices"][0]["id"]);

        }

        [Fact]
        public void Post()
        {

            string path = "/invoice";
            Dictionary<string, object> data = new Dictionary<string, object>() {
                {
                    "invoices", new List<Dictionary<string, object>>() { new Dictionary<string, object>()
                        {
                            { "amount", 100 },
                            { "name", "Iron Bank S.A." },
                            { "taxId", "20.018.183/0001-80" }
                        },

                    }
                }
            };

            JObject request = Request.Post(
                    path: path,
                    payload: data,
                    user: user
                ).Json();

            Assert.NotNull(request["invoices"][0]["id"]);

        }

        [Fact]
        public void Patch()
        {

            string path = "/invoice";

            JObject initialState = Request.Get(
                    path: path,
                    query: new Dictionary<string, object>() { { "limit", 1 } },
                    user: user
                ).Json();

            path += "/" + initialState["invoices"][0]["id"].ToString();

            Dictionary<string, object> data = new Dictionary<string, object>() { { "amount", 0 } };

            Request.Patch(
                    path: path,
                    payload: data,
                    user: user
                );

            JObject finalState = Request.Get(
                    path: path,
                    query: null,
                    user: user
                ).Json();

            Assert.NotNull(finalState["invoice"]["id"]);

        }

        [Fact]
        public void Delete()
        {

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {
                    "transfers", new List<Dictionary<string, object>>() {

                        new Dictionary<string, object>()
                        {
                            { "amount", 10000 },
                            { "name", "Steve Rogers" },
                            { "taxId", "851.127.850-80" },
                            { "bankCode",  "001" },
                            { "branchCode", "1234" },
                            { "accountNumber", "123456-0" },
                            { "accountType", "checking" },
                            { "scheduled", DateTime.Now.AddDays(1) },
                            { "externalId", Guid.NewGuid().ToString() }
                        }
                    }
                }
            };

            JObject create = Request.Post(
                    path: "/transfer/",
                    payload: data,
                    user: user
                ).Json();

            JObject deleted = Request.Delete(
                path: "/transfer/" + create["transfers"][0]["id"].ToString(),
                user: user
               ).Json();

            Assert.NotNull(deleted["transfer"]["id"]);

        }

        [Fact]
        public void Put()
        {

            string path = "/split-profile";
            Dictionary<string, object> data = new Dictionary<string, object>() {
                {
                    "profiles", new List<Dictionary<string, object>>() {
                        new Dictionary<string, object>()
                        {
                            { "interval", "day" },
                            { "delay", 0 }
                        }
                    }
                }
            };

            JObject request = Request.Put(
                    path: path,
                    payload: data,
                    user: user
                ).Json();

            Assert.Equal(request["profiles"][0]["delay"], 0);
            Assert.Equal(request["profiles"][0]["interval"], "day");

        }


    }
}

