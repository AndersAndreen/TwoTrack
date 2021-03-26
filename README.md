# TwoTrack
A Railway-oriented approach to functional style result handling in .Net

***IMPORTANT: This project is still pre-alpha! Expect daily updates and breaking changes.***

# Introduction - What is this?
As a developer I have always struggled with writing readable code describing a clear business case and on the same time having sufficient error handling. What starts out as readable code often ends up as a chain of null checks and if-statements to check that everything is in order. And if there are errors, how do I select which ones to show the user, which ones to log and which ones to ignore?  And what about messages to report a successful operation?

In the summer of 2019 i came across a way of applying functional style result/error handling in .net called [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/). Using this method you encapsulate all return values in a result object that holds the value, if there is one, as well as two collections: Errors and Confirmations.

## Principles:
- Clear intent over verbose error handling
- Injecting functionality over getting value out
- Errors over exceptions

## Features:
- Automatic null check of inputs
- Built in optional and configurable catch and conversion of exceptions to errors
- Supports both error messages and confirmation messages
- Designed to support both Web APIs and ASP .NET solutions
- Error and confirmation levels are compatible with ILogger for easy conversion
 
# API Reference
## TtError 
parameters:
- ErrorLevel
  - For logging: Warning, Error, Critical
  - For APIs: ReportWarning, ReportError
  - For ASP .Net Views: ReportWarning, ReportError
- Category
  - For logging: "Exception", "DatabareError" etc. 
  - For APIs: "Status400BadRequest",  "Status500InternalServerError" etc. 
  - For ASP .Net Views: "UserName", "Telephone", "Password" etc.
- Description
- StackTrace

## TtError<T>
- All the features of TtError
- encapsulation of one result object/value.
