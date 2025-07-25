# Stark Bank .NET SDK

Welcome to the Stark Bank .NET SDK! This tool is made for .NET
developers who want to easily integrate with our API.
This SDK version is compatible with the Stark Bank API v2.

If you have no idea what Stark Bank is, check out our [website](https://www.starkbank.com/)
and discover a world where receiving or making payments
is as easy as sending a text message to your client!

# Introduction

# Index

- [Introduction](#introduction)
    - [Supported .NET versions](#supported-net-versions)
    - [API documentation](#stark-bank-api-documentation)
    - [Versioning](#versioning)
- [Setup](#setup)
    - [Install our SDK](#1-install-our-sdk)
    - [Create your Private and Public Keys](#2-create-your-private-and-public-keys)
    - [Register your user credentials](#3-register-your-user-credentials)
    - [Setting up the user](#4-setting-up-the-user)
    - [Setting up the error language](#5-setting-up-the-error-language)
    - [Resource listing and manual pagination](#6-resource-listing-and-manual-pagination)
- [Testing in Sandbox](#testing-in-sandbox) 
- [Usage](#usage)
    - [Transactions](#create-transactions): Account statement entries
    - [Balance](#get-balance): Account balance
    - [Transfers](#create-transfers): Wire transfers (TED and manual Pix)
    - [DictKeys](#get-dict-key): Pix Key queries to use with Transfers
    - [Institutions](#query-bacen-institutions): Instutitions recognized by the Central Bank
    - [Invoices](#create-invoices): Reconciled receivables (dynamic Pix QR Codes)
    - [DynamicBrcode](#create-dynamicbrcodes): Simplified reconciled receivables (dynamic Pix QR Codes)
    - [Deposits](#query-deposits): Other cash-ins (static Pix QR Codes, DynamicBrcodes, manual Pix, etc)
    - [Boletos](#create-boletos): Boleto receivables
    - [BoletoHolmes](#investigate-a-boleto): Boleto receivables investigator
    - [BrcodePayments](#pay-a-br-code): Pay Pix QR Codes
    - [BoletoPayments](#pay-a-boleto): Pay Boletos
    - [UtilityPayments](#create-utility-payments): Pay Utility bills (water, light, etc.)
    - [TaxPayments](#create-tax-payments): Pay taxes
    - [DarfPayments](#create-darf-payment): Pay DARFs
    - [PaymentPreviews](#preview-payment-information-before-executing-the-payment): Preview all sorts of payments
    - [PaymentRequest](#create-payment-requests-to-be-approved-by-authorized-people-in-a-cost-center): Request a payment approval to a cost center
    - [CorporateHolders](#create-corporateholders): Manage cardholders
    - [CorporateCards](#create-corporatecards): Create virtual and/or physical cards
    - [CorporateInvoices](#create-corporateinvoices): Add money to your corporate balance
    - [CorporateWithdrawals](#create-corporatewithdrawals): Send money back to your Workspace from your corporate balance
    - [CorporateBalance](#get-your-corporatebalance): View your corporate balance
    - [CorporateTransactions](#query-corporatetransactions): View the transactions that have affected your corporate balance
    - [CorporateEnums](#corporate-enums): Query enums related to the corporate purchases, such as merchant categories, countries and card purchase methods
    - [MerchantSession](#merchant-session): The Merchant Session allows you to create a session prior to a purchase. Sessions are essential for defining the parameters of a purchase, including funding type, expiration, 3DS, and more.
    - [MerchantCard](#merchant-card): The Merchant Card resource stores information about cards used in approved purchases.
    - [MerchantInstallment](#merchant-installment): Merchant Installments are created for every installment in a purchase.
    - [MerchantPurchase](#merchant-purchase): The Merchant Purchase section allows users to retrieve detailed information of the purchases.
    - [Webhooks](#create-a-webhook-subscription): Configure your webhook endpoints and subscriptions
    - [WebhookEvents](#process-webhook-events): Manage webhook events
    - [WebhookEventAttempts](#query-failed-webhook-event-delivery-attempts-information): Query failed webhook event deliveries
    - [Workspaces](#create-a-new-workspace): Manage your accounts
    - [Request](#request): Send a custom request to Stark Bank. This can be used to access features that haven't been mapped yet.
- [Handling errors](#handling-errors)
- [Help and Feedback](#help-and-feedback)

# Supported .NET Versions

This library supports the following .NET versions:

* .NET Standard 2.0+

# Stark Bank API documentation

Feel free to take a look at our [API docs](https://www.starkbank.com/docs/api).

# Versioning

This project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.

# Setup

## 1. Install our SDK

StarkBank`s .NET SDK is available on NuGet as starkbank 2.16.1.

1.1 To install the Package Manager:

```sh
Install-Package starkbank -Version 2.16.1
```

1.2 To install the .NET CLI:

```sh
dotnet add package starkbank --version 2.16.1
```

1.3 To install by PackageReference:

```sh
<PackageReference Include="starkbank" Version="2.16.1" />
```

1.4 To install with Paket CLI:

```sh
paket add starkbank --version 2.16.1
```

## 2. Create your Private and Public Keys

We use ECDSA. That means you need to generate a secp256k1 private
key to sign your requests to our API, and register your public key
with us so we can validate those requests.

You can use one of following methods:

2.1. Check out the options in our [tutorial](https://starkbank.com/faq/how-to-create-ecdsa-keys).

2.2. Use our SDK:

```c#
(string privateKey, string publicKey) = StarkBank.Key.Create();

# or, to also save .pem files in a specific path
(string privateKey, string publicKey) = StarkBank.Key.Create("file/keys");
```

**NOTE**: When you are creating a new integration user, it is recommended that you create the
keys inside the infrastructure that will use it, in order to avoid risky internet
transmissions of your **private-key**. Then you can export the **public-key** alone to the
computer where it will be used in the new Project creation.

## 3. Register your user credentials

You can interact directly with our API using two types of users: Projects and Organizations.

- **Projects** are workspace-specific users, that is, they are bound to the workspaces they are created in.
One workspace can have multiple Projects.
- **Organizations** are general users that control your entire organization.
They can control all your Workspaces and even create new ones. The Organization is bound to your company's tax ID only.
Since this user is unique in your entire organization, only one credential can be linked to it.


3.1. To create a Project in Sandbox:

3.1.1. Log into [Starkbank Sandbox](https://web.sandbox.starkbank.com)

3.1.2. Go to Menu > Integrations

3.1.3. Click on the "New Project" button

3.1.4. Create a Project: Give it a name and upload the public key you created in section 2

3.1.5. After creating the Project, get its Project ID

3.1.6. Use the Project ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
string privateKeyContent = "-----BEGIN EC PARAMETERS-----\nBgUrgQQACg==\n-----END EC PARAMETERS-----\n-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIMCwW74H6egQkTiz87WDvLNm7fK/cA+ctA2vg/bbHx3woAcGBSuBBAAK\noUQDQgAE0iaeEHEgr3oTbCfh8U2L+r7zoaeOX964xaAnND5jATGpD/tHec6Oe9U1\nIF16ZoTVt1FzZ8WkYQ3XomRD4HS13A==\n-----END EC PRIVATE KEY-----";

StarkBank.Project project = new StarkBank.Project(
    environment: "sandbox",
    id: "5656565656565656",
    privateKey: privateKeyContent
);
```

3.2. To create Organization credentials in Sandbox:

3.2.1. Log into [Starkbank Sandbox](https://web.sandbox.starkbank.com)

3.2.2. Go to Menu > Integrations

3.2.3. Click on the "Organization public key" button

3.2.4. Upload the public key you created in section 2 (only a legal representative of the organization can upload the public key)

3.2.5. Click on your profile picture and then on the "Organization" menu to get the Organization ID

3.2.6. Use the Organization ID and private key to create the object below:

```c#
// Get your private key from an environment variable or an encrypted database.
// This is only an example of a private key content. You should use your own key.
string privateKeyContent = "-----BEGIN EC PARAMETERS-----\nBgUrgQQACg==\n-----END EC PARAMETERS-----\n-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIMCwW74H6egQkTiz87WDvLNm7fK/cA+ctA2vg/bbHx3woAcGBSuBBAAK\noUQDQgAE0iaeEHEgr3oTbCfh8U2L+r7zoaeOX964xaAnND5jATGpD/tHec6Oe9U1\nIF16ZoTVt1FzZ8WkYQ3XomRD4HS13A==\n-----END EC PRIVATE KEY-----";

StarkBank.Organization organization = new StarkBank.Organization(
    environment: "sandbox",
    id: "5656565656565656",
    privateKey: privateKeyContent,
    workspaceID: null  // You only need to set the workspaceID when you are operating a specific workspaceID
);

// To dynamically use your organization credentials in a specific workspaceID,
// you can use the Organization.Replace() method:
StarkBank.Balance.Get(user: Organization.Replace(organization, "4848484848484848"));
```

NOTE 1: Never hard-code your private key. Get it from an environment variable or an encrypted database.

NOTE 2: We support `'sandbox'` and `'production'` as environments.

NOTE 3: The credentials you registered in `sandbox` do not exist in `production` and vice versa.


## 4. Setting up the user

There are two kinds of users that can access our API: **Project** and **Member**.

- `Project` and `Organization` are designed for integrations and are the ones meant for our SDKs.
- `Member` is the one you use when you log into our webpage with your e-mail.

There are two ways to inform the user to the SDK:

4.1 Passing the user as argument in all functions:

```c#
StarkBank.Balance balance = StarkBank.Balance.Get(user: project); //or organization
```

4.2 Set it as a default user in the SDK:

```c#
StarkBank.Settings.User = project; //or organization

StarkBank.Balance balance = StarkBank.Balance.Get();
```

Just select the way of passing the user that is more convenient to you.
On all following examples we will assume a default user has been set.

## 5. Setting up the error language

The error language can also be set in the same way as the default user:

```c#
StarkBank.Settings.Language = "en-US";
```

Language options are "en-US" for english and "pt-BR" for brazilian portuguese. English is default.

## 6. Resource listing and manual pagination

Almost all SDK resources provide a `Query` and a `Page` function.

- The `Query` function provides a straight forward way to efficiently iterate through all results that match the filters you inform,
seamlessly retrieving the next batch of elements from the API only when you reach the end of the current batch.
If you are not worried about data volume or processing time, this is the way to go.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Transaction> transactions = StarkBank.Transaction.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Transaction transaction in transactions) {
    Console.WriteLine(transaction);
}
```

- The `Page` function gives you full control over the API pagination. With each function call, you receive up to
100 results and the cursor to retrieve the next batch of elements. This allows you to stop your queries and
pick up from where you left off whenever it is convenient. When there are no more elements to be retrieved, the returned cursor will be `null`.


```c#
using System;
using System.Collections.Generic;

List<Transaction> page;
string cursor = null;

while (true)
{
    (page, cursor) = StarkBank.Transaction.Page(cursor: cursor);
    foreach (Transaction entity in page)
    {
        Console.WriteLine(entity);
    }
    if (cursor == null)
    {
        break;
    }
}
```

To simplify the following SDK examples, we will only use the `Query` function, but feel free to use `Page` instead.

# Testing in Sandbox

Your initial balance is zero. For many operations in Stark Bank, you'll need funds
in your account, which can be added to your balance by creating an Invoice or a Boleto. 

In the Sandbox environment, most of the created Invoices and Boletos will be automatically paid,
so there's nothing else you need to do to add funds to your account. Just create
a few Invoices and wait around a bit.

In Production, you (or one of your clients) will need to actually pay this Invoice or Boleto
for the value to be credited to your account.

# Usage

Here are a few examples on how to use the SDK. If you have any doubts, check out
the function or class docstring to get more info or go straight to our [API docs].

## Create transactions

To send money between Stark Bank accounts, you can create transactions:

```c#
using System;
using System.Collections.Generic;

List<StarkBank.Transaction> transactions = StarkBank.Transaction.Create(
    new List<StarkBank.Transaction> {
        new StarkBank.Transaction(
            amount: 100,  // (R$ 1.00)
            receiverID: "1029378109327810",
            description: "Transaction to dear provider",
            externalID: "12345",  // so we can block anything you send twice by mistake
            tags: new List<string> { "provider" }
        ),
        new StarkBank.Transaction(
            amount: 234,  // (R$ 2.34)
            receiverID: "2093029347820947",
            description: "Transaction to the other provider",
            externalID: "12346",  // so we can block anything you send twice by mistake
            tags: new List<string> { "provider" }
        )
    }
);

foreach(StarkBank.Transaction transaction in transactions) {
    Console.WriteLine(transaction);
}
```

**Note**: Instead of using Transaction objects, you can also pass each transaction element in dictionary format

## Query transactions

To understand your balance changes (bank statement), you can query
transactions. Note that our system creates transactions for you when
you receive boleto payments, pay a bill or make transfers, for example.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Transaction> transactions = StarkBank.Transaction.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Transaction transaction in transactions) {
    Console.WriteLine(transaction);
}
```

## Get a transaction

You can get a specific transaction by its id:

```c#
using System;

StarkBank.Transaction transaction = StarkBank.Transaction.Get("5155165527080960");

Console.WriteLine(transaction);
```

## Get balance

To know how much money you have in your workspace, run:

```c#
using System;

StarkBank.Balance balance = StarkBank.Balance.Get();

Console.WriteLine(balance);
```

## Create transfers

You can also create transfers in the SDK (TED/Pix) and configure transfer behavior according to its rules.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.Transfer> transfers = StarkBank.Transfer.Create(
    new List<StarkBank.Transfer> {
        new StarkBank.Transfer(
            amount: 100,  // R$ 1,00
            bankCode: "260",  // TED
            branchCode: "0001",
            accountNumber: "10000-0",
            taxID: "012.345.678-90",
            name: "Tony Stark",
            tags: new List<string> { "iron", "suit" }
        ),
        new StarkBank.Transfer(
            amount: 200,  // R$ 2,00
            bankCode: "20018183",  // Pix
            branchCode: "1234",
            accountNumber: "123456-7",
            accountType: "salary",
            externalID: "my-internal-id-12345",
            taxID: "012.345.678-90",
            name: "Jon Snow",
            scheduled: DateTime.Now.AddDays(1),
            description: "Sword",
            rules: new List<BrcodePayment.Rule>() {
                new BrcodePayment.Rule(
                    key: "resendingLimit",              // Set maximum number of retries if Transfer fails due to systemic issues at the receiver bank
                    value: 5                            // Our resending limit is 10 by default
                )
            }
        )
    }
);

foreach(StarkBank.Transfer transfer in transfers) {
    Console.WriteLine(transfer);
}
```

**Note**: Instead of using Transfer objects, you can also pass each transfer element in dictionary format

## Query transfers

You can query multiple transfers according to filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Transfer> transfers = StarkBank.Transfer.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Transfer transfer in transfers) {
    Console.WriteLine(transfer.Name);
}
```

## Get a transfer

To get a single transfer by its id, run:

```c#
using System;

StarkBank.Transfer transfer = StarkBank.Transfer.Get("5155165527080960");

Console.WriteLine(transfer);
```

## Cancel a scheduled transfer

To cancel a single scheduled transfer by its id, run:

```c#
using System;

StarkBank.Transfer transfer = StarkBank.Transfer.Delete("5155165527080960");

Console.WriteLine(transfer);
```

## Get a transfer PDF

A transfer PDF may also be retrieved by passing its id.
This operation is only valid if the transfer status is "processing" or "success".

```c#
byte[] pdf = StarkBank.Transfer.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("transfer.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Query transfer logs

You can query transfer logs to better understand transfer life cycles.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Transfer.Log> logs = StarkBank.Transfer.Log.Query(limit: 50);

foreach(StarkBank.Transfer.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get a transfer log

You can also get a specific log by its id.

```c#
using System;

StarkBank.Transfer.Log log = StarkBank.Transfer.Log.Get("5155165527080960");

Console.WriteLine(log);
```

## Get dict key

You can get Pix key's parameters by its id.

```c#
using System;

StarkBank.DictKey dictKey = DictKey.Get("tony@starkbank.com");

Console.WriteLine(dictKey);
```

## Query your DICT keys

To take a look at the Pix keys linked to your workspace, just run the following:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.DictKey> dictKeys = StarkBank.DictKey.Query(
    status: "registered",
    type: "evp"
);

foreach(StarkBank.DictKey dictKey in dictKeys) {
    Console.WriteLine(dictKey);
}
```

## Query Bacen institutions

You can query institutions registered by the Brazilian Central Bank for Pix and TED transactions.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.Institution> institutions = StarkBank.Institution.Query(search: "stark");

foreach(StarkBank.Institution.Log institution in institutions) {
    Console.WriteLine(institution);
}
```

## Create invoices

You can create dynamic QR Code invoices to charge customers or to receive money from accounts you have in other banks. 

Since the banking system only understands value modifiers (discounts, fines and interest) when dealing with **dates** (instead of **datetimes**), these values will only show up in the end user banking interface if you use **dates** in the "due" and "discounts" fields. 

If you use **datetimes** instead, our system will apply the value modifiers in the same manner, but the end user will only see the final value to be paid on his interface.

Also, other banks will most likely only allow payment scheduling on invoices defined with **dates** instead of **datetimes**.

```c#
# coding: utf-8
using System;
using System.Collections.Generic;

List<StarkBank.Invoice> invoices = StarkBank.Invoice.Create(
    new List<StarkBank.Invoice> {
        new StarkBank.Invoice(
            amount: 248,
            descriptions: new List<Dictionary<string, object>>() {
                new Dictionary<string, object> {
                    {"key", "Arya"},
                    {"value", "Not today"}
                }
            },
            discounts: new List<Dictionary<string, object>>() {
                new Dictionary<string, object> {
                    {"percentage", 10},
                    {"due", new DateTime(2021, 4, 25, 13, 17, 15)}
                }
            },
            due: new DateTime(2021, 5, 25, 13, 17, 15),
            expiration: 123456789,
            fine: 2.5,
            interest: 1.3,
            name: "Arya Stark",
            taxID: "29.176.331/0001-69",
            tags: new List<string> { "immediate" }
        ),
        new StarkBank.Invoice(
            amount: 248,
            discounts: new List<Dictionary<string, object>>() {
                new Dictionary<string, object> {
                    {"percentage", 10},
                    {"due", new DateTime(2021, 4, 25)}
                }
            },
            due: new DateTime(2021, 5, 25),
            expiration: 123456789,
            fine: 2.5,
            interest: 1.3,
            name: "Arya Stark",
            taxID: "29.176.331/0001-69",
            tags: new List<string> { "scheduled" }
        )
    }
);

foreach(StarkBank.Invoice invoice in invoices) {
    Console.WriteLine(invoice);
}
```

**Note**: Instead of using Invoice objects, you can also pass each invoice element in dictionary format

## Get an invoice

After its creation, information on an invoice may be retrieved by its id.
Its status indicates whether it's been paid.

```c#
using System;

StarkBank.Invoice invoice = StarkBank.Invoice.Get("5715709195714560");

Console.WriteLine(invoice);
```

## Get an invoice QRCode

After its creation, an invoice QRCode may be retrieved by its id.

```c#
using System;

byte[] png = StarkBank.Invoice.Qrcode("5715709195714560");

System.IO.File.WriteAllBytes("qrcode.png", png);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Get an invoice PDF

After its creation, an invoice PDF may be retrieved by its id.

```c#
using System;

byte[] pdf = StarkBank.Invoice.Pdf("5715709195714560");

System.IO.File.WriteAllBytes("invoice.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Cancel an invoice

You can also cancel an invoice by its id.
Note that this is not possible if it has been paid already.

```c#
using System;

StarkBank.Invoice invoice = StarkBank.Invoice.Update(
    "6312789471657984",
    status: "canceled"
);

Console.WriteLine(invoice);
```

## Update an invoice

You can update an invoice's amount, due date and expiration by its id.
If the invoice has already been paid, the amount can still be decreased, causing the payment to be reversed to the payer.
To fully reverse the invoice, pass `amount: 0`.

```c#
using System;

StarkBank.Invoice invoice = StarkBank.Invoice.Update(
    "6312789471657984",
    amount: 0
);

Console.WriteLine(invoice);
```

## Query invoices

You can get a list of created invoices given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Invoice> invoices = StarkBank.Invoice.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkBank.Invoice invoice in invoices) {
    Console.WriteLine(invoice);
}
```

## Query invoice logs

Logs are pretty important to understand the life cycle of an invoice.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Invoice.Log> logs = StarkBank.Invoice.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkBank.Invoice.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get an invoice log

You can get a single log by its id.

```c#
using System;

StarkBank.Invoice.Log log = StarkBank.Invoice.Log.Get("4701727546671104");

Console.WriteLine(log);
```

## Get a reversed invoice log PDF

Whenever an Invoice is successfully reversed, a reversed log will be created.
To retrieve a specific reversal receipt, you can request the corresponding log PDF:

```c#
using System;

byte[] pdf = StarkBank.Invoice.Log.Pdf("4701727546671104");
System.IO.File.WriteAllBytes("invoice-log.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.


## Get an invoice payment information

Once an invoice has been paid, you can get the payment information using the InvoicePayment sub-resource:

```c#
using System;

StarkBank.InvoicePayment payment = Invoice.Payment("5155165527080960");

Console.WriteLine(payment);
```

## Create DynamicBrcodes

You can create simplified dynamic QR Codes to receive money using Pix transactions. 
When a DynamicBrcode is paid, a Deposit is created with the tags parameter containing the character “dynamic-brcode/” followed by the DynamicBrcode’s uuid "dynamic-brcode/{uuid}" for conciliation.

The differences between an Invoice and the DynamicBrcode are the following:

|                       | Invoice | DynamicBrcode |
|-----------------------|:-------:|:-------------:|
| Expiration            |    ✓    |       ✓       |
| Can only be paid once |    ✓    |       ✓       |
| Due, fine and fee     |    ✓    |       X       |
| Discount              |    ✓    |       X       |
| Description           |    ✓    |       X       |
| Can be updated        |    ✓    |       X       |

**Note:** In order to check if a BR code has expired, you must first calculate its expiration date (add the expiration to the creation date). 
**Note:** To know if the BR code has been paid, you need to query your Deposits by the tag "dynamic-brcode/{uuid}" to check if it has been paid.

```c#
using System;

List<DynamicBrcode> brcodes = StarkBank.DynamicBrcode.Create(
    new List<DynamicBrcode>() { 
        new DynamicBrcode(
            amount: 23571,  // R$ 235,71 
            expiration: 3600
        ),
        new DynamicBrcode(    
            amount: 23571,  // R$ 235,71 
            expiration: 3600
        )
    }
)

foreach(StarkBank.DynamicBrcode brcode in brcodes) {
    Console.WriteLine(brcode);
}
```

**Note**: Instead of using DynamicBrcode objects, you can also pass each brcode element in dictionary format

## Get a DynamicBrcode

After its creation, information on a DynamicBrcode may be retrieved by its uuid.

```c#
using System;

StarkBank.DynamicBrcode brcode = StarkBank.DynamicBrcode.Get("bb9cd43ea6f4403391bf7ef6aa876600");

Console.WriteLine(brcode);
```

## Query DynamicBrcodes

You can get a list of created DynamicBrcodes given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.DynamicBrcode> brcodes = StarkBank.DynamicBrcode.Query(
    after: new DateTime(2023, 4, 1),
    before: new DateTime(2023, 4, 30)
);

foreach(StarkBank.DynamicBrcode brcode in brcodes) {
    Console.WriteLine(brcode);
}
```

## Query deposits

You can get a list of created deposits given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Deposit> deposits = StarkBank.Deposit.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkBank.Deposit deposit in deposits) {
    Console.WriteLine(deposit);
}
```

## Get a deposit

After its creation, information on a deposit may be retrieved by its id. 

```c#
using System;

StarkBank.Deposit deposit = StarkBank.Deposit.Get("5715709195714560");

Console.WriteLine(deposit);
```

## Query deposit logs

Logs are pretty important to understand the life cycle of a deposit.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Deposit.Log> logs = StarkBank.Deposit.Log.Query(
    after: new DateTime(2019, 4, 1),
    before: new DateTime(2021, 4, 30)
);

foreach(StarkBank.Deposit.Log log in logs) {
    Console.WriteLine(log);
}
```

## Update a deposit

Update a deposit by passing its id to be partially or fully reversed.

```c#
using System;

StarkBank.Deposit deposit = StarkBank.Deposit.Update(
    "5155165527080960",
    amount: 0
);

Console.WriteLine(deposit);
```

## Get a deposit log

You can get a single log by its id.

```c#
using System;

StarkBank.Deposit.Log log = StarkBank.Deposit.Log.Get("4701727546671104");

Console.WriteLine(log);
```

## Create boletos

You can create boletos to charge customers or to receive money from accounts
you have in other banks.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.Boleto> boletos = StarkBank.Boleto.Create(
    new List<StarkBank.Boleto> {
        new StarkBank.Boleto(
            amount: 23571,  // R$ 235,71
            name: "Buzz Aldrin",
            taxID: "012.345.678-90",
            streetLine1: "Av. Paulista, 200",
            streetLine2: "10 andar",
            district: "Bela Vista",
            city: "São Paulo",
            stateCode: "SP",
            zipCode: "01310-000",
            due: DateTime.Today.Date.AddDays(30),
            fine: 5,  // 5%
            interest: 2.5  // 2.5% per month
        )
    }
);

foreach (StarkBank.Boleto boleto in boletos) {
    Console.WriteLine(boleto);
}
```

**Note**: Instead of using Boleto objects, you can also pass each boleto element in dictionary format

## Get a boleto

After its creation, information on a boleto may be retrieved by passing its id.
Its status indicates whether it's been paid.

```c#
using System;

StarkBank.Boleto boleto = StarkBank.Boleto.Get("5155165527080960");

Console.WriteLine(boleto);
```

## Get a boleto PDF

After its creation, a boleto PDF may be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.Boleto.Pdf("5155165527080960", layout: "default");

System.IO.File.WriteAllBytes("boleto.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Delete a boleto

You can also cancel a boleto by its id.
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.Boleto boleto = StarkBank.Boleto.Delete("5155165527080960");

Console.WriteLine(boleto);
```

## Query boletos

You can get a list of created boletos given some filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Boleto> boletos = StarkBank.Boleto.Query(
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Boleto boleto in boletos) {
    Console.WriteLine(boleto);
}
```

## Query boleto logs

Logs are pretty important to understand the life cycle of a boleto.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Boleto.Log> logs = StarkBank.Boleto.Log.Query(limit: 150);

foreach(StarkBank.Boleto.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get a boleto log

You can get a single log by its id.

```c#
using System;

StarkBank.Boleto.Log log = StarkBank.Boleto.Log.Get("5155165527080960");

Console.WriteLine(log);
```

## Investigate a boleto

You can discover if a StarkBank boleto has been recently paid before we receive the response on the next day.
This can be done by creating a BoletoHolmes object, which fetches the updated status of the corresponding
Boleto object according to CIP to check, for example, whether it is still payable or not. The investigation
happens asynchronously and the most common way to retrieve the results is to register a "boleto-holmes" webhook
subscription, although polling is also possible.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.BoletoHolmes> payments = StarkBank.BoletoHolmes.Create(
    new List<StarkBank.BoletoHolmes> {
        new StarkBank.BoletoHolmes(
            boletoID: "5656565656565656",
        ),
        new StarkBank.BoletoHolmes(
            boletoID: "4848484848484848",
            tags: new List<string> { "elementary" }
        )
    }
);

foreach(StarkBank.BoletoHolmes sherlock in holmes) {
    Console.WriteLine(sherlock);
}
```

**Note**: Instead of using BoletoHolmes objects, you can also pass each payment element in dictionary format

## Get a boleto holmes

To get a single Holmes by its id, run:

```c#
using System;

StarkBank.BoletoHolmes sherlock = StarkBank.BoletoHolmes.Get("5155165527080960");

Console.WriteLine(sherlock);
```

## Query boleto holmes

You can search for boleto Holmes using filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BoletoHolmes> holmes = StarkBank.BoletoHolmes.Query(
    tags: new List<string> { "elementary" }
);

foreach(StarkBank.BoletoHolmes sherlock in holmes) {
    Console.WriteLine(sherlock);
}
```

## Query boleto holmes logs

Searches are also possible with boleto holmes logs:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BoletoHolmes.Log> logs = StarkBank.BoletoHolmes.Log.Query(
    holmesIds: new List<string> { "5155165527080960", "76551659167801921" }
);

foreach(StarkBank.BoletoHolmes.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get a boleto holmes log

You can also get a boleto holmes log by specifying its id.

```c#
using System;

StarkBank.BoletoHolmes.Log log = StarkBank.BoletoHolmes.Log.Get("5155165527080960");

Console.WriteLine(log);
```

## Pay a BR Code

Paying a BR Code is also simple. After extracting the BR Code encoded in the Pix QR Code, you can do the following:

```c#
using System;
using System.Collections.Generic;

List<StarkBank.BrcodePayment> payments = StarkBank.BrcodePayment.Create(
    new List<StarkBank.BrcodePayment> {
        new StarkBank.BrcodePayment(
            brcode: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A",
            taxID: "012.345.678-90",
            scheduled: DateTime.Today.Date.AddDays(2),
            description: "this will be fast",
            tags: new List<string> { "pix", "qrcode" },
            rules: new List<BrcodePayment.Rule>() {
                new BrcodePayment.Rule(
                    key: "resendingLimit",                  // Set maximum number of retries if Payment fails due to systemic issues at the receiver bank
                    value: 5                                // Our resending limit is 10 by default
                )
            }
        )
    }
);

foreach(StarkBank.BrcodePayment payment in payments) {
    Console.WriteLine(payment);
}
```

**Note**: You can also configure payment behavior according to its rules
**Note**: Instead of using BrcodePayment objects, you can also pass each payment element in dictionary format

## Get a BR Code payment

To get a single BR Code payment by its id, run:

```c#
using System;

StarkBank.BrcodePayment payment = StarkBank.BrcodePayment.Get("19278361897236187236");

Console.WriteLine(payment);
```

## Get a BR Code payment PDF

After its creation, a BR Code payment PDF may be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.BrcodePayment.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("brcode_payment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Cancel a BR Code payment

You can cancel a BR Code payment by changing its status to "canceled".
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.BrcodePayment payment = StarkBank.BrcodePayment.Update(
    "6312789471657984",
    status: "canceled"
);

Console.WriteLine(payment);
```


## Query BR Code payments

You can search for BR Code payments using filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BrcodePayment> payments = StarkBank.BrcodePayment.Query(
    tags: new List<string> { "company_1", "company_2" }
);

foreach(StarkBank.BrcodePayment payment in payments) {
    Console.WriteLine(payment);
}
```

## Query BR Code payment logs

Searches are also possible with BR Code payment logs:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BrcodePayment.Log> logs = StarkBank.BrcodePayment.Log.Query(
    paymentIds: new List<string> { "5155165527080960", "76551659167801921" }
);

foreach(StarkBank.BrcodePayment.Log log in logs) {
    Console.WriteLine(log);
}
```


## Get a BR Code payment log

You can also get a BR Code payment log by specifying its id.

```c#
using System;

StarkBank.BrcodePayment.Log log = StarkBank.BrcodePayment.Log.Get("5155165527080960");

Console.WriteLine(log);
```


## Pay a boleto

Paying a boleto is also simple.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.BoletoPayment> payments = StarkBank.BoletoPayment.Create(
    new List<StarkBank.BoletoPayment> {
        new StarkBank.BoletoPayment(
            line: "34191.09008 64694.017308 71444.640008 1 96610000014500",
            taxID: "012.345.678-90",
            scheduled: DateTime.Today.Date.AddDays(2),
            description: "take my money",
            tags: new List<string> { "take", "my", "money" }
        ),
        new StarkBank.BoletoPayment(
            barCode: "34191972300000289001090064694197307144464000",
            taxID: "012.345.678-90",
            scheduled: DateTime.Today.Date.AddDays(1),
            description: "take my money one more time",
            tags: new List<string> { "again" }
        )
    }
);

foreach(StarkBank.BoletoPayment payment in payments) {
    Console.WriteLine(payment);
}
```

**Note**: Instead of using BoletoPayment objects, you can also pass each payment element in dictionary format

## Get a boleto payment

To get a single boleto payment by its id, run:

```c#
using System;

StarkBank.BoletoPayment payment = StarkBank.BoletoPayment.Get("19278361897236187236");

Console.WriteLine(payment);
```

## Get a boleto payment PDF

After its creation, a boleto payment PDF may be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.BoletoPayment.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("boleto_payment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Delete a boleto payment

You can also cancel a boleto payment by its id.
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.BoletoPayment payment = StarkBank.BoletoPayment.Delete("5155165527080960");

Console.WriteLine(payment);
```

## Query boleto payments

You can search for boleto payments using filters.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BoletoPayment> payments = StarkBank.BoletoPayment.Query(
    tags: new List<string> { "company_1", "company_2" }
);

foreach(StarkBank.BoletoPayment payment in payments) {
    Console.WriteLine(payment);
}
```

## Query boleto payment logs

Searches are also possible with boleto payment logs:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.BoletoPayment.Log> logs = StarkBank.BoletoPayment.Log.Query(
    paymentIds: new List<string> { "5155165527080960", "76551659167801921" }
);

foreach(StarkBank.BoletoPayment.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get a boleto payment log

You can also get a boleto payment log by specifying its id.

```c#
using System;

StarkBank.BoletoPayment.Log log = StarkBank.BoletoPayment.Log.Get("5155165527080960");

Console.WriteLine(log);
```

## Create utility payments

It"s also simple to pay utility bills (such as electricity and water bills) in the SDK.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.UtilityPayment> payments = StarkBank.UtilityPayment.Create(
    new List<StarkBank.UtilityPayment> {
        new StarkBank.UtilityPayment(
            line: "83680000001 7 08660138003 0 71070987611 8 00041351685 7",
            scheduled: DateTime.Today.Date.AddDays(2),
            description: "take my money",
            tags: new List<string> { "take", "my", "money" }
        ),
        new StarkBank.UtilityPayment(
            barCode: "83600000001512801380037107172881100021296561",
            scheduled: DateTime.Today.Date.AddDays(1),
            description: "take my money one more time",
            tags: new List<string> { "again" }
        )
    }
);

foreach(StarkBank.UtilityPayment payment in payments) {
    Console.WriteLine(payment);
}
```

**Note**: Instead of using UtilityPayment objects, you can also pass each payment element in dictionary format

## Query utility payments

To search for utility payments using filters, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.UtilityPayment> payments = StarkBank.UtilityPayment.Query(
    tags: new List<string> { "electricity", "gas" }
);

foreach(StarkBank.UtilityPayment payment in payments) {
    Console.WriteLine(payment);
}
```

## Get a utility payment

You can get a specific bill by its id:

```c#
using System;

StarkBank.UtilityPayment payment = StarkBank.UtilityPayment.Get("5155165527080960");

Console.WriteLine(payment);
```

## Get a utility payment PDF

After its creation, a utility payment PDF may also be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.UtilityPayment.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("electricity_payment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Delete a utility payment

You can also cancel a utility payment by its id.
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.UtilityPayment payment = StarkBank.UtilityPayment.Delete("5155165527080960");

Console.WriteLine(payment);
```

## Query utility payment logs

You can search for payments by specifying filters. Use this to understand the
bills life cycles.

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.UtilityPayment.Log> logs = StarkBank.UtilityPayment.Log.Query(
    paymentIds: new List<string> { "102893710982379182", "92837912873981273" }
);

foreach(StarkBank.UtilityPayment.Log log in logs) {
    Console.WriteLine(log);
}
```

## Get a utility payment log

If you want to get a specific payment log by its id, just run:

```c#
using System;

StarkBank.UtilityPayment.Log log = StarkBank.UtilityPayment.Log.Get("1902837198237992");

Console.WriteLine(log);
```

## Create tax payments

It is also simple to pay taxes (such as ISS and DAS) using this SDK.

```c#
using System;

List<StarkBank.TaxPayment> payments = StarkBank.TaxPayment.Create(new List<StarkBank.TaxPayment>() { 
    new StarkBank.TaxPayment(
        barCode: "85660000001549403280074119002551100010601813",
        description: "fix the road",
        tags: new List<string> { "take", "my", "money" },
        scheduled: "2020-08-13"
    ),
    new StarkBank.TaxPayment(
        line: "85800000003 0 28960328203 1 56072020190 5 22109674804 0",
        description: "build the hospital, hopefully",
        tags: new List<string> { "expensive" },
        scheduled: "2020-08-13"
    )
 });

foreach (StarkBank.TaxPayment payment in payments)
{
    Console.WriteLine(payment);
}
```

 **Note**: Instead of using TaxPayment objects, you can also pass each payment element in dictionary format

## Query tax payments

To search for tax payments using filters, run:

```c#
using System;

List<StarkBank.TaxPayment> payments = StarkBank.TaxPayment.Query(limit: 5).ToList();

foreach (StarkBank.TaxPayment payment in payments)
{
    Console.WriteLine(payment);
}
```

## Get tax payment

You can get a specific tax payment by its id:

```c#
using System;

StarkBank.TaxPayment payment = StarkBank.TaxPayment.Get("5155165527080960");

Console.WriteLine(payment);
```

## Get tax payment PDF

After its creation, a tax payment PDF may also be retrieved by its id.

```c#
using System;

byte[] pdf = StarkBank.TaxPayment.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("taxPayment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Delete tax payment

You can also cancel a tax payment by its id.
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.TaxPayment payment = StarkBank.TaxPayment.Delete("5155165527080960");

Console.WriteLine(payment);
```

## Query tax payment logs

You can search for payment logs by specifying filters. Use this to understand each payment life cycle.

```c#
using System;

List<StarkBank.TaxPayment.Log> logs = StarkBank.TaxPayment.Log.Query(
    limit: 5
).ToList();

foreach (StarkBank.TaxPayment.Log log in logs)
{
    Console.WriteLine(log);
}
```

## Get tax payment log

If you want to get a specific payment log by its id, just run:

```c#
using System;

StarkBank.TaxPayment.Log log = StarkBank.TaxPayment.Log.Get("1902837198237992");

Console.WriteLine(log);
```

**Note**: Some taxes can't be payed with bar codes. Since they have specific parameters, each one of them has its own
resource and routes, which are all analogous to the TaxPayment resource. The ones we currently support are:
- DarfPayment, for DARFs

## Create DARF payment

If you want to manually pay DARFs without barCodes, you may create DarfPayments:

```c#
using System;

List<StarkBank.DarfPayment> payments = StarkBank.DarfPayment.Create(
    new List<StarkBank.DarfPayment>() { 
        new StarkBank.DarfPayment(
            revenueCode: "1240",
            taxId: "012.345.678-90",
            competence: "2020-09-01",
            referenceNumber: "2340978970",
            nominalAmount: 1234,
            fineAmount: 12,
            interestAmount: 34,
            due: DateTime.Today.Date.AddDays(30),
            scheduled: DateTime.Today.Date.AddDays(30),
            tags: new List<string> { "DARF", "making money" },
            description: "take my money",
        )
    }
);

foreach (StarkBank.DarfPayment payment in payments)
{
    Console.WriteLine(payment);
}
```

**Note**: Instead of using DarfPayment objects, you can also pass each payment element in dictionary format

## Query DARF payments

To search for DARF payments using filters, run:

```c#
using System;

List<StarkBank.DarfPayment> payments = StarkBank.DarfPayment.Query(
    tags: new List<string>{ "darf", "july" }
).ToList();

foreach (StarkBank.DarfPayment payment in payments)
{
    Console.WriteLine(payment);
}
```

## Get DARF payment

You can get a specific DARF payment by its id:

```c#
using System;

StarkBank.DarfPayment payment = StarkBank.DarfPayment.Get("5155165527080960");

Console.WriteLine(payment);
```

## Get DARF payment PDF

After its creation, a DARF payment PDF may also be retrieved by its id. 

```c#
using System;

byte[] pdf = StarkBank.DarfPayment.Pdf("5155165527080960");

System.IO.File.WriteAllBytes("darfPayment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

## Delete DARF payment

You can also cancel a DARF payment by its id.
Note that this is not possible if it has been processed already.

```c#
using System;

StarkBank.DarfPayment payment = StarkBank.DarfPayment.Delete("5155165527080960");

Console.WriteLine(payment);
```

## Query DARF payment logs

You can search for payment logs by specifying filters. Use this to understand each payment life cycle.

```c#
using System;

List<StarkBank.DarfPayment.Log> logs = StarkBank.DarfPayment.Log.Query(
    limit: 5
).ToList();

foreach (StarkBank.DarfPayment.Log log in logs)
{
    Console.WriteLine(log);
}
```

## Get DARF payment log

If you want to get a specific payment log by its id, just run:

```c#
using System;

StarkBank.DarfPayment.Log log = StarkBank.DarfPayment.Log.Get("1902837198237992");

Console.WriteLine(log);
```

## Preview payment information before executing the payment

You can preview multiple types of payment to confirm any information before actually paying.
If the "scheduled" parameter is not informed, today will be assumed as the intended payment date.
Right now, the "scheduled" parameter only has effect on BrcodePreviews.
This resource is able to preview the following types of payment:
"brcode-payment", "boleto-payment", "utility-payment" and "tax-payment"

```c#
using System;

List<PaymentPreview> previews = PaymentPreview.Create(new List<PaymentPreview>
{
    new PaymentPreview(id: "00020126580014br.gov.bcb.pix0136a629532e-7693-4846-852d-1bbff817b5a8520400005303986540510.005802BR5908T'Challa6009Sao Paulo62090505123456304B14A", scheduled: DateTime.Today.Date.AddDays(3)),
    new PaymentPreview(id: "34191.09008 61207.727308 71444.640008 5 81310001234321")
});

foreach (StarkBank.PaymentPreview preview in previews)
{
    Console.WriteLine(preview);
}
```

**Note**: Instead of using PaymentPreview objects, you can also pass each request element in dictionary format


## Create payment requests to be approved by authorized people in a cost center

You can also request payments that must pass through a specific cost center approval flow to be executed.
In certain structures, this allows double checks for cash-outs and also gives time to load your account
with the required amount before the payments take place.
The approvals can be granted at our web banking and must be performed according to the rules
specified in the cost center.

**Note**: The value of the centerID parameter can be consulted by logging into our web banking and going
to the desired Cost Center page.

```c#
using System;
using System.Collections.Generic;

List<StarkBank.PaymentRequest> requests = StarkBank.PaymentRequest.Create(
    new List<StarkBank.PaymentRequest> {
        new StarkBank.PaymentRequest(
            centerID: "5656565656565656",
            due: DateTime.Today.Date.AddDays(2),
            payment: new StarkBank.Transfer(
                amount: 100,  // R$ 1,00
                bankCode: "033",
                branchCode: "0001",
                accountNumber: "10000-0",
                taxID: "012.345.678-90",
                name: "Master Yoda"
            )
        ),
        new StarkBank.PaymentRequest(
            centerID: "5656565656565656",
            payment: new StarkBank.BoletoPayment(
                line: "34191.09008 64694.017308 71444.640008 1 96610000014500",
                taxID: "012.345.678-90",
                description: "Payment for spare droid parts"
            ),
            tags: new List<string> { "rd2d", "invoice/1234" }
        )
    }
);

foreach(StarkBank.PaymentRequest request in requests) {
    Console.WriteLine(request);
}
```

**Note**: Instead of using PaymentRequest objects, you can also pass each payment element in dictionary format

## Query payment requests

To search for payment requests, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.PaymentRequest> requests = StarkBank.PaymentRequest.Query(
    centerID: "5656565656565656",
    after: DateTime.Today.Date.AddDays(-10),
    before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.PaymentRequest request in requests) {
    Console.WriteLine(request);
}
```

## Create CorporateHolders

You can create card holders to which your cards will be bound.
They support spending rules that will apply to all underlying cards.

```c#
using System;

List<StarkBank> holders = StarkBank.CorporateHolder.Create(
    new List<StarkBank.CorporateHolder> {
        new StarkBank.CorporateHolder(
            name: "Iron Bank S.A.",
            tags: new List<string> { "Traveler Employee" },
            rules: new List<StarkBank.CorporateRule>() {
                new StarkBank.CorporateRule(
                    name: "General USD",
                    interval: "day",
                    amount: 100000,
                    currencyCode: "USD"
                )
            }
        )
    }
)

foreach (StarkBank.CorporateHolder holder in holders) {
    Console.Write(holder);   
}
```

**Note**: Instead of using CorporateHolder objects, you can also pass each element in dictionary format

## Query CorporateHolders

You can query multiple holders according to filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateHolder> holders = StarkBank.CorporateHolder.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach(StarkBank.CorporateHolder holder in holders)
{
    Console.Write(holder);
}
```

## Cancel a CorporateHolder

To cancel a single Corporate Holder by its id, run:

```c#
using System;
using StarkBank;


StarkBank.CorporateHolder holder = StarkBank.CorporateHolder.Cancel("5353197895942144");

Console.Write(holder);
```

## Get a CorporateHolder

To get a single Corporate Holder by its id, run:

```c#
using System;
using StarkBank;


StarkBank.CorporateHolder holder = StarkBank.CorporateHolder.Get("5353197895942144");

Console.Write(holder);
```

## Query CorporateHolder logs

You can query holder logs to better understand holder life cycles.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateHolder.Log> logs = StarkBank.CorporateHolder.Log.Query(limit: 10);

foreach (StarkBank.CorporateHolder.Log log in logs)
{
    Console.Write(log);
}
```

## Get a CorporateHolder log

You can also get a specific log by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporateHolder.Log log = StarkBank.CorporateHolder.Log.Get("6299741604282368");

Console.Write(log);
```

## Create CorporateCard

You can issue cards with specific spending rules.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


StarkBank.CorporateCard card = StarkBank.CorporateCard.Create(
    new StarkBank.CorporateCard(
        holderID: "5155165527080960"
    )
);

Console.Write(card);
```

## Query CorporateCards

You can get a list of created cards given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateCard> cards = StarkBank.CorporateCard.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2023, 3, 1)
);

foreach(StarkBank.CorporateCard card in cards)
{
    Console.Write(card);
}
```

## Get a CorporateCard

After its creation, information on a card may be retrieved by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporateCard card = StarkBank.CorporateCard.Get("5353197895942144");

Console.Write(card);
```

## Update a CorporateCard

You can update a specific card by its id.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


Dictionary<string, object> patchData = new Dictionary<string, object> {
    { "status", "blocked" }
};

StarkBank.CorporateCard card = StarkBank.CorporateCard.Update("5353197895942144", patchData);

Console.Write(card);
```

## Cancel a CorporateCard

You can also cancel a card by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporateCard card = StarkBank.CorporateCard.Cancel("5353197895942144");

Console.Write(card);
```

## Query CorporateCard logs

Logs are pretty important to understand the life cycle of a card.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateCard.Log> logs = StarkBank.CorporateCard.Log.Query(limit: 10);

foreach (StarkBank.CorporateCard.Log log in logs)
{
    Console.Write(log);
}
```

## Get a CorporateCard log

You can get a single log by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporateCard.Log log = StarkBank.CorporateCard.Log.Get("6299741604282368");

Console.Write(log);
```

## Query CorporatePurchases

You can get a list of created purchases given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporatePurchase> purchases = StarkBank.CorporatePurchase.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkBank.CorporatePurchase purchase in purchases)
{
    Console.Write(purchase);
}
```

## Get a CorporatePurchase

After its creation, information on a purchase may be retrieved by its id. 

```c#
using System;
using StarkBank;

StarkBank.CorporatePurchase purchase = StarkBank.CorporatePurchase.Get("5642359077339136");

Console.Write(purchase);
```

## Query CorporatePurchase logs

Logs are pretty important to understand the life cycle of a purchase.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporatePurchase.Log> logs = StarkBank.CorporatePurchase.Log.Query(limit: 10);

foreach (StarkBank.CorporatePurchase.Log log in logs)
{
    Console.Write(log);
}
```

## Get a CorporatePurchase log

You can get a single log by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporatePurchase.Log log = StarkBank.CorporatePurchase.Log.Get("6428086769811456");

Console.Write(log);
```

## Create CorporateInvoices

You can create Pix invoices to transfer money from accounts you have in any bank to your Corporate balance,
allowing you to run your corporate operation.

```c#
using System;
using StarkBank;

StarkBank.CorporateInvoice invoice = StarkBank.CorporateInvoice.Create(
    new StarkBank.CorporateInvoice(
        amount: 10000
    )
);
    
Console.Write(invoice);
```

**Note**: Instead of using CorporateInvoice objects, you can also pass each element in dictionary format

## Query CorporateInvoices

You can get a list of created invoices given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateInvoice> invoices = StarkBank.CorporateInvoice.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkBank.CorporateInvoice invoice in invoices)
{
    Console.Write(invoice);
}
```

## Create CorporateWithdrawals

You can create withdrawals to send cash back from your Corporate balance to your Banking balance
by using the Withdrawal resource.

```c#
using System;
using StarkBank;


StarkBank.CorporateWithdrawal withdrawal = StarkBank.CorporateWithdrawal.Create(
    new StarkBank.CorporateWithdrawal(
        amount: 10000,
        externalID: "3257"
    )
);

Console.Write(withdrawal);
```

**Note**: Instead of using CorporateWithdrawal objects, you can also pass each element in dictionary format

## Get a CorporateWithdrawal

After its creation, information on a withdrawal may be retrieved by its id.

```c#
using System;
using StarkBank;


StarkBank.CorporateWithdrawal withdrawal = StarkBank.CorporateWithdrawal.Get("5440727945314304");

Console.Write(withdrawal);
```

## Query CorporateWithdrawals

You can get a list of created withdrawals given some filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateWithdrawal> withdrawals = StarkBank.CorporateWithdrawal.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkBank.CorporateWithdrawal withdrawal in withdrawals)
{
    Console.Write(withdrawal);
}
```

## Get your CorporateBalance

To know how much money you have available to run authorizations, run:

```c#
using System;
using StarkBank;


StarkBank.CorporateBalance balance = StarkBank.CorporateBalance.Get();

Console.Write(balance);
```

## Query CorporateTransactions

To understand your balance changes (corporate statement), you can query
transactions. Note that our system creates transactions for you when
you make purchases, withdrawals, receive corporate invoice payments, for example.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CorporateTransaction> transactions = StarkBank.CorporateTransaction.Query(
    after: new DateTime(2019, 1, 1),
    before: new DateTime(2022, 3, 1)
);

foreach (StarkBank.CorporateTransaction transaction in transactions)
{
    Console.Write(transaction);
}
```

## Get a CorporateTransaction

You can get a specific transaction by its id:

```c#
using System;
using StarkBank;


StarkBank.CorporateTransaction transaction = StarkBank.CorporateTransaction.Get("6539944898068480");

Console.Write(transaction);
```

## Corporate Enums

### Query MerchantCategories

You can query any merchant categories using this resource.
You may also use MerchantCategories to define specific category filters in CorporateRules.
Either codes (which represents specific MCCs) or types (code groups) will be accepted as filters.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.MerchantCategory> categories = StarkBank.MerchantCategory.Query(
    search: "food"
);

foreach (StarkBank.MerchantCategory category in categories)
{
    Console.Write(category);
}
```

### Query MerchantCountries

You can query any merchant countries using this resource.
You may also use MerchantCountries to define specific country filters in CorporateRules.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.MerchantCountry> countries = StarkBank.MerchantCountry.Query(
    search: "brazil"
);

foreach (StarkBank.MerchantCountry country in countries)
{
    Console.Write(country);
}
```

### Query CardMethods

You can query available card methods using this resource.
You may also use CardMethods to define specific purchase method filters in CorporateRules.

```c#
using System;
using System.Collections.Generic;
using StarkBank;


IEnumerable<StarkBank.CardMethod> methods = StarkBank.CardMethod.Query(
    search: "token"
);

foreach (StarkBank.CardMethod method in methods)
{
    Console.Write(method);
}
```

## Merchant Session

The Merchant Session allows you to create a session prior to a purchase.
Sessions are essential for defining the parameters of a purchase, including funding type, expiration, 3DS, and more.

## Create a MerchantSession

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantSession.AllowedInstallment installement = new MerchantSession.AllowedInstallment(
    totalAmount: 1000,
    count: 1
);

MerchantSession example = new MerchantSession(
    allowedFundingTypes: new List<string> { "credit" },
    allowedInstallments: new List<MerchantSession.AllowedInstallment> { installement },
    challengeMode: "disabled",
    expiration: 3600,
    tags: new List<string> { "yourTags" }
);

MerchantSession session = MerchantSession.Create(example);

Console.WriteLine(session);
```

You can create a MerchantPurchase through a MerchantSession by passing its UUID.
**Note**: This method must be implemented in your front-end to ensure that sensitive card data does not pass through the back-end of the integration.

### Create a MerchantSession Purchase

This route can be used to create a Merchant Purchase directly from the payer's client application. The UUID of a Merchant Session that was previously created by the merchant is necessary to access this route.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantSession.Purchase purchaseExample = new MerchantSession.Purchase(
    amount: 1000,
    installmentCount: 1,
    cardExpiration: "2035-01",
    cardNumber: "5102589999999954",
    cardSecurityCode: "123",
    holderName: "Holder Name",
    holderEmail: "holdeName@email.com",
    holderPhone: "11111111111",
    fundingType: "credit",
    billingCountryCode: "BRA",
    billingCity: "São Paulo",
    billingStateCode: "SP",
    billingStreetLine1: "Rua do Holder Name, 123",
    billingStreetLine2: "",
    billingZipCode: "11111-111",
    metadata: new Dictionary<string, object> {
        { "userAgent", "Postman" },
        { "userIp", "255.255.255.255" },
        { "language", "pt-BR" },
        { "timezoneOffset", 3 },
        { "extraData", "extraData" }
    }
);

MerchantSession.Purchase purchase = MerchantSession.PostPurchase(id: "c32f9d2385974957a777f8351921afd7", purchaseExample);

Console.WriteLine(purchase);
```

### Query MerchantSessions

Get a list of merchant sessions in chunks of at most 100. If you need smaller chunks, use the limit parameter.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

List<MerchantSession> sessions = MerchantSession.Query(limit: 2).ToList();
foreach (MerchantSession session in sessions)
{
    Console.WriteLine(session);
}
```

### Get a MerchantSession

Retrieve detailed information about a specific session by its id.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantSession session = MerchantSession.Get("5911980657344512");
Console.WriteLine(session);
```
## Merchant Purchase

The Merchant Purchase section allows users to retrieve detailed information of the purchases.

## Create a MerchantPurchase

The Merchant Purchase resource can be used to charge customers with credit or debit cards. If a card hasn't been used before, a Merchant Session Purchase must be created and approved with that specific card before it can be used directly in a Merchant Purchase.

```c#
using System;
using System.Collections.Generic;
using StarkBank.MerchantPurchase;

MerchantPurchase purchase = new MerchantPurchase(
    amount: 1000,
    cardId: "6295415968235520",
    challengeMode: "disabled",
    fundingType: "credit"
);

MerchantPurchase createdPurchase = MerchantPurchase.Create(purchase);
```

### Query MerchantPurchases

Get a list of merchant purchases in chunks of at most 100. If you need smaller chunks, use the limit parameter.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

List<MerchantPurchase> purchases = MerchantPurchase.Query(limit: 2).ToList();
foreach (MerchantPurchase purchase in purchases)
{
    Console.WriteLine(purchase);
}
```

### Get a MerchantPurchase

Retrieve detailed information about a specific purchase by its id.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantPurchase purchase = MerchantPurchase.Get("5911980657344512");
Console.WriteLine(purchase);
```

## Merchant Card

The Merchant Card resource stores information about cards used in approved purchases.
These cards can be used in new purchases without the need to create a new session.

### Query MerchantCards

Get a list of merchant cards in chunks of at most 100. If you need smaller chunks, use the limit parameter.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

List<MerchantCard> cards = MerchantCard.Query(limit: 2).ToList();
foreach (MerchantCard card in cards)
{
    Console.WriteLine(card);
}
```

### Get a MerchantCard

Retrieve detailed information about a specific card by its id.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantCard card = MerchantCard.Get("5911980657344512");
Console.WriteLine(card);
```

## Merchant Installment

Merchant Installments are created for every installment in a purchase.
These resources will track its own due payment date and settlement lifecycle.

### Query MerchantInstallments

Get a list of merchant installments in chunks of at most 100. If you need smaller chunks, use the limit parameter.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

List<MerchantInstallment> installments = MerchantInstallment.Query(limit: 2).ToList();
foreach (MerchantInstallment installment in installments)
{
    Console.WriteLine(installment);
}
```

### Get a MerchantInstallment

Retrieve detailed information about a specific installment by its id.

```c#
using System;
using System.Collections.Generic;
using StarkBank;

MerchantInstallment installment = MerchantInstallment.Get("5911980657344512");
Console.WriteLine(installment);
```

## Create a webhook subscription

To create a webhook subscription and be notified whenever an event occurs, run:

```c#
using System;

StarkBank.Webhook webhook = StarkBank.Webhook.Create(
    url: "https://webhook.site/dd784f26-1d6a-4ca6-81cb-fda0267761ec",
    subscriptions: new List<string> { "transfer", "invoice", "deposit", "brcode-payment", "boleto", "boleto-payment", "utility-payment", "tax-payment" }
);

Console.WriteLine(webhook);
```

## Query webhooks

To search for registered webhooks, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Webhook> webhooks = StarkBank.Webhook.Query();

foreach(StarkBank.Webhook webhook in webhooks) {
    Console.WriteLine(webhook);
}
```

## Get a webhook

You can get a specific webhook by its id.

```c#
using System;

StarkBank.Webhook webhook = StarkBank.Webhook.Get("10827361982368179");

Console.WriteLine(webhook);
```

## Delete a webhook

You can also delete a specific webhook by its id.

```c#
using System;

StarkBank.Webhook webhook = StarkBank.Webhook.Delete("10827361982368179");

Console.WriteLine(webhook);
```

## Process webhook events

It"s easy to process events that arrived in your webhook. Remember to pass the
signature header so the SDK can make sure it's really StarkBank that sent you
the event.

```c#
using System;

Response response = listen();  // this is the method you made to get the events posted to your webhook endpoint

StarkBank.Event parsedEvent = StarkBank.Event.Parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
);

if (parsedEvent.Subscription == "transfer") {
    StarkBank.Transfer.Log log = parsedEvent.Log as StarkBank.Transfer.Log;
    Console.WriteLine(log.Transfer);
}
else if (parsedEvent.Subscription == "invoice") {
    StarkBank.Invoice.Log log = parsedEvent.Log as StarkBank.Invoice.Log;
    Console.WriteLine(log.Invoice);
}
else if (parsedEvent.Subscription == "deposit") {
    StarkBank.Deposit.Log log = parsedEvent.Log as StarkBank.Deposit.Log;
    Console.WriteLine(log.Deposit);
}
else if (parsedEvent.Subscription == "brcode-payment") {
    StarkBank.BrcodePayment.Log log = parsedEvent.Log as StarkBank.BrcodePayment.Log;
    Console.WriteLine(log.Payment);
}
else if (parsedEvent.Subscription == "boleto-payment") {
    StarkBank.BoletoPayment.Log log = parsedEvent.Log as StarkBank.BoletoPayment.Log;
    Console.WriteLine(log.Payment);
}
else if (parsedEvent.Subscription == "utility-payment") {
    StarkBank.UtilityPayment.Log log = parsedEvent.Log as StarkBank.UtilityPayment.Log;
    Console.WriteLine(log.Payment);
}
```

## Query webhook events

To search for webhook events, run:

```c#
using System;
using System.Collections.Generic;

IEnumerable<StarkBank.Event> events = StarkBank.Event.Query(
    after: DateTime.Today,
    isDelivered: false
);

foreach(StarkBank.Event eventObject in events) {
    Console.WriteLine(eventObject);
}
```

## Get a webhook event

You can get a specific webhook event by its id.

```c#
using System;

StarkBank.Event eventObject = StarkBank.Event.Get("10827361982368179");

Console.WriteLine(eventObject);
```

## Delete a webhook event

You can also delete a specific webhook event by its id.

```c#
using System;

StarkBank.Event eventObject = StarkBank.Event.Delete("10827361982368179");

Console.WriteLine(eventObject);
```

## Set webhook events as delivered

This can be used in case you"ve lost events.
With this function, you can manually set events retrieved from the API as
"delivered" to help future event queries with `isDelivered: false`.

```c#
using System;

StarkBank.Event eventObject = StarkBank.Event.Update("129837198237192", isDelivered: true);

Console.WriteLine(eventObject);
```

## Query failed webhook event delivery attempts information

You can also get information on failed webhook event delivery attempts.

```c#
using System;

List<StarkBank.Event.Attempt> attempts = StarkBank.Event.Attempt.Query(after: "2020-03-20").ToList();

foreach(StarkBank.Event.Attempt attempt in attempts) {
    Console.WriteLine(attempt);
}
```

## Get a failed webhook event delivery attempt information

To retrieve information on a single attempt, use the following function:

```c#
using System;

StarkBank.Event.Attempt attempt = Starkbank.Event.Attempt.Get("1616161616161616");

Console.WriteLine(attempt);
```

## Create a new Workspace

The Organization user allows you to create new Workspaces (bank accounts) under your organization.
Workspaces have independent balances, statements, operations and users.
The only link between your Workspaces is the Organization that controls them.

**Note**: This route will only work if the Organization user is used with a null workspaceID.

```c#
using System;

StarkBank.Workspace workspace = StarkBank.Workspace.Create(
    username: "iron-bank-workspace-1",
    name: "Iron Bank Workspace 1",
    user: organization
);

Console.WriteLine(workspace);
```

## List your Workspaces

This route lists Workspaces. If no parameter is passed, all the workspaces the user has access to will be listed, but
you can also find other Workspaces by searching for their usernames or IDs directly.

```c#
using System;

List<StarkBank.Workspace> workspaces = StarkBank.Workspace.Query(
    limit: 30
).ToList();

foreach (StarkBank.Workspace workspace in workspaces)
{
    Console.WriteLine(workspace);
}
```

## Get a Workspace

You can get a specific Workspace by its id.

```c#
using System;

StarkBank.Workspace workspace = StarkBank.Workspace.Get("10827361982368179");

Console.WriteLine(workspace);
```

## Update a Workspace

You can update a specific Workspace by its id.

```c#
using System;
using System.IO;

byte[] image = File.ReadAllBytes("../../../logo.png");

Workspace updatedWorkspace = Workspace.Update(
    id: workspace.ID,
    name: "Updated workspace test",
    username: "new-username-test",
    allowedTaxIds: new List<string>(new string[] { "359.536.680-82", "20.018.183/0001-80" }),
    picture: image,
    pictureType: "image/png",
    user: Organization.Replace(organization, workspace.ID)
);

Console.WriteLine(updatedWorkspace);
```

You can also block a specific Workspace by its id.

```c#
using System;

Workspace updatedWorkspace = Workspace.Update(
    id: workspace.ID,
    status: "blocked",
    user: Organization.Replace(organization, workspace.ID)
);

Console.WriteLine(updatedWorkspace);
```

# Request

This resource allows you to send HTTP requests to StarkBank routes.

## GET

You can perform a GET request to any StarkBank route.

It's possible to get a single resource using its id in the path.

```c#
using StarkBank;

string exampleId = "5155165527080960"
JObejct request = Request.Get(
    path="/invoice/" + exampleId
).Json();

Console.WriteLine(request.ToString());
```

You can also get the specific resource log,

```c#
using StarkBank;

string exampleId = "5155165527080960";
JObejct request = Request.Get(
    path="/invoice/log/" + exampleId
).Json();

Console.WriteLine(request.ToString());
```

This same method will be used to list all created items for the requested resource.

```c#
using StarkBank;

string after = "2024-01-01";
string before = "2024-02-01";
string cursor = "";

JObject request = Request.Get(
        path="/invoice/",
        query={
            "after": after,
            "before": before,
            "cursor": cursor
        }
    ).Json();

Console.WriteLine(request.ToString());
```

To list logs, you will use the same logic as for getting a single log.

```c#
using StarkBank;

string after = "2024-01-01";
string before = "2024-02-01";
string cursor = "";

JObject request = Request.Get(
        path="/invoice/log",
        query={
            "after": after,
            "before": before,
            "cursor": cursor
        }
    ).Json();

Console.WriteLine(request.ToString());
```


You can get a resource file using this method.

```c#
using StarkBank;

string exampleId = "5155165527080960";
[]byte pdf = Request.Get(
    path="/invoice/" + exampleId + "/pdf",
).ByteContent;

System.IO.File.WriteAllBytes("request.pdf", pdf);
```

## POST

You can perform a POST request to any StarkBank route.

This will create an object for each item sent in your request

**Note**: It's not possible to create multiple resources simultaneously. You need to send separate requests if you want to create multiple resources, such as invoices and boletos.

```c#
using StarkBank;

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
    path="/invoice",
    body=data,
).Json();
Console.WriteLine(request.ToString())
```

## PATCH

You can perform a PATCH request to any StarkBank route.

It's possible to update a single item of a StarkBank resource.
```c#
using StarkBank;

string exampleId = "5155165527080960"

Dictionary<string, object> data = new Dictionary<string, object>() { { "amount", 0 } };

JObject request = Request.Patch(
    path="/invoice/" + exampleId,
    body=data,
).Json();
Console.WriteLine(request.ToString());
```

## PUT

You can perform a PUT request to any StarkBank route.

It's possible to put a single item of a StarkBank resource.
```c#
using StarkBank;

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
    path="/split-profile",
    body=data,
).Json();
Console.WriteLine(request.ToJson());
```

## DELETE

You can perform a DELETE request to any StarkBank route.

It's possible to delete a single item of a StarkBank resource.
```c#
using StarkBank;

string exampleId = "5155165527080960"
JObject request = Request.Delete(
    path="/transfer/" + exampleId,
).Json();
Console.WriteLine(request.ToString());        
```

# Handling errors

The SDK may raise one of four types of errors: __InputErrors__, __InternalServerError__, __UnknownError__, __InvalidSignatureError__

__InputErrors__ will be raised whenever the API detects an error in your request (status code 400).
If you catch such an error, you can get its elements to verify each of the
individual errors that were detected in your request by the API.
For example:

```c#
using System;
using System.Collections.Generic;
using StarkBank.Error;

try {
    List<StarkBank.Transaction> transactions = StarkBank.Transaction.Create(
        new List<StarkBank.Transaction> {
            new StarkBank.Transaction(
                amount: 99999999999999,  // (R$ 999,999,999,999.99)
                receiverID: "1029378109327810",
                description: ".",
                externalID: "12345",  // so we can block anything you send twice by mistake
                tags: new List<string> { "provider" }
            )
        }
    );
} catch (InputErrors e) {
    foreach (ErrorElement error in e.Errors) {
        Console.WriteLine(error.Code);
        Console.WriteLine(error.Message);
    }
}
```

__InternalServerError__ will be raised if the API runs into an internal error.
If you ever stumble upon this one, rest assured that the development team
is already rushing in to fix the mistake and get you back up to speed.

__UnknownError__ will be raised if a request encounters an error that is
neither __InputErrors__ nor an __InternalServerError__, such as connectivity problems.

__InvalidSignatureError__ will be raised specifically by StarkBank.Event.parse()
when the provided content and signature do not check out with the Stark Bank public
key.

# Help and Feedback

If you have any questions about our SDK, just send us an email.
We will respond you quickly, pinky promise. We are here to help you integrate with us ASAP.
We also love feedback, so don't be shy about sharing your thoughts with us.

Email: help@starkbank.com
