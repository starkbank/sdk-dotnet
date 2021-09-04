using System;
using System.Collections.Generic;
using System.Linq;
using StarkBank.Utils;

namespace StarkBank
{
    /// <summary>
    /// PaymentPreview object
    /// <br/>
    /// A PaymentPreview is used to get information from a payment code before confirming the payment.
    /// This resource can be used to preview BR Codes and bar codes of boleto, tax and utility payments
    /// <br/>
    /// Properties:
    /// <list>
    ///     <item>ID [string]: Main identification of the payment. This should be the BR Code for Pix payments and lines or bar codes for payment slips. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062", "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
    ///     <item>Scheduled [DateTime, default now]: intended payment date. Right now, this parameter only has effect on BrcodePreviews. ex: new DateTime(2020, 3, 11, 8, 0, 0, 0)</item>
    ///     <item>Type [string]: Payment type. ex: "brcode-payment", "boleto-payment", "utility-payment" or "tax-payment"</item>
    ///     <item>payment [BrcodePreview, BoletoPreview, UtilityPreview or TaxPreview]: Information preview of the informed payment.</item>
    /// </list>
    /// </summary>
    public partial class PaymentPreview : Resource
    {

        public DateTime? Scheduled { get; }
        public string Type { get; }
        public object Payment { get; }

        /// <summary>
        /// PaymentPreview object
        /// <br/>
        /// A PaymentPreview is used to get information from a payment code before confirming the payment.
        /// This resource can be used to preview BR Codes and bar codes of boleto, tax and utility payments
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>ID [string]: Main identification of the payment. This should be the BR Code for Pix payments and lines or bar codes for payment slips. ex: "34191.09008 63571.277308 71444.640008 5 81960000000062", "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A"</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>Scheduled [DateTime, default now]: intended payment date. Right now, this parameter only has effect on BrcodePreviews. ex: new DateTime(2020, 3, 11, 8, 0, 0, 0)</item>
        /// </list>
        /// <br/>
        /// Attributes (return-only):
        /// <list>
        ///     <item>Type [string]: Payment type. ex: "brcode-payment", "boleto-payment", "utility-payment" or "tax-payment"</item>
        ///     <item>payment [BrcodePreview, BoletoPreview, UtilityPreview or TaxPreview]: Information preview of the informed payment.</item>
        /// </list>
        /// </summary>
        public PaymentPreview(string id = null, DateTime? scheduled = null,
            string type = null, object payment = null) : base(id)
        {
            Scheduled = scheduled;
            Type = type;
            Payment = payment;

            var subResourceByType = new Dictionary<string, Api.ResourceMaker>()
            {
                { "brcode-payment",  BrcodePreview.ResourceMaker},
                { "boleto-payment",  BoletoPreview.ResourceMaker},
                { "tax-payment",  TaxPreview.ResourceMaker},
                { "utility-payment",  UtilityPreview.ResourceMaker}
            };

            if (Type != null && subResourceByType.ContainsKey(Type))
            {
                Payment = Api.FromApiJson(subResourceByType[Type], Payment);
            }
        }

        /// <summary>
        /// Create PaymentPreviews
        /// <br/>
        /// Send a list of PaymentPreviews objects for processing in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of PaymentPreviews objects]: list of PaymentPreviews objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PaymentPreviews objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PaymentPreview> Create(List<PaymentPreview> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (PaymentPreview)o);
        }

        /// <summary>
        /// Create PaymentPreviews
        /// <br/>
        /// Send a list of Dictionaries for processing in the Stark Bank API
        /// <br/>
        /// Parameters (required):
        /// <list>
        ///     <item>previews [list of Dictionaries]: list of PaymentPreviews objects to be created in the API</item>
        /// </list>
        /// <br/>
        /// Parameters (optional):
        /// <list>
        ///     <item>user [Organization/Project object]: Organization or Project object. Not necessary if StarkBank.Settings.User was set before function call</item>
        /// </list>
        /// <br/>
        /// Return:
        /// <list>
        ///     <item>list of PaymentPreviews objects with updated attributes</item>
        /// </list>
        /// </summary>
        public static List<PaymentPreview> Create(List<Dictionary<string, object>> previews, User user = null)
        {
            (string resourceName, Api.ResourceMaker resourceMaker) = Resource();
            return Rest.Post(
                resourceName: resourceName,
                resourceMaker: resourceMaker,
                entities: previews,
                user: user
            ).ToList().ConvertAll(o => (PaymentPreview)o);
        }

        internal static (string resourceName, Api.ResourceMaker resourceMaker) Resource()
        {
            return (resourceName: "PaymentPreview", resourceMaker: ResourceMaker);
        }

        public static SubResource ResourceMaker(dynamic json)
        {
            string id = json.id;
            string scheduledString = json.scheduled;
            DateTime? scheduled = Checks.CheckNullableDateTime(scheduledString);
            string type = json.type;
            object payment = json.payment;

            return new PaymentPreview(
                id: id, scheduled: scheduled, type: type, payment: payment
            );
        }
    }
}
