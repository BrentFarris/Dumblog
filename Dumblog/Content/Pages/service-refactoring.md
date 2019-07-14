# Service Refactoring
Nicholas Ventimiglia 2019-07-14 (Init)
___

### 1) Review the public API
- A perfect contract is ACID or REST, 4 methods.
- Flag methods with side effects. Separate requests from mutations.
- Flag methods that mutate and return a complex result.
- Flag methods with callbacks.
- Flag leaky abstractions.
- Flag circumlucute, non-concise models

### 2) Estimate a refactor of the public API
Often when a service api needs to be refactored it was because it was over engineered to begin with. Whatever its problem, the bigger problem is its spread. Now consuming modules are implementing and extending the bad practice.

### 3) Review the internal procedures
- Flag large methods, a method should be no more than 10 lines of code.
- Flag bad practices. Here are a few common ones for services.
- Flag competing async philosophies (callbacks, adapters, tasks, queues, ect.).
- Flag lower level dependencies (views inside services).
- Flag horizontal level dependencies (services inside services).
- Flag multiple counters, locks, and other flow control statements.

### 4) Estimate a refactor of the private API
Often when a private api needs to be refactored its because of competing flows. The train wreck of multiple inputs and juggling of asynchronous logic. If this is the case, review the document on async behaviours, choose the best *one* for your use case, and continue. 

The second largest problem is dependencies. The spaghetti of reusing other logic adhoc via service location. The anti-pattern of lower level services calling into higher level services.. *even with indirection*. 


### 5) Do the fucking work


