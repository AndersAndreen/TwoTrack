# TwoTrack
A Railway-oriented approach to functional style result handling in .Net

***IMPORTANT: This project is still pre-alpha! Expect daily updates and breaking changes.***

# Introduction - What is this?
As a developer I have always struggled with writing readable code describing a clear business case and on the same time having sufficient error handling. What starts out as readable code often ends up littered with null checks and if-statements to check that everything is in order. And if there are errors, how do I select which ones to show the user, which ones to log and which ones to ignore?  And what about messages to report a successful operation?

In the summer of 2019 i came across a way of applying functional style result/error handling in .net called [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/). Using this method you encapsulate all return values in a result object that holds the value, if there is one, as well as two collections: Errors and Confirmations. TwoTrack is my C# implementation of Railway Oriented Programming. It is still in it's infancy, but feel free to check it out! I have included some use case scenarios in the form of unit tests, and a demo web app to show some of the functionality that I've implemented so far.

## Features:
- Automatic null check of inputs
- Built in optional and configurable try-catch and conversion of exceptions to errors
- Supports both error messages and confirmation messages
- Error and confirmation levels are compatible with ILogger for easy conversion
- Designed to support both Web APIs and ASP .NET solutions

## Principles:
TwoTrack generally follows these four principles:
- Clear intent over verbose error handling
- Injecting functionality over getting value out
- Errors over null values
- Errors over exceptions

As you probably noticed all principles are written in the same form as the agile manifesto. While there is evident value in the approches on the right, TwoTrack  value the approaches on the left more.

## A first Example
Let's take a look at how it all works. Below is an example taken from one of the use case scenarios (taken from TwoTrackUseCaseScenarioTests.UsersAndOrders.cs):

```C#
            var (user, orders) = TwoTrack.Ok() // step 1 (arrange)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase) // step 2 (arrange)
                .Enclose(() => _context.Users.FirstOrDefault(user1 => user1.UserName == userName)) // step 3 (act)
                .Enclose(user1 => _context.Orders.Where(order => order.UserId == user1.UserId).ToList()) // step 4 (act)
                .LogErrors(errors => Log(errors.ToArray())) // step 5 (act)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final nullcheck)
        }

```

Using TwoTrack we get 6 steps in 6 simple lines. First two setup steps and then all actions. All exception handling is implicit, and if a function returns a null value TwoTrack automatically prevents subsequent function calls. 

Let's compare this to some code with corresponding functionality and error handling, but without using TwoTrack:

```C#
            User user = default; // step 1 (arrange):
            List<Order> orders = default; // step 1 (arrange):
            try
            {
                user = _context.Users.FirstOrDefault(user1 => user1.UserName == userName); // step 3 (act)
                if( user is null) Log(TtError.ResultNullError()); // step 5 (logging)
                else orders = _context.Orders.Where(order => order.UserId == user?.UserId).ToList(); // step 4 (act)
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase) // step 2 (arrange)
            {
                Log(TtError.Exception(e)); // step 5 (logging)
            }
            user ??= User.Empty(); // step 6 (nullcheck)
            orders ??= new List<Order>(); // step 6 (final nullcheck)
```
Here we get 14 lines with explicit try-catch and null coalescing. And one if-else statement. And we also have to remeber to log errors in two places instead of one.


