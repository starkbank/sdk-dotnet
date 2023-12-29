using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// CorporateRule object
    /// <br/>
    /// The CorporateRule object displays the spending rules of CorporateCards and CorporateHolders created in your Workspace.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Name [string]: rule name. ex: "Travel" or "Food"</item>
    ///     <item>Amount [integer]: maximum amount that can be spent in the informed interval. ex: 200000 (= R$ 2000.00)</item>
    ///     <item>ID [string, default null]: unique id returned when a CorporateRule is created, used to update a specific CorporateRule. ex: "5656565656565656"</item>
    ///     <item>Interval [string, default "lifetime"]: interval after which the rule amount counter will be reset to 0. ex: "instant", "day", "week", "month", "year" or "lifetime"</item>
    ///     <item>Schedule [string, default null]: schedule time for user to spend. ex: "every monday, wednesday from 00:00 to 23:59 in America/Sao_Paulo"</item>
    ///     <item>Purposes [list of string, default null]: list of strings representing the allowed purposes for card purchases, you can use this to restrict ATM withdrawals. ex: ["purchase", "withdrawal"]</item>
    ///     <item>CurrencyCode [string, default "BRL"]: code of the currency that the rule amount refers to. ex: "BRL" or "USD"</item>
    ///     <item>Categories [list of MerchantCategories, default null]: merchant categories accepted by the rule. ex: new List<string>{ new MerchantCategory() }</item>
    ///     <item>Countries [list of MerchantCountries, default null]: countries accepted by the rule. ex: new List<string>{ new MerchantCountry() }</item>
    ///     <item>Methods [list of CardMethods, default null]: card purchase methods accepted by the rule. ex: new List<string>{ new CardMethod() }</item>
    ///     <item>CounterAmount [integer]: current rule spent amount. ex: 1000</item>
    ///     <item>CurrencySymbol [string]: currency symbol. ex: "R$""</item>
    ///     <item>CurrencyName [string]: currency name. ex: "Brazilian Real"</item>
    /// </list>
    /// </summary>
    ///
    public partial class CorporateRule : Resource
    {
        public string Name { get; }
        public long Amount { get; }
        public string Interval { get; }
        public string Schedule { get; }
        public string CurrencyCode { get; }
        public List<string> Purposes { get; }
        public List<MerchantCategory> Categories { get; }
        public List<MerchantCountry> Countries { get; }
        public List<CardMethod> Methods { get; }
        public string CounterAmount { get; }
        public string CurrencySymbol { get; }
        public string CurrencyName { get; }

        /// <summary>
        /// CorporateRule object
        /// <br/>
        /// The CorporateRule object displays the spending rules of CorporateCards and CorporateHolders created in your Workspace.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>name [string]: rule name. ex: "Travel" or "Food"</item>
        ///     <item>amount [integer]: maximum amount that can be spent in the informed interval. ex: 200000 (= R$ 2000.00)</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>id [string, default null]: unique id returned when a CorporateRule is created, used to update a specific CorporateRule. ex: "5656565656565656"</item>
        ///     <item>interval [string, default "lifetime"]: interval after which the rule amount counter will be reset to 0. ex: "instant", "day", "week", "month", "year" or "lifetime"</item>
        ///     <item>schedule [string, default null]: schedule time for user to spend. ex: "every monday, wednesday from 00:00 to 23:59 in America/Sao_Paulo"</item>
        ///     <item>purposes [list of string, default null]: list of strings representing the allowed purposes for card purchases, you can use this to restrict ATM withdrawals. ex: ["purchase", "withdrawal"]</item>
        ///     <item>currencyCode [string, default "BRL"]: code of the currency that the rule amount refers to. ex: "BRL" or "USD"</item>
        ///     <item>categories [list of MerchantCategories, default null]: merchant categories accepted by the rule. ex: new List<string>{ new MerchantCategory() }</item>
        ///     <item>countries [list of MerchantCountries, default null]: countries accepted by the rule. ex: new List<string>{ new MerchantCountry() }</item>
        ///     <item>methods [list of CardMethods, default null]: card purchase methods accepted by the rule. ex: new List<string>{ new CardMethod() }</item>
        /// </list>
        /// Attributes (expanded return-only):
        /// <list>
        ///     <item>counterAmount [integer]: current rule spent amount. ex: 1000</item>
        ///     <item>currencySymbol [string]: currency symbol. ex: "R$""</item>
        ///     <item>currencyName [string]: currency name. ex: "Brazilian Real"</item>
        /// </list>
        /// </summary>
        public CorporateRule(string name, long amount, string interval = null, string schedule = null, List<string> purposes = null, string currencyCode = "BRL", 
            List<MerchantCategory> categories = null, List<MerchantCountry> countries = null, string id = null, List<CardMethod> methods = null, 
            string counterAmount = null, string currencySymbol = null, string currencyName = null
        ) : base(id)
        { 
            Name = name;
            Amount = amount;
            Interval = interval;
            Schedule = schedule;
            Purposes = purposes;
            CurrencyCode = currencyCode;
            Categories = categories;
            Countries = countries;
            Methods = methods;
            CounterAmount = counterAmount;
            CurrencySymbol = currencySymbol;
            CurrencyName = currencyName;
        }
        
        public static List<CorporateRule> ParseRules(dynamic json)
        {
            if (json == null) return null;

            List<CorporateRule> rules = new List<CorporateRule>();

            foreach (dynamic rule in json)
            {
                rules.Add(CorporateRule.ResourceMaker(rule));
            }
            return rules;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "CorporateRule", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string name = json.name;
            long amount = json.amount;
            string interval = json.interval;
            string schedule = json.schedule;
            string currencyCode = json.currencyCode;
            List<string> purposes = json.purposes?.ToObject<List<string>>();
            List<MerchantCategory> categories = MerchantCategory.ParseCategories(json.categories);
            List<MerchantCountry> countries = MerchantCountry.ParseCountries(json.countries);
            List<CardMethod> methods = CardMethod.ParseMethods(json.methods);
            string counterAmount = json.counterAmount;
            string currencySymbol = json.currencySymbol;
            string currencyName = json.currencyName;

            return new CorporateRule(
                id: id, name: name, amount: amount, interval: interval, schedule: schedule, purposes: purposes, currencyCode: currencyCode, 
                categories: categories, countries: countries, methods: methods, counterAmount: counterAmount, currencySymbol: currencySymbol, 
                currencyName: currencyName
            );
        }
    }
}
