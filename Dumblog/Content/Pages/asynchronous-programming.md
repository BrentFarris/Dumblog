# Asynchronous Programming
___

Asynchronous programming is strategy of calling a method at a undetermined later time. Generally this method set at runtime, and because of this we introduce *race conditions*.


| Solution  |Stateless|Listeners|Mixing |Chained|Inline |
| --------- |-------------------------------------------|
| Callbacks | NO      | MANY    |**NO** | YES   | NO    |
| Adapter   | **YES** |**MONO** |*Maybe*| YES   | NO    |
| Events    | **YES** | MANY    |**NO** |**NO** | NO    |
| Queue     | **YES** | MANY*    |**NO** |**NO**| NO    |
| Tasks     | NO      |**MONO** |**NO** |**NO** |**YES**|


### Stateless
Is the method in a static location? Or are we passing the method as an object? It is a best practice to have a consistent method location and instead pass the *data* around as an object. By doing this we can have predictable logic flow, we are separating our *instruction and data* stacks on the cpu, and we can serialize the entire application state if needed.

### Listeners

Having multiple listeners to a single resource. This may be dangerous because it introduces branching logic. While this can be a valid strategy to achieve extensible, often Its a result of not thinking out the complete systems flow. We are literally saying 'something happens and *I don't know what*'. 

### Layer Mixing
The act of a lower level service (data) calling into a higher level service (view). Now the data layer needs to know about view specific cases so that it can call the correct method on completion. 

### Daisy Chaining

Also known as the train wreck, the act of having a middle layer which passes the responsibility of calling the method to something else. Here we can quickly loose track of our stack as the flow down into lower level modules gets branched into other modules.

### Inline Control
The idea that we can consolidate our control code into a single method. This is only possible in tasks due to the `async` language feature.

### Final Thoughts

In a game or other realtime application, my favorite solution is the queue. It is ideal for handling a *stream* of messages in a stateless way.

For transient transactions, my favorite solution is the Task. It may not be stateless, but it is great at consolidating the transactions control logic into a single location.

The adapter pattern is a great standardizing complex services with many results.

Callbacks and events are the worst.