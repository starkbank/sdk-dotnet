# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.


## [Unreleased]
### Changed
- internal structure to use starkcore as a dependency

## [2.10.0] - 2023-09-18
### Removed
- accountCreated, created, owned attributes to DictKey resource
- accountNumber, branchCode attributes to PaymentPreview resource
### Changed
- accountNumber, branchCode attributes to DictKey resource
### Fixed 
- accountType docstring attribute to DictKey resource

## [2.9.0] - 2023-05-12
### Added
- description attribute to CorporatePurchase.Log resource
- purpose attribute to CorporateRule resource
- Invoice.Rule sub-resource
- rules attribute to Invoice resource

## [2.8.0] - 2023-04-11
### Added
- CorporateBalance resource
- CorporateCard resource
- CorporateHolder resource
- CorporateInvoice resource
- CorporatePurchase resource
- CorporateRule resource
- CorporateTransaction resource
- CorporateWithdrawal resource
- CardMethod sub-resource
- MerchantCategory sub-resource
- MerchantCountry sub-resource

## [2.7.0] - 2023-03-22
### Added
- picture and pictureType parameters to Workspace.update method
- rules and metadata attribute to Transfer resource
- Transfer.Rule sub-resource
- rules attribute to BrcodePayment resource
- BrcodePayment.Rule sub-resource
- DynamicBrcode resource
- status, created, organizationId and pictureUrl attributes to Workspace resource
- workspaceId attribute to Boleto resource
- updated attribute to BoletoHolmes.Log resource
- updated, transactionIds and type attributes to UtilityPayment resource
- description attribute to PaymentRequest resource
- transactionIds attribute to BoletoPayment, DarfPayment and TaxPayment resource
### Changed
- amount attribute to parameter to BoletoPayment resource
### Removed
- deprecated BrcodePreview resource

## [2.6.4] - 2022-09-22
### Added
- Extra parameter to Boleto.query()

## [2.6.3] - 2022-05-05
### Added
- OurNumber parameter to Boleto resource

## [2.6.2] - 2021-11-10
### Changed
- starkbank-ecdsa library version to 1.3.3

## [2.6.1] - 2021-11-04
### Changed
- starkbank-ecdsa library version to 1.3.2

## [2.6.0] - 2021-09-04
### Added
- Support for scheduled invoices, which will display discounts, fine, interest, etc. on the users banking interface when dates are used instead of datetimes
- PaymentPreview resource to preview multiple types of payments before confirmation: BrcodePreview, BoletoPreview, UtilityPreview and TaxPreview

## [2.5.0] - 2021-07-27
### Added
- "payment" account type for Pix related resources
- Event.WorkspaceId property to allow multiple Workspace Webhook identification
- Workspace.Update() to allow parameter updates
- AllowedTaxIds property to Workspace resource
- Invoice.Link property to allow easy access to invoice webpage
- Transfer.Description property to allow control over corresponding Transaction descriptions
- Base exception class
- missing parameters to Boleto, BrcodePayment, Deposit, DictKey and Invoice resources
- InvoicePayment sub-resource to allow retrieval of invoice payment information
- Event.Attempt sub-resource to allow retrieval of information on failed webhook event delivery attempts
- pdf method for retrieving PDF receipts from reversed invoice logs
- page functions as a manual-pagination alternative to queries
- Institution resource to allow query of institutions recognized by the Brazilian Central Bank for Pix and TED transactions
- TaxPayment resource
- DarfPayment resource to allow DARF tax payment without bar code
### Fixed
- special characters in BrcodePreview query

## [2.4.0] - 2021-01-21
### Added
- Transfer.accountType property to allow "checking", "salary" or "savings" account specification
- Transfer.externalID property to allow users to take control over duplication filters

## [2.3.0] - 2021-01-16
### Added
- Organization user
- Workspace resource

## [2.2.4] - 2020-12-29
### Added
- Invoice.PdfUrl property
### Fixed
- Invoice reversal with Invoice.Update(amount: 0)
- BrcodePreview.query cursor issue

## [2.2.3] - 2020-11-18
### Fixed
- Bad midnight DateTime string conversions

## [2.2.2] - 2020-11-17
### Fixed
- DateTime string timezone bug in some environments

## [2.2.1] - 2020-11-17
### Fixed
- Invoice optional due parameter

## [2.2.0] - 2020-11-16
### Added
- Invoice resource to load your account with dynamic QR Codes
- DictKey resource to get DICT (Pix) key parameters
- Deposit resource to receive transfers passively
- Pix support in Transfer resource
- BrcodePayment support to pay static and dynamic Pix QR Codes

## [2.1.0] - 2020-10-28
### Added
- BoletoHolmes to investigate boleto status according to CIP

## [2.0.0] - 2020-10-20
### Added
- ids parameter to Transaction.query
- ids parameter to Transfer.query
- PaymentRequest resource to pass payments through manual approval flow

### Fixed
- BoletoPayment test case
- UtilityPayment test case

## [0.9.0] - 2020-08-12
### Added
- Transfer.scheduled parameter to allow Transfer scheduling
- Transfer.Delete to cancel scheduled Transfers
- Transaction query by tags

## [0.8.0] - 2020-06-05
### Added
- Transfer query taxID parameter

## [0.7.0] - 2020-05-26
### Added
- StarkBank.Settings.Language to specify error language as "en-US" or "pt-BR"
- Boleto PDF layout option
### Change
- Test user credentials to environment variable instead of hard-code
- Default user from StarkBank.User.Default to StarkBank.Settings.User

## [0.6.0] - 2020-05-12
### Added
- "receiver_name" & "receiver_tax_id" properties to Boleto entities

## [0.5.1] - 2020-05-12
### Fixed
- /GET requests in .NET Framework 4.6.1

## [0.5.0] - 2020-05-12
### Added
- Support for .NET Standard 2.0

## [0.4.0] - 2020-05-05
### Added
- Support for dictionaries in create methods
- "balance" property to Transaction entities
### Fixed
- Docstrings

## [0.3.1] - 2020-05-05
### Fixed
- SDK User-Agent header

## [0.3.0] - 2020-04-29
### Added
- "discounts" property to Boleto entities
### Changed
- Internal folder structure
### Fixed
- pt-BR environment credential headers bug

## [0.2.0] - 2020-04-17
### Changed
- StarkBank.Error.Error to StarkBank.Error.ErrorElement

## [0.1.0] - 2020-04-17
### Added
- Full Stark Bank API v2 compatibility
