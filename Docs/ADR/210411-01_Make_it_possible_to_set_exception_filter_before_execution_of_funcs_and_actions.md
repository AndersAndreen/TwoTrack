# 210411-01_Make_it_possible_to_set_exception_filter_before_execution_of_funcs_or_actions

## Status: accepted
[//]: # (proposed, accepted, rejected, deprecated, superseded)

## Context
The default exception filter in TwoTrack is to ignore all exceptions. This pose a problem for two static factory methods:
`Enclose(Action action)` and `Enclose<T>(Func<T> func)`. As both these are static it is not possible to 
set an exception filter before the action/function is executed. Since these functions return void or a naked value they 
lack the protection of TwoTrack.

The methods Enclose(Func<ITwoTrack> func) and Enclose<T>(Func<ITwoTrack<T>> func) does not pose the same problem, since their 
return values are encapsulated in an ITwoTrack.

## Decision
- Remove factory methods `ITwoTrack Enclose(Action action)` and `ITwoTrack<T> Enclose<T>(Func<T> func)` from class `TwoTrack`.
- Replace all TwoTrack.Enclose(action) with TwoTrack.Ok().Enclose(action).
- Replace all TwoTrack.Enclose(func) with TwoTrack.Ok().Enclose(func).
- Implement the Enclose functionality inside TtResult and TtResultGeneric.
- Add a factory method `TwoTrack.Do(exceptionFilter)`

## Consequences 
- Clearer intent at the cost of a few more key strokes.
- By forcing users to start with `TwoTrack.Do()` or `TwoTrack.Do(exceptionFilter)` it becomes easier to see when exceptions 
are caught and when they aren't. It also makes it easy to add an exceptionFilter later.
