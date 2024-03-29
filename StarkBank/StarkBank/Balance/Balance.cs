﻿using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;


namespace StarkBank
{
    /// <summary>
    /// Balance object
    /// <br/>
    /// The Balance object displays the current balance of the workspace,
    /// which is the result of the sum of all transactions within this
    /// workspace.The balance is never generated by the user, but it
    /// can be retrieved to see the available information.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: unique id returned when Balance is created. ex: "5656565656565656"</item>
    ///     <item>Amount [long integer]: current balance amount of the workspace in cents. ex: 200 (= R$ 2.00)</item>
    ///     <item>Currency [string]: currency of the current workspace. Expect others to be added eventually. ex: "BRL"</item>
    ///     <item>Updated [DateTime]: update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public class Balance : Resource
    {
        public long Amount { get; }
        public string Currency { get; }
        public DateTime Updated { get; }

        /// <summary>
        /// Balance object
        /// <br/>
        /// The Balance object displays the current balance of the workspace,
        /// which is the result of the sum of all transactions within this
        /// workspace.The balance is never generated by the user, but it
        /// can be retrieved to see the available information.
        /// <br/>
        /// Attributes(return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when Balance is created. ex: "5656565656565656"</item>
        ///     <item>amount [long integer]: current balance amount of the workspace in cents. ex: 200 (= R$ 2.00)</item>
        ///     <item>currency [string]: currency of the current workspace. Expect others to be added eventually. ex: "BRL"</item>
        ///     <item>updated [DateTime]: update datetime for the balance. ex: DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public Balance(string id, long amount, string currency, DateTime updated) : base(id)
        {
            Amount = amount;
            Currency = currency;
            Updated = updated;
        }

        /// <summary>
        /// Retrieve the Balance object
        /// <br/>
        /// Receive the Balance object linked to your workspace in the Stark Bank API
        /// <br/>
        /// Parameters(optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>Balance object with updated attributes</item>
        /// </list>
        /// </summary>
        public static Balance Get(User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object>(),
                user: user
            ).First() as Balance;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "Balance", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            long amount = json.amount;
            string currency = json.currency;
            string updatedString = json.updated;
            DateTime updated = StarkCore.Utils.Checks.CheckDateTime(updatedString);

            return new Balance(id: id, amount: amount, currency: currency, updated: updated);
        }
    }
}
