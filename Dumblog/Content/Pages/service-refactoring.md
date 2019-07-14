# Service Refactoring
Nicholas Ventimiglia
___

### 1) Review the public API
- A perfect service contract is ACID or REST, 4 methods.
- `Post` or other mutating methods should return `void`
- `Put` or transacting methods should return `void` or a *simple* result Boolean.
- `Get` methods should return an unmolested copy, and never mutate the data.
- Flag methods with callbacks for closer review.
- Flag leaky abstractions for sealing.

### 2) Estimate a refactor of the public API
Often when a service api needs to be refactored it was because it was over engineered to begin with. Whatever its problem, the bigger problem is its spread. Now consuming modules are implementing and extending the bad practice.

### 3) Review the private procedures
- Clean large methods, a method should be no more than 10 lines of code.
- A service should adhere to a single async pattern (adapter, task, events or queue).
- Remove horizontal level dependencies (services inside services).
- Remove multiple counters, locks, and other flow control statements.

### 4) Estimate a refactor of the private procedures
Often when a private procedures needs to be refactored, its because of competing async flows. If this is the case, review the document on async behaviours, choose the best *one* for your use case, and continue.

Moreover, this is many times a fact because of branching async flows inherited from dependencies. (e.g. we need to wait on Module A and Module B before we can continue). If this is the case, return to step one and strongly consider refactoring the dependent modules in such a way as to not *require* waiting.