# TtResultTests: Immutability

## AddError(TtError)
step 1 states:
1: succeeeded - 0 errors (e)
2: failed - 1 error (e)

step 2 actions and e(e)pected results:
1: null -> e+1 errors 
2: error -> e+1 errors
(3 is untestable since TtError is a value object): throws -> e+1 errors 

=> 4 tests needed


## AddConfirmation(TtConfirmation)
step 1 states:
1: succeeeded - 0 errors (e), 0 confirmations (c)
2: failed - 1 error (e), 0 confirmations (c)
3: succeeeded - 0 errors (e), 1 confirmations (c)
4: failed - 1 error (e), 1 confirmations (c)

step 2 actions and e(e)pected results:
1: null -> e+1 error, c+0 confirmations
2: confirmation -> e+0 error, c+1 confirmation
(3 is untestable since TtError is a value object): throws -> e+1 error, c+0 confirmations

=> 8 tests needed


## AddErrors(IEnumerable<TtError>)
step 1 states:
1: succeeeded - 0 errors
2: failed - 1 error

step 2 actions and e(e)pected results (when adding two errors):
1: null -> e+1 errors 
3: error -> e+2 errors
(3 is untestable since TtError is a value object): throws -> e+1 errors 

=> 4 tests needed


## AddConfirmations(IEnumerable<TtConfirmation>)
step 1 states:
1: succeeeded - 0 errors (e), 0 confirmations (c)
2: failed - 1 error (e), 0 confirmations (c)
3: succeeeded - 0 errors (e), 1 confirmations (c)
4: failed - 1 error (e), 1 confirmations (c)

step 2 actions and e(e)pected results:
1: null -> e+1 error, c+0 confirmations
2: confirmation -> e+0 error, c+2 confirmations
(3 is untestable since TtError is a value object): throws -> e+1 error, c+0 confirmations

=> 8 tests needed


## Do(Action)
step 1 states:
1: succeeeded - 0 errors (e)
2: failed - 1 error (e)

step 2 actions and e(e)pected results:
1: null -> e+1 errors
2: throws -> e+1 errors
3: succeeded -> if (1 succeeded) e+0 errors

=> 6 tests needed

## Do(Func<ITwoTrack>)
step 1 states:
1: succeeeded - 0 errors (e)
2: failed - 1 error (e)

step 2 actions and e(e)pected results:
1: null -> e+1 error
2: Error -> e+1 error
3: throws -> e+1 error
4: succeeded -> if (1 succeeded) e+0 errors

=> 8 tests needed

## Do<T>(Func<ITwoTrack<T>> func)
step 1 states:
1: succeeeded - 0 errors (e)
2: failed - 1 error (e)

step 2 actions and e(e)pected results:
1: null -> e+1 error
2: Error -> e+1 error
3: throws -> e+1 error
4: succeeded -> if (1 succeeded) e+0 errors

=> 8 tests needed