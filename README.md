# TwoTrack
A Railway-oriented approach to functional style result handling in .Net

## Principles:
- Clear intent over verbose error handling
- Injecting functionality over getting value out
- Errors over exceptions

## Features:
- Automatic null check of inputs
- Built in optional and configurable catch and conversion of exceptions to errors
- Supports both error messages and confirmation messages
- Designed to support both Webb APIs and ASP .NET solutions
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
