# TwoTrack
A Railway-oriented approach to functional style result handling in .Net

## Principles:
- Clear intent over verbose error handling
- Injecting functionality over getting value out
- Errors over exceptions
- Async all the way - if you want it

## Features:
- Automatic null check of inputs
- Supports both error and confirmation messages
- Designed to support both Webb APIs and ASP .NET solutions
- Error and confirmation levels are compatible with ILogger
 
# Documentation
## TtError 
parameters:
- ErrorLevel
  - For logging: Warning, Error, Critical
  - For APIs: ReportWarning, ReportError
  - For ASP .Net Views: ReportWarning, ReportError
- Category (
  - For logging: "Exception", "DatabareError" etc. 
  - For APIs: "Status400BadRequest",  "Status500InternalServerError" etc. 
  - For ASP .Net Views: "UserName", "Telephone", "Password" etc.
- Description
- StackTrace

