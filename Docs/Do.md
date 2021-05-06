

## ITwoTrack.Do Method
Perform some action to cause some side effect, and keep the current values.

## ITwoTrack.Do()
```
ITwoTrack Do(Action action);
ITwoTrack Do(Func<ITwoTrack> func);
ITwoTrack Do<T>(Func<ITwoTrack<T>> func);
```

**Typical usage:** 
- Perform some action to cause a side effect, f.ex. Call SaveChanges, set a value outside of the TwoTrack scope or print some data to the console.
