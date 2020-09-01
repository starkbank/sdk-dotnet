using System;
using System.Linq;
using System.Collections.Generic;

namespace StarkBank
{
	/// <summary>
	/// DictKey object
	/// <br/>
	/// DictKey represents a PIX key registered in Bacens DICT system.
	/// <br/>
	/// Properties:
	/// <list>
	///     <item> ID [string]: DictKey object unique id and DICT key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"
	///     <item> Type [string, default null]: DICT key type. ex: "email", "cpf", "cnpj", "phone" or "evp"
	///     <item> Name [string, default null]: account owner full name. ex: "Tony Stark"
	///     <item> TaxId [string, default null]: tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"
	///     <item> OwnerType [string, default null]: DICT key owner type. ex "naturalPerson" or "legalPerson"
	///     <item> Ispb [string, default null]: ISPB code used for transactions. ex: "20018183"
	///     <item> BranchCode [string, default null]: bank account branch code associated with the DICT key. ex: "9585"
	///     <item> AccountNumber [string, default null]: bank account number associated with the DICT key. ex: "9828282578010513"
	///     <item> AccountType [string, default null]: bank account type associated with the DICT key. ex: "checking", "saving" e "salary"
	///     <item> Status [string, default null]: current DICT key status. ex: "created", "registered", "canceled" or "failed"
	///     <item> AccountCreated [string, default null]: creation datetime of the bank account associated with the DICT key. ex: "2020-11-05T14:55:08.812665+00:00"
	///     <item> Owned [string, default null]: datetime since when the current owner hold this DICT key. ex : "2020-11-05T14:55:08.812665+00:00"     
	///     <item> Created [string, default null]: creation datetime for the DICT key. ex: "2020-03-10 10:30:00.000"
	/// </list>
	/// </summary>
	public partial class DictKey : Utils.Resource
    {
		public string Type { get; }
		public string Name { get; }
		public string TaxId { get; }
		public string OwnerType { get; }
		public string Ispb { get; }
		public string BranchCode { get; }
		public string AccountNumber { get; }
		public string AccountType { get; }
		public string Status { get; }
		public string AccountCreated { get; }
		public string Owned { get; }
		public string Created { get; }
		/// <summary>
		/// DictKey object
		/// <br/>
		/// DictKey represents a PIX key registered in Bacen"s DICT system.
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item> id [string]: DictKey object unique id and PIX key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"
		/// Attributes (return-only):
		/// <list>
		///     <item> Type [string, default null]: PIX key type. ex: "email", "cpf", "cnpj", "phone" or "evp"
		///     <item> Name [string, default null]: account owner full name. ex: "Tony Stark"
		///     <item> TaxId [string, default null]: tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"
		///     <item> OwnerType [string, default null]: PIX key owner type. ex "naturalPerson" or "legalPerson"
		///     <item> Ispb [string, default null]: ISPB code used for transactions. ex: "20018183"
		///     <item> BranchCode [string, default null]: bank account branch code associated with the PIX key. ex: "9585"
		///     <item> AccountNumber [string, default null]: bank account number associated with the PIX key. ex: "9828282578010513"
		///     <item> AccountType [string, default null]: bank account type associated with the PIX key. ex: "checking", "saving" e "salary"
		///     <item> Status [string, default null]: current PIX key status. ex: "created", "registered", "canceled" or "failed"
		///     <item> AccountCreated [DateTime, default null]: creation datetime of the bank account associated with the PIX key. ex: "2020-11-05T14:55:08.812665+00:00"
		///     <item> Owned [DateTime, default null]: datetime since when the current owner hold this PIX key. ex : "2020-11-05T14:55:08.812665+00:00"     
		///     <item> Created [DateTime, default null]: creation datetime for the PIX key. ex: "2020-03-10 10:30:00.000"
		/// </list>
		/// </summary>
		public DictKey(string id = null, string type = null, string name = null, string taxId = null, string ownerType = null, string ispb = null,
					string branchCode = null, string accountNumber = null, string accountType = null, string status = null,
					string accountCreated = null, string owned = null, string created = null) : base(id)
		{
			Type = type;
			Name = name;
			TaxId = taxId;
			OwnerType = ownerType;
			Ispb = ispb;
			BranchCode = branchCode;
			AccountNumber = accountNumber;
			AccountType = accountType;
			Status = status;
			AccountCreated = accountCreated;
			Owned = owned;
			Created = created;
		}

		/// <summary>
		/// Retrieve a specific DictKey
		/// <br/>
		/// Receive a single DictKey object by passing its id
		/// <br/>
		/// Parameters(required):
		/// <list>
		///     <item> id [string]: DictKey object unique id and PIX key itself. ex: "tony@starkbank.com", "722.461.430-04", "20.018.183/0001-80", "+5511988887777", "b6295ee1-f054-47d1-9e90-ee57b74f60d9"
		/// </list>
		/// <br/>
		/// Parameters(optional):
		/// <list>
		///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>DictKey object with updated attributes</item>
		/// </list>
		/// </summary>
		public static DictKey Get(string id, User user = null)
		{
			(string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
			return Utils.Rest.GetId(
				resourceName: resourceName,
				resourceMaker: resourceMaker,
				id: id,
				user: user
			) as DictKey;
		}

		/// <summary>
		/// Retrieve DictKeys
		/// <br/>
		/// Receive an IEnumerable of DictKey objects associated with your Stark Bank Workspace
		/// <br/>
		/// Parameters (optional):
		/// <list>
		///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
		///     <item>type [string, default null]: DictKey type.ex: "cpf", "cnpj", "phone", "email" or "evp"<item>
		///     <item>after [DateTime, default null] date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>before [DateTime, default null] date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
		///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
		///     <item>status [string, default null]: filter for status of retrieved objects. ex: "created", "paid", "canceled" or "overdue"</item>
		///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
		/// </list>
		/// <br/>
		/// Return:
		/// <list>
		///     <item>IEnumerable of DictKey objects with updated attributes</item>
		/// </list>
		/// </summary>
		public static IEnumerable<DictKey> Query(int? limit = null, string type = null, DateTime? after = null,
			DateTime? before = null, List<string> ids = null, string status = null, User user = null)
		{
			(string resourceName, Utils.Api.ResourceMaker resourceMaker) = Resource();
			return Utils.Rest.GetList(
				resourceName: resourceName,
				resourceMaker: resourceMaker,
				query: new Dictionary<string, object> {
					{ "limit", limit },
					{ "type", type },
					{ "after", new Utils.StarkBankDate(after) },
					{ "before", new Utils.StarkBankDate(before) },
					{ "ids", ids },
					{ "status", status }
				},
				user: user
			).Cast<DictKey>();
		}

		internal static (string resourceName, Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "DictKey", resourceMaker: ResourceMaker);
        }

        internal static Utils.Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
			string type = json.type;
			string name = json.name;
			string taxId = json.taxId;
			string ownerType = json.ownerType;
			string ispb = json.ispb;
			string branchCode = json.branchCode;
			string accountNumber = json.accountNumber;
			string accountType = json.accountType;
			string status = json.status;
			string accountCreated = json.accountCreated;
			string owned = json.owned;
            string created = json.created;

            return new DictKey(
                id: id, type: type, accountCreated: accountCreated, accountType: accountType, name: name,
				taxId: taxId, ownerType: ownerType, ispb: ispb, branchCode: branchCode, accountNumber: accountNumber,
				status: status, owned: owned, created: created
            );
        }
	}
}
