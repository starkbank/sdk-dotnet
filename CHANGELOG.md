# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to the following versioning pattern:

Given a version number MAJOR.MINOR.PATCH, increment:

- MAJOR version when the **API** version is incremented. This may include backwards incompatible changes;
- MINOR version when **breaking changes** are introduced OR **new functionalities** are added in a backwards compatible manner;
- PATCH version when backwards compatible bug **fixes** are implemented.


## [Unreleased]
### Added
- Invoice resource to load your account with dynamic QR Codes
- DictKey resource to get DICT (PIX) key parameters
- Deposit resource to receive transfers passively
- PIX support in Transfer resource

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
