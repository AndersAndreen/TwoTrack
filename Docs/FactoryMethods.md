# Factory methods

## TwoTrack.Ok()
![alt text](./Img/StaticOk.png "Diagram symbol for method TwoTrack Ok.")
Static method that generates an ITwoTrack object without any errors.

**Usage:** 
- To start an ItwoTrack command sequence
- To return a success object, maybe with some confirmation messages, from a service or helper method.

## TwoTrack.Fail()
![alt text](./Img/StaticFail.png "Diagram symbol for method TwoTrack Fail.")
Static method that generates an ITwoTrack object with errors. This method has three overloads:

`ITwoTrack Fail(Exception exception)`  
`ITwoTrack Fail(TwoTrackError error)`  
`ITwoTrack Fail(ITtCloneable result, TwoTrackError error = default)`  

**Usage:** 
- To return a failure object with an error message from a service or helper method.
- To take all errors and messages from another result object (TwoTrack or TwoTrack<T>), add another error message.

## TwoTrack.Fail<T>()
![alt text](./Img/StaticFailT.png "Diagram symbol for method TwoTrack Fail of T.")
Static method that generates an ITwoTrack object of type T with errors. This method has three overloads:

 `ITwoTrack<T> Fail<T>(Exception exception)`  
 `ITwoTrack<T> Fail<T>(TwoTrackError error)`  
 `ITwoTrack<T> Fail<T>(ITtCloneable result, TwoTrackError error = default)`  

 **Usage:** 
- To return an empty failure object of type T with an error message from a service or helper method.
- To take all errors and messages from another result object (TwoTrack or TwoTrack<T>), add another error message.
