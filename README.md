# TwoTrack
A Railway-oriented approach to functional style result handling in .Net

***NOTE: This project is in alpha. There is no support for async code yet, but it's in the roadmap.***

# Introduction - What is this?
As a developer I have always struggled with writing readable code describing a clear business case and on the same time having sufficient error handling. What starts out as readable code often ends up littered with null checks and if-statements. And if there are errors, how do I select which ones to show the user, which ones to log and which ones to ignore? And what about messages to report a successful operation?

In the summer of 2019 i came across a way of applying functional style result/error handling called [Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/). I implemented my first C# version of this in an internal closed source development project. TwoTrack is my open source version of the original implementation. It is still in it's infancy, but feel free to check it out! I have included some use case scenarios in the form of unit tests to show some of the functionality that I've implemented so far.

Railway Oriented Programming uses the metaphor of two parallel tracks: A success track and a failure track. Result data travels along the success track until there is an error. If an error occurs the data switches to the failure track and succeeding actions are bypassed. All values gets encapsulated in a result object that holds the values, as well as two collections: Errors and Confirmations. If there is an error all access to the values are blocked. 

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
            var user = _userRepository.GetByUserName(userName);  
            var orders = _orderRepository.GetOrders(user);  
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
            catch (Exception e) when (e is SomeExceptionThownByEnitiyFramework)
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
            var (user, orders) = TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is SomeExceptionThownByEnitiyFramework)
                .Enclose(() => userName)
                .Select(_userRepository.GetByUserName)
                .Enclose(_orderRepository.GetOrders)
                .LogErrors(_logger.Log) 
                .ValueOrDefault((User.Empty(), new List<Order>()));
            var toReturn = orders;
```

Using TwoTrack we get 7 straight steps in 7 lines without any indentation. First three setup steps, then two acting steps and finally some logging before returning the values. Both repository calls have implicit exception catching, and if `_userRepository.GetByUserName` returns a null value TwoTrack automatically prevents `_orderRepository.GetOrders` from running. 

Nice eh?

# Usage Documentation
[Factory methods](./Docs/FactoryMethods.md)  
[Enclose vs Select vs Do](./Docs/EncloseVsSelectVsDo.md)  
[Enclose](./Docs/Enclose.md)  
[Select](./Docs/Select.md)  
[Do](./Docs/Do.md)  

# ADR documentation 
*(just started, not really much here yet)*  
[Architecture Decision Record](./Docs/ADR)  

