

## ITwoTrack.Select Method
Select works just like Linq's `IEnumerable<T>.Select()`: Take an input value of any type and output a value of any other type. 
The difference from Linq (besides the error handling) is that `ITwoTrack.Select()` works on any type, not just IEnumerable<T>.

**Typical usage:**
- Mapping some values into a new type, f.ex. from EF-model to DTO or view model.
- Discard some values no longer needed to increase readability.
- Do some database call to get data that is needed for further processing, replacing the current data.

[Go back](../README.md#usage-documentation)  
