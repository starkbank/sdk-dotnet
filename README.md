# Stark Bank .NET SDK

Welcome to the Stark Bank .NET SDK! This tool is made for .NET
developers who want to easily integrate with our API.
This SDK version is compatible with the Stark Bank API v2.

If you have no idea what Stark Bank is, check out our [website](https://www.starkbank.com/)
and discover a world where receiving or making payments
is as easy as sending a text message to your client!

## Supported .NET Versions

This library supports the following .NET versions:

* .NET Standard 2.1+

## Stark Bank API documentation

If you want to take a look at our API, follow [this link](https://starkbank.com/docs/api/v2).

## Installation

StarkBank`s .NET SDK is available on NuGet as StarkBank 2.0.0.

## Versioning

This project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.

## Creating a Project

To connect to the Stark Bank API, you need user credentials. We currently have 2
kinds of users: Members and Projects. Given the purpose of this SDK, it only
supports Projects, which is a type of user made specially for direct API
integrations. To start using the SDK, create your first Sandbox Project in our
[website](https://sandbox.web.starkbank.com) in the Project session.

Once you"ve created your project, load it in the SDK:

```c#
StarkBank.Project project = new StarkBank.Project(
    environment: "sandbox",
    id: "5298175129822310",
    privateKey: "-----BEGIN EC PRIVATE KEY-----\nMHQCAQEEIOJ3xkQ9NRdMPLLSrX3OlaoexG8JZgQyTMdX1eISCXaCoBcGBSuBBAAK\noUQDQgAEUneBQJsBhZl8/nPQd4YUe/UqEAtyJRH01YyWrg+nsNcSRlc1GzC3DB+X\nCPZXBUbsMQAbLoWXIN1pqIX2b/NE9Q==\n-----END EC PRIVATE KEY-----"
);
```

Once you are done testing and want to move to Production, create a Project
in your Production account ([click here](https://web.starkbank.com)). Also,
when you are loading your Project, change the environment from `"sandbox"` to
`"production"` in the constructor shown above.

NOTE: Never hard-code your private key. Get it from an environment variable, for example.

## Setting up the user

You can inform the project to the SDK in two different ways.

The first way is passing the user argument in all methods, such as:

```c#
StarkBank.Balance balance = StarkBank.Balance.Get(user: project);
```

Or, alternatively, if you want to use the same project on all requests,
we recommend you set it as the default user by doing:

```c#
StarkBank.User.Default = project;

StarkBank.Balance balance = StarkBank.Balance.Get();
```

Just select the way of passing the project user that is more convenient to you.
On all following examples we will assume a default user has been set.

## Testing in Sandbox

Your initial balance is zero. For many operations in Stark Bank, you"ll need funds
in your account, which can be added to your balance by creating a Boleto.

In the Sandbox environment, 90% of the created Boletos will be automatically paid,
so there"s nothing else you need to do to add funds to your account. Just create
a few and wait around a bit.

In Production, you (or one of your clients) will need to actually pay this Boleto
for the value to be credited to your account.


## Usage

Here are a few examples on how to use the SDK. If you have any doubts, check out
the function or class docstring to get more info or go straight to our [API docs].

### Get balance

To know how much money you have in your workspace, run:

```c#
StarkBank.Balance balance = StarkBank.Balance.Get();

Console.WriteLine(balance);
```

### Create boletos

You can create boletos to charge customers or to receive money from accounts
you have in other banks.

```c#
List<StarkBank.Boleto> boletos = StarkBank.Boleto.Create(
  boletos: new List<StarkBank.Boleto> {
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
      due: "2020-3-20",
      fine: 5,  // 5%
      interest: 2.5  // 2.5% per month
    )
  }
);

foreach (StarkBank.Boleto in boletos) {
  Console.WriteLine(boleto);
}
```

### Get boleto

After its creation, information on a boleto may be retrieved by passing its id.
Its status indicates whether it's been paid.

```c#
StarkBank.Boleto boleto = StarkBank.Boleto.Get(id: "5155165527080960")

Console.WriteLine(boleto);
```

### Get boleto PDF

After its creation, a boleto PDF may be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.Boleto.Pdf(id: "5155165527080960");

System.IO.File.WriteAllBytes("boleto.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

### Delete boleto

You can also cancel a boleto by its id.
Note that this is not possible if it has been processed already.

```c#
StarkBank.Boleto boleto = StarkBank.Boleto.Delete(id: "5155165527080960");

Console.WriteLine(boleto);
```

### Query boletos

You can get a list of created boletos given some filters.

```c#
using System;

List<StarkBank.Boleto> boletos = StarkBank.Boleto.Query(
  after: DateTime.Today.Date.AddDays(-10),
  before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Boleto boleto in boletos) {
  Console.WriteLine(boleto);
}
```

### Query boleto logs

Logs are pretty important to understand the life cycle of a boleto.

```c#
List<StarkBank.Boleto.Log> logs = StarkBank.Boleto.Log.Query(limit: 150);

foreach(StarkBank.Boleto.Log log in logs) {
  Console.WriteLine(log);
}
```

### Get a boleto log

You can get a single log by its id.

```c#
StarkBank.Boleto.Log log = StarkBank.Boleto.Log.Get(id: "5155165527080960");

Console.WriteLine(log);
```

### Create transfers

You can also create transfers in the SDK (TED/DOC).

```c#
List<StarkBank.Transfer> transfers = StarkBank.Transfer.Create(
  transfers: new List<StarkBank.Transfer> {
    new StarkBank.Transfer(
      amount: 100,  // R$ 1,00
      bankCode: "200",
      branchCode: "0001",
      accountNumber: "10000-0",
      taxID: "012.345.678-90",
      name: "Tony Stark",
      tags: new List<string> { "iron", "suit" }
    ),
    new StarkBank.Transfer(
      amount: 200,  // R$ 2,00
      bankCode: "341",
      branchCode: "1234",
      accountNumber: "123456-7",
      taxID: "012.345.678-90",
      name: "Jon Snow",
    )
  }
);

foreach(StarkBank.Transfer transfer in transfers) {
  Console.WriteLine(transfer);
}
```

### Query transfers

You can query multiple transfers according to filters.

```c#
using System;

List<StarkBank.Transfer> transfers = StarkBank.Transfer.Query(
  after: DateTime.Today.Date.AddDays(-10),
  before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Transfer transfer in transfers) {
  Console.WriteLine(transfer.name);
}
```

### Get transfer

To get a single transfer by its id, run:

```c#
StarkBank.Transfer transfer = StarkBank.Transfer.Get(id: "5155165527080960")

Console.WriteLine(transfer);
```

### Get transfer PDF

A transfer PDF may also be retrieved by passing its id.
This operation is only valid if the transfer status is "processing" or "success".

```c#
byte[] pdf = StarkBank.Transfer.Pdf(id: "5155165527080960");

System.IO.File.WriteAllBytes("transfer.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

### Query transfer logs

You can query transfer logs to better understand transfer life cycles.

```c#
List<StarkBank.Transfer.Log> logs = StarkBank.Transfer.Log.Query(limit: 50);

foreach(StarkBank.Transfer.Log log in logs) {
  Console.WriteLine(log);
}
```

### Get a transfer log

You can also get a specific log by its id.

```c#
StarkBank.Transfer.Log log = StarkBank.Transfer.Log.Get(id: "5155165527080960");

Console.WriteLine(log);
```

### Pay a boleto

Paying a boleto is also simple.

```c#
List<StarkBank.BoletoPayment> payments = StarkBank.BoletoPayment.Create(
  payments: new List<StarkBank.BoletoPayment> {
    new StarkBank.BoletoPayment(
      line: "34191.09008 61207.727308 71444.640008 5 81310001234321",
      taxID: "012.345.678-90",
      scheduled: "2020-03-13",
      description: "take my money",
      tags: new List<string> { "take", "my", "money" }
    ),
    new StarkBank.BoletoPayment(
      barCode: "34197819200000000011090063609567307144464000",
      taxID: "012.345.678-90",
      scheduled: "2020-03-14",
      description: "take my money one more time",
      tags: new List<string> { "again" }
    )
  }
);

foreach(StarkBank.BoletoPayment payment in payments) {
  Console.WriteLine(payments);
}
```

### Get boleto payment

To get a single boleto payment by its id, run:

```c#
StarkBank.BoletoPayment payment = StarkBank.BoletoPayment.Get(id: "19278361897236187236");

Console.WriteLine(payment);
```

### Get boleto payment PDF

After its creation, a boleto payment PDF may be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.BoletoPayment.Pdf(id: "5155165527080960");

System.IO.File.WriteAllBytes("boleto_payment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

### Delete boleto payment

You can also cancel a boleto payment by its id.
Note that this is not possible if it has been processed already.

```c#
StarkBank.BoletoPayment payment = StarkBank.BoletoPayment.Delete(id: "5155165527080960");

Console.WriteLine(payment);
```

### Query boleto payments

You can search for boleto payments using filters.

```c#
List<StarkBank.BoletoPayment> payments = StarkBank.BoletoPayment.Query(
  tags: new List<string> { "company_1", "company_2" }
);

foreach(StarkBank.BoletoPayment payment in payments) {
  Console.WriteLine(payments);
}
```

### Query boleto payment logs

Searches are also possible with boleto payment logs:

```c#
List<StarkBank.BoletoPayment.Log> logs = StarkBank.BoletoPayment.Log.Query(
  paymentIds: new List<string> { "5155165527080960", "76551659167801921" }
);

foreach(StarkBank.BoletoPayment.Log log in logs) {
  Console.WriteLine(log);
}
```


### Get boleto payment log

You can also get a boleto payment log by specifying its id.

```c#
List<StarkBank.BoletoPayment.Log> log = StarkBank.BoletoPayment.Log.Get(id: "5155165527080960");

Console.WriteLine(log);
```

### Create utility payment

It"s also simple to pay utility bills (such as electricity and water bills) in the SDK.

```c#
using System;

List<StarkBank.UtilityPayment> payments = StarkBank.UtilityPayment.Create(
  payments: new List<StarkBank.UtilityPayment> {
    new StarkBank.UtilityPayment(
      line: "34197819200000000011090063609567307144464000",
      scheduled: DateTime.Today.Date.AddDays(15),
      description: "take my money",
      tags: new List<string> { "take", "my", "money" }
    ),
    new StarkBank.UtilityPayment(
      barCode: "34191.09008 61207.727308 71444.640008 5 81310001234321",
      scheduled: DateTime.Today.Date.AddDays(45),
      description: "take my money one more time",
      tags: new List<string> { "again" }
    )
  }
);

foreach(StarkBank.UtilityPayment payment in payments) {
  Console.WriteLine(payment);
}
```

### Query utility payments

To search for utility payments using filters, run:

```c#
List<StarkBank.UtilityPayment> payments = StarkBank.UtilityPayment.Query(
  tags: new List<string> { "electricity", "gas" }
);

foreach(StarkBank.UtilityPayment payment in payments) {
  Console.WriteLine(payment);
}
```

### Get utility payment

You can get a specific bill by its id:

```c#
StarkBank.UtilityPayment payment = StarkBank.UtilityPayment.Get(id: "5155165527080960");

Console.WriteLine(payment);
```

### Get utility payment PDF

After its creation, a utility payment PDF may also be retrieved by passing its id.

```c#
byte[] pdf = StarkBank.UtilityPayment.Pdf(id: "5155165527080960");

System.IO.File.WriteAllBytes("electricity_payment.pdf", pdf);
```

Be careful not to accidentally enforce any encoding on the raw pdf content,
as it may yield abnormal results in the final file, such as missing images
and strange characters.

### Delete utility payment

You can also cancel a utility payment by its id.
Note that this is not possible if it has been processed already.

```c#
StarkBank.UtilityPayment payment = StarkBank.UtilityPayment.Delete(id: "5155165527080960");

Console.WriteLine(payment);
```

### Query utility bill payment logs

You can search for payments by specifying filters. Use this to understand the
bills life cycles.

```c#
List<StarkBank.UtilityPayment.Log> logs = StarkBank.UtilityPayment.Log.Query(
  paymentIds: new List<string> { "102893710982379182", "92837912873981273" }
);

foreach(StarkBank.UtilityPayment.Log log in logs) {
  Console.WriteLine(log);
}
```

### Get utility bill payment log

If you want to get a specific payment log by its id, just run:

```c#
List<StarkBank.UtilityPayment.Log> log = StarkBank.UtilityPayment.Log.Get(id: "1902837198237992");

Console.WriteLine(log);
```

### Create transactions

To send money between Stark Bank accounts, you can create transactions:

```c#
List<StarkBank.Transaction> transactions = StarkBank.Transaction.Create(
  transactions: new List<StarkBank.Transaction> {
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

### Query transactions

To understand your balance changes (bank statement), you can query
transactions. Note that our system creates transactions for you when
you receive boleto payments, pay a bill or make transfers, for example.

```c#
using System;

List<StarkBank.Transaction> transactions = StarkBank.Transaction.Query(
  after: DateTime.Today.Date.AddDays(-10),
  before: DateTime.Today.Date.AddDays(-1)
);

foreach(StarkBank.Transaction transaction in transactions) {
  Console.WriteLine(transaction);
}
```

### Get transaction

You can get a specific transaction by its id:

```c#
StarkBank.Transaction transaction = StarkBank.Transaction.Get(id: "5155165527080960");

Console.WriteLine(transaction);
```

### Create webhook subscription

To create a webhook subscription and be notified whenever an event occurs, run:

```c#
StarkBank.Webhook webhook = StarkBank.Webhook.Create(
  url: "https://webhook.site/dd784f26-1d6a-4ca6-81cb-fda0267761ec",
  subscriptions: new List<string> { "transfer", "boleto", "boleto-payment", "utility-payment" }
);

Console.WriteLine(webhook);
```

### Query webhooks

To search for registered webhooks, run:

```c#
List<StarkBank.Webhook> webhooks = StarkBank.Webhook.Query();

foreach(StarkBank.Webhook webhook in webhooks) {
  Console.WriteLine(webhook);
}
```

### Get webhook

You can get a specific webhook by its id.

```c#
StarkBank.Webhook webhook = StarkBank.Webhook.Get(id: "10827361982368179");

Console.WriteLine(webhook);
```

### Delete webhook

You can also delete a specific webhook by its id.

```c#
StarkBank.Webhook webhook = StarkBank.Webhook.Delete(id: "10827361982368179");

Console.WriteLine(webhook);
```

### Process webhook events

It"s easy to process events that arrived in your webhook. Remember to pass the
signature header so the SDK can make sure it's really StarkBank that sent you
the event.

```c#
Response response = listen()  // this is the method you made to get the events posted to your webhook

StarkBank.Event parsedEvent = StarkBank.Event.parse(
    content: response.Content,
    signature: response.Headers["Digital-Signature"]
);

if (parsedEvent.subscription == "transfer") {
  Console.WriteLine(parsedEvent.log.transfer);
} else if (parsedEvent.subscription == "boleto") {
  Console.WriteLine(parsedEvent.log.boleto);
} else if (parsedEvent.subscription == "boleto-payment") {
  Console.WriteLine(parsedEvent.log.payment);
} else if (parsedEvent.subscription == "utility-payment") {
  Console.WriteLine(parsedEvent.log.payment);
}
```

### Query webhook events

To search for webhook events, run:

```c#
using System;

List<StarkBank.Event> events = StarkBank.Event.Query(
    after: DateTime.Today,
    isDelivered: false
);

foreach(StarkBank.Event eventObject in events) {
  Console.WriteLine(eventObject);
}
```

### Get webhook event

You can get a specific webhook event by its id.

```c#
StarkBank.Event event = StarkBank.Event.Get(id: "10827361982368179");

Console.WriteLine(event);
```

### Delete webhook event

You can also delete a specific webhook event by its id.

```c#
StarkBank.Event event = StarkBank.Event.Delete(id: "10827361982368179");

Console.WriteLine(event);
```

### Set webhook events as delivered

This can be used in case you"ve lost events.
With this function, you can manually set events retrieved from the API as
"delivered" to help future event queries with `isDelivered: false`.

```c#
StarkBank.Event event = StarkBank.Event.update(id: "129837198237192", isDelivered: true);

Console.WriteLine(event);
```

## Handling errors

The SDK may raise one of four types of errors: __InputErrors__, __InternalServerError__, __UnknownError__, __InvalidSignatureError__

__InputErrors__ will be raised whenever the API detects an error in your request (status code 400).
If you catch such an error, you can get its elements to verify each of the
individual errors that were detected in your request by the API.
For example:

```c#
try {
    List<StarkBank.Transaction> transactions = StarkBank.Transaction.Create(
        transactions: new List<StarkBank.Transaction> {
            new StarkBank.Transaction(
                amount: 99999999999999,  // (R$ 999,999,999,999.99)
                receiverID: "1029378109327810",
                description: ".",
                externalID: "12345",  // so we can block anything you send twice by mistake
                tags: new List<string> { "provider" }
            )
        }
    );
} catch (StarkBank.Error.InputErrors e) {
    foreach (StarkBank.Error.Error error in e.Errors)
    {
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

## Key pair generation

The SDK provides a helper to allow you to easily create ECDSA secp256k1 keys to use
new within our API. If you ever need a pair of keys, just run:

```c#
(string privateKey, string publicKey) = StarkBank.Key.Create();

# or, to also save .pem files in a specific path
(string privateKey, string publicKey) = StarkBank.Key.Create("file/keys");
```

NOTE: When you are creating a new Project, it is recommended that you create the
keys inside the infrastructure that will use it, in order to avoid risky internet
transmissions of your **private-key**. Then you can export the **public-key** alone to the
computer where it will be used in the new Project creation.


[API docs]: (https://starkbank.com/docs/api/v2)
