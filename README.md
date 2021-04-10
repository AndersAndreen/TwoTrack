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
Let's take a look at how it all works. Below is an example taken from one of the included test use case scenarios (taken from TwoTrackUseCaseScenarioTests.UsersAndOrders.cs):

```C#
            var (user, orders) = TwoTrack.Enclose(() => userName) // step 1 (arrange)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase) // step 2 (arrange)
                .Select(_userRepository.GetByUserName) // step 3 (db call)
                .Enclose(_orderRepository.GetOrders) // step 4 (db call)
                .LogErrors(_logger.Log) // step 5 (logging)
                .ValueOrDefault((User.Empty(), new List<Order>())); // step 6 (final null handling)
        }

```

Using TwoTrack we get 6 steps in 6 simple lines. First two setup steps, then two database calls and finally some logging before returning the values. All exception handling is implicit, and if `_userRepository.GetByUserName` returns a null value TwoTrack automatically prevents `_orderRepository.GetOrders` from running. 

Let's compare this to some code with corresponding functionality and error handling, but without using TwoTrack:

```C#
            User user = default; // step 1 (arrange):
            ICollection<Order> orders = default; // step 1 (arrange):
            try
            {
                user = _userRepository.GetByUserName(userName); // step 3 (act)
                if (user is null) _logger.Log(TtError.ResultNullError()); // step 5 (logging)
                else orders = _orderRepository.GetOrders(user); // step 4 (act)
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase) // step 2 (arrange)
            {
                _logger.Log(TtError.Exception(e)); // step 5 (logging)
            }
            user ??= User.Empty(); // step 6 (final null handling)
            orders ??= new List<Order>(); // step 6 (final null handling)
```
Here we get 14 lines with explicit try-catch and null coalescing. And one if-else statement. And we also have to remeber to log errors in two places instead of one.


![alt text](./Docs/Img/StaticEnclose.png "Diagram image showing StaticEnclose" | height=100)



