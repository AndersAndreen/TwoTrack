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


Let's take a look at how it all works. Below is an example taken from one of the included test use case scenarios (taken from TwoTrackUseCaseScenarioTests.UsersAndOrders.cs). The imagined scenario is to get all placed orders for a certain user.

### 1: The naive implementation
Let's start with  the most simple implementation. Get user by userName and then all orders for thar user:
```C#
            var user = _userRepository.GetByUserName(userName); // step 3 (act)
            var orders = _orderRepository.GetOrders(user); // step 4 (act)
            return orders;
```

As we all know, this would never work in a real world implementation. There is no error handling whatsoever and the application is bound to crash.

### 2: Error handling without TwoTrack
So, lets add some error handling to make it more realistic:

```C#
            User user = default;
            ICollection<Order> orders = default; 
            try
            {
                user = _userRepository.GetByUserName(userName); 
                if (user is null) _logger.Log(TwoTrackError.ResultNullError()); // logging
                else orders = _orderRepository.GetOrders(user); 
            }
            catch (Exception e) when (e is SomeExceptionThownByDatabase)
            {
                _logger.Log(TwoTrackError.Exception(e)); // logging
            }
            orders ??= new List<Order>();
            return orders;
```
Here we get 14 lines with explicit try-catch and null coalescing and an if-else statement. And we also have to remember to log errors in two places instead of one.

### 3: Error handling using TwoTrack
Now let's instead use TwoTrack to get the same functionality
```C#
            var (user, orders) = TwoTrack.Ok().Enclose(() => userName)
                .SetExceptionFilter(ex => ex is SomeExceptionThownByDatabase)
                .Select(_userRepository.GetByUserName)
                .Enclose(_orderRepository.GetOrders)
                .LogErrors(_logger.Log)
                .ValueOrDefault((User.Empty(), new List<Order>()));
            return orders;
```

As we can see: Using TwoTrack we get 6 straight steps in 7 lines without any indentation. First two setup steps, then two acting steps and finally some logging before returning the values. All exception handling is implicit, and if `_userRepository.GetByUserName` returns a null value TwoTrack automatically prevents `_orderRepository.GetOrders` from running. 

Nice eh?


# User Guide
[Factory methods](./Docs/FactoryMethods.md)



