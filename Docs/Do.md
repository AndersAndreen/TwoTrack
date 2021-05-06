

## ITwoTrack.Do Method


## ITwoTrack.Do()
```
ITwoTrack Do(Action action);
ITwoTrack Do(Func<ITwoTrack> func);
ITwoTrack Do<T>(Func<ITwoTrack<T>> func);
```

**Typical usage:** 
- Call DbContext.SaveChanges(),
- Set a value outside of the TwoTrack scope
- Print some data to the console.
