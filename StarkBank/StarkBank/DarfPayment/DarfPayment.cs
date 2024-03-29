using System;
using System.Linq;
using StarkBank.Utils;
using System.Collections.Generic;

namespace StarkBank
{
    /// <summary>
    /// DarfPayment object
    /// <br/>
    /// When you initialize a DarfPayment, the entity will not be automatically
    /// created in the Stark Bank API. The 'create' function sends the objects
    /// to the Stark Bank API and returns the list of created objects.
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>Description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
    ///     <item>RevenueCode [string]: 4-digit tax code assigned by Federal Revenue. ex: "5948"</item>
    ///     <item>TaxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
    ///     <item>Competence [DateTime]: competence month of the service. ex: datetime.date(2021, 4, 30)</item>
    ///     <item>NominalAmount [int]: amount due in cents without fee or interest. ex: 23456 (= R$ 234.56)</item>
    ///     <item>FineAmount [int]: fixed amount due in cents for fines. ex: 234 (= R$ 2.34)</item>
    ///     <item>InterestAmount [int]: amount due in cents for interest. ex: 456 (= R$ 4.56)</item>
    ///     <item>Due [DateTime]: due date for payment. ex: datetime.date(2021, 5, 17)</item>
    ///     <item>ReferenceNumber [string, null]: number assigned to the region of the tax. ex: "08.1.17.00-4"</item>
    ///     <item>Scheduled [DateTime or string, default today]: payment scheduled date. ex: DateTime(2020, 3, 10)</item>
    ///     <item>Tags [list of strings, default null]: list of strings for tagging</item>
    ///     <item>ID [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
    ///     <item>Status [string]: current payment status. ex: "success" or "failed"</item>
    ///     <item>Amount [int]: Total amount due calculated from other amounts. ex: 24146 (= R$ 241.46)</item>
    ///     <item>Fee [integer]: fee charged when the DarfPayment is processed. ex: 0 (= R$ 0.00)</item>
    ///     <item>TransactionIds [list of strings]: ledger transaction ids linked to this DarfPayment. ex: ["19827356981273"]</item>
    ///     <item>Updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    ///     <item>Created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
    /// </list>
    /// </summary>
    public partial class DarfPayment : Resource
    {
        public string Description { get; }
        public string RevenueCode { get; }
        public string TaxID { get; }
        public DateTime Competence { get; }
        public long FineAmount { get; }
        public long NominalAmount { get; }
        public long InterestAmount { get; }
        public DateTime Due { get; }
        public string ReferenceNumber { get; }
        public DateTime? Scheduled { get; }
        public List<string> Tags { get; }
        public string Status { get; }
        public long? Amount { get; }
        public string Fee { get; }
        public List<string> TransactionIds { get; }
        public DateTime? Updated { get; }
        public DateTime? Created { get; }

        /// <summary>
        /// DarfPayment object
        /// <br/>
        /// When you initialize a DarfPayment, the entity will not be automatically
        /// created in the Stark Bank API. The 'create' function sends the objects
        /// to the Stark Bank API and returns the list of created objects.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>description [string]: Text to be displayed in your statement (min. 10 characters). ex: "payment ABC"</item>
        ///     <item>revenueCode [string]: 4-digit tax code assigned by Federal Revenue. ex: "5948"</item>
        ///     <item>taxID [string]: payer tax ID (CPF or CNPJ) with or without formatting. ex: "01234567890" or "20.018.183/0001-80"</item>
        ///     <item>competence [DateTime]: competence month of the service. ex: datetime.date(2021, 4, 30)</item>
        ///     <item>nominalAmount [int]: amount due in cents without fee or interest. ex: 23456 (= R$ 234.56)</item>
        ///     <item>fineAmount [int]: fixed amount due in cents for fines. ex: 234 (= R$ 2.34)</item>
        ///     <item>interestAmount [int]: amount due in cents for interest. ex: 456 (= R$ 4.56)</item>
        ///     <item>due [DateTime]: due date for payment. ex: datetime.date(2021, 5, 17)</item>
        /// </list>
        /// Parameters (optional):
        /// <list>
        ///     <item>referenceNumber [string, null]: number assigned to the region of the tax. ex: "08.1.17.00-4"</item>
        ///     <item>scheduled [DateTime or string, default today]: payment scheduled date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings]: list of strings for tagging</item>
        /// </list>
        /// Attributes (return-only):
        /// <list>
        ///     <item>id [string]: unique id returned when payment is created. ex: "5656565656565656"</item>
        ///     <item>status [string]: current payment status. ex: "success" or "failed"</item>
        ///     <item>amount [int]: Total amount due calculated from other amounts. ex: 24146 (= R$ 241.46)</item>
        ///     <item>fee [integer]: fee charged when the DarfPayment is processed. ex: 0 (= R$ 0.00)</item>
        ///     <item>transactionIds [list of strings]: ledger transaction ids linked to this DarfPayment. ex: ["19827356981273"]</item>
        ///     <item>updated [DateTime]: latest update datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        ///     <item>created [DateTime]: creation datetime for the payment. ex: new DateTime(2020, 3, 10, 10, 30, 0, 0)</item>
        /// </list>
        /// </summary>
        public DarfPayment(string description, string revenueCode, string taxID, DateTime competence, long fineAmount,
            long nominalAmount, long interestAmount, DateTime due, string id = null, string referenceNumber = null,
            DateTime? scheduled = null, List<string> tags = null, string status = null, long? amount = null,
            string fee = null, List<string> transactionIds = null, DateTime? updated = null, DateTime? created = null
        ) : base(id)
        {
            Description = description;
            RevenueCode = revenueCode;
            TaxID = taxID;
            Competence = competence;
            FineAmount = fineAmount;
            NominalAmount = nominalAmount;
            InterestAmount = interestAmount;
            Due = due;
            ReferenceNumber = referenceNumber;
            Scheduled = scheduled;
            Tags = tags;
            Status = status;
            Amount = amount;
            Fee = fee;
            TransactionIds = transactionIds;
            Updated = updated;
            Created = created;
        }

        /// <summary>
        /// Create DarfPayments
        /// <br/>
        /// Send a list of DarfPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of DarfPayment objects]: list of DarfPayment objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DarfPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DarfPayment> Create(List<DarfPayment> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (DarfPayment)o);
        }

        /// <summary>
        /// Create DarfPayments
        /// <br/>
        /// Send a list of DarfPayment objects for creation in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>payments [list of Dictionaries]: list of Dictionaries representing the DarfPayments to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DarfPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<DarfPayment> Create(List<Dictionary<string, object>> payments, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: payments,
                user: user
            ).ToList().ConvertAll(o => (DarfPayment)o);
        }

        /// <summary>
        /// Retrieve a specific DarfPayment
        /// <br/>
        /// Receive a single DarfPayment object previously created by the Stark Bank API by passing its id
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>DarfPayment object with updated attributes</item>
        /// </list>
        /// </summary>
        public static DarfPayment Get(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as DarfPayment;
        }

        /// <summary>
        /// Retrieve a specific DarfPayment pdf file
        /// <br/>
        // Receive a single DarfPayment pdf file generated in the Stark Bank API by passing its id.
        // Only valid for tax payments with "success" status.
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: object unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>DarfPayment pdf file</item>
        /// </list>
        /// </summary>
        public static byte[] Pdf(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetContent(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                subResourceName: "pdf",
                id: id,
                user: user
            );
        }

        /// <summary>
        /// Retrieve DarfPayments
        /// <br/>
        /// Receive an IEnumerable of DarfPayment objects previously created in the Stark Bank API
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>IEnumerable of DarfPayment objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static IEnumerable<DarfPayment> Query(int? limit = null, DateTime? after = null, DateTime? before = null,
            List<string> tags = null, List<string> ids = null, string status = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.GetList(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "tags", tags },
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            ).Cast<DarfPayment>();
        }

        /// <summary>
        /// Retrieve paged DarfPayments
        /// <br/>
        /// Receive a list of up to 100 DarfPayment objects previously created in the Stark Bank API and the cursor to the next page.
        /// Use this function instead of query if you want to manually page your requests.
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>cursor [string, default null]: cursor returned on the previous page function call</item>
        ///     <item>limit [integer, default null]: maximum number of objects to be retrieved. Unlimited if null. ex: 35</item>
        ///     <item>after [DateTime, default null]: date filter for objects created only after specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>before [DateTime, default null]: date filter for objects created only before specified date. ex: DateTime(2020, 3, 10)</item>
        ///     <item>tags [list of strings, default null]: tags to filter retrieved objects. ex: ["tony", "stark"]</item>
        ///     <item>ids [list of strings, default null]: list of ids to filter retrieved objects. ex: ["5656565656565656", "4545454545454545"]</item>
        ///     <item>status [string, default null]: filter for status of retrieved objects. ex: "paid"</item>
        ///     <item>user [Project object, default null]: Project object. Not necessary if StarkBank.User.Default was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of DarfPayment objects with updated attributes and cursor to retrieve the next page of DarfPayment objects</item>
        /// </list>
        /// </summary>
        public static (List<DarfPayment> page, string pageCursor) Page(string cursor = null, int? limit = null, DateTime? after = null,
            DateTime? before = null, List<string> tags = null, List<string> ids = null, string status = null, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            (List<StarkCore.Utils.SubResource> page, string pageCursor) = Rest.GetPage(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                query: new Dictionary<string, object> {
                    { "cursor", cursor },
                    { "limit", limit },
                    { "after", new StarkCore.Utils.StarkDate(after) },
                    { "before", new StarkCore.Utils.StarkDate(before) },
                    { "tags", tags },
                    { "ids", ids },
                    { "status", status }
                },
                user: user
            );
            List<DarfPayment> darfPayments = new List<DarfPayment>();
            foreach (StarkCore.Utils.SubResource subResource in page)
            {
                darfPayments.Add(subResource as DarfPayment);
            }
            return (darfPayments, pageCursor);
        }

        /// <summary>
        /// Delete a DarfPayment entity
        /// <br/>
        /// Delete a DarfPayment entity previously created in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>id [string]: DarfPayment unique id. ex: "5656565656565656"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>deleted DarfPayment object</item>
        /// </list>
        /// </summary>
        public static DarfPayment Delete(string id, User user = null)
        {
            (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) = Resource();
            return Rest.DeleteId(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                id: id,
                user: user
            ) as DarfPayment;
        }

        internal static (string resourceName, StarkCore.Utils.Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "DarfPayment", resourceMaker: ResourceMaker);
        }

        internal static Resource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string description = json.description;
            string revenueCode = json.revenueCode;
            string taxID = json.taxId;
            long fineAmount = json.fineAmount;
            long nominalAmount = json.nominalAmount;
            long interestAmount = json.interestAmount;
            string referenceNumber = json.referenceNumber;
            string status = json.status;
            long? amount = json.amount;
            string fee = json.fee;
            List<string> tags = new List<string>();
            if (json.tags != null)
            {
                tags = json.tags.ToObject<List<string>>();
            }
            string competenceString = json.competence;
            DateTime competence = StarkCore.Utils.Checks.CheckDateTime(competenceString);
            string dueString = json.scheduled;
            DateTime due = StarkCore.Utils.Checks.CheckDateTime(dueString);
            string scheduledString = json.scheduled;
            DateTime? scheduled = StarkCore.Utils.Checks.CheckNullableDateTime(scheduledString);
            string createdString = json.created;
            DateTime? created = StarkCore.Utils.Checks.CheckNullableDateTime(createdString);
            string updatedString = json.updated;
            DateTime? updated = StarkCore.Utils.Checks.CheckNullableDateTime(updatedString);

            return new DarfPayment(
                description: description, revenueCode: revenueCode, taxID: taxID, competence: competence,
                fineAmount: fineAmount, nominalAmount: nominalAmount, interestAmount: interestAmount,
                due: due, id: id, referenceNumber: referenceNumber, scheduled: scheduled, tags: tags,
                status: status, amount: amount, fee: fee, updated: updated, created: created
            );
        }
    }
}
