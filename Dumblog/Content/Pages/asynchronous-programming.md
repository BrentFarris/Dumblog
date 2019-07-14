# Asynchronous Programming
___

Asynchronous programming is strategy of calling a method at a undetermined later time. Generally this method set at runtime, and because of this we introduce *race conditions*.


| Solution  |Stateless|Listeners|Mixing |Chained|Inline |
| --------- |-------------------------------------------|
| Callbacks | NO      | MANY    |**NO** | YES   | NO    |
| Adapter   | **YES** |**MONO** |YES| YES   | NO    |
| Events    | **YES** | MANY    |**NO** |**NO** | NO    |
| Queue     | **YES** | MANY*    |**NO** |**NO**| NO    |
| Tasks     | NO      |**MONO** |**NO** |**NO** |**YES**|


### Stateless
Is the method in a static location, or are we passing the method as an object? It is a best practice to have the method in a constant location, and pass the *data* around as an object. By doing this we can have predictable logic flows, we are respecting the separation of our *instruction and data* stacks, and we can serialize the entire application state if needed.

### Listeners
Having multiple listeners to a single output introduces branching. While this can be a valid strategy for extensiblity, it often a result of not thinking out the complete systems flow. 
 

### Layer Mixing
The act of a lower level service (data) calling into a higher level service (view). Now the data layer needs to know about view specific cases so that it can call the correct method on completion. 

### Daisy Chaining
Passes the responsibility of completion to something else. Here we can quickly loose our stack as we follow the stack into lower level modules.

### Inline Control
The idea that we can consolidate our control code into a single method. This is only possible in tasks due to the `async` language feature which allows us to `await` inline.

### Final Thoughts
In a realtime application, my favorite solution is the `Queue`. It is ideal for handling a *stream* of messages in a stateless way.

For transient transactions, my favorite solution is the `Task`. It may not be stateless, but it is great at consolidating the transactions control logic into a single location.

The `Adapter pattern` is a great standardizing complex services with many results.

`Events` have a place, but are very much abused.

`Callbacks` are the worst.