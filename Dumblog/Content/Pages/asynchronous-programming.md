# Asynchronous Programming
___

Asynchronous programming is art of using a method pointer to be called at a undetermined later time. Generally this pointer set at runtime, because of this we introduce unknown opportunities for competing flows.... *race conditions*.


| Solution  |Stateless|Listeners|Mixing |Chained|Inline |
| --------- |-------------------------------------------|
| Callbacks | NO      | MANY    |**NO** | YES   | NO    |
| Adapter   | **YES** |**MONO** |*Maybe*| YES   | NO    |
| Events    | **YES** | MANY    |**NO** |**NO** | NO    |
| Queue     | **YES** | MANY*    |**NO** |**NO**|**YES**|
| TASKS     | NO      |**MONO** |**NO** |**NO** |**YES**|


**Stateless** Is good because it means the service does not need to cache or manage the state of the transaction. Generally these transactions are cached as pointers which can not be cached preventing caching of the complete application state.

**Listeners** Having multiple listeners is dangerous because it introduces branching into the logical flow. Often Its a result of not thinking out the complete systems flow. We are literally saying 'something happens and I dont know what'. 

> A Queue should have one listener... but there is no protection.

**Mixing** The act of a lower level service (data) calling into a higher level service (view). While this is often done via a layer of indirection, the core problem is still there : The data layer needs to know about view specific cases so that it can call the correct method on completion. 

**Chained** Because the service has access to a pointer to the view code, it is very easy to wrap this into another call and *pass the buck* in a daisy chain. We can quickly loose track of our stack and our code starts to look like javascript.

**Inline** The idea that we can consolidate our control code into a single method.