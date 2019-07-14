# Asynchronous Programming
___

Asynchronous programming is art of using a method pointer to be called at a undetermined later time. Generally this pointer set at runtime, because of this we introduce unknown opportunities for competing flows.... *race conditions*.


| Solution  |Stateless|Listeners|Mixing |Chained|Inline |
| --------- |-------------------------------------------|
| Callbacks | NO      | MANY    |**NO** | YES   | NO    |
| Adapter   | **YES** |**MONO** |*Maybe*| YES   | NO    |
| Events    | **YES** | MANY    |**NO** |**NO** | NO    |
| Queue     | **YES** | MANY*    |**NO** |**NO**|**YES**|
| Tasks     | NO      |**MONO** |**NO** |**NO** |**YES**|


**Stateless** Is good because it means the service does not need to cache or manage the state of the transaction. Generally these transactions are cached as pointers which can not be cached preventing caching of the complete application state.

**Listeners** Having multiple listeners is dangerous because it introduces branching into the logical flow. Often Its a result of not thinking out the complete systems flow. We are literally saying 'something happens and I dont know what'. 

> A Queue should have one listener... but there is no protection.

**Mixing** The act of a lower level service (data) calling into a higher level service (view). While this is often done via a layer of indirection, the core problem is still there : The data layer needs to know about view specific cases so that it can call the correct method on completion. 

**Chained** Because the service has access to a pointer to the view code, it is very easy to wrap this into another call and *pass the buck* in a daisy chain. We can quickly loose track of our stack and our code starts to look like javascript.

**Inline** The idea that we can consolidate our control code into a single method.

### Final Thoughts

In a game, my favorite solution is the Queue. It is ideal for handling a *stream* of messages in a stateless way.

For transient transactions, my favorite solution is the Task. It may not be stateless, but it is great at consolidating the transactions control logic into a single location.

The adapter pattern are a great way to rope in bad callback code. They do the job of standardizing the spaghetti, making it easier to cut. 

Callbacks and events are the worst.