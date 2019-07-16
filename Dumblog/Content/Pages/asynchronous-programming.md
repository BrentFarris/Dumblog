# Asynchronous Programming
Nicholas Ventimiglia
___

Asynchronous programming is strategy of calling a method at an undetermined later time. 

Below is a comparison grid of various asynchronous strategies. When deciding grid values, I assumed the implementer is dumb and chose the worse possible scenario.


| Solution  |Stateless|Listeners|Mixing |Chained|Inline |
| --------- |-------------------------------------------|
| Callbacks | NO      | MANY    |YES | YES   | NO    |
| Adapter   | **YES** |**MONO** |YES| YES   | NO    |
| Events    | **YES** | MANY    |YES |**NO** | NO    |
| Queue     | **YES** | MANY*    |**NO** |**NO**| NO    |
| Tasks     | NO      |**MONO** |**NO** |**NO** |**YES**|


### Stateless
Is the method in a static location, or are we passing the method as an object (`Action`)? It is a best practice to have a static completion handle, and pass the *data* around as an object. 

By doing this we can have predictable logic flows, we are respecting the separation of our *instruction and data* stacks, and we can serialize the entire application state if needed.

### Listeners
Having multiple listeners to a single output introduces branching. While this can be a valid strategy for extensiblity, it often a result of being lazy and thinking out our flows. 
 
### Layer Mixing
Does the lower level service have *any* reference to the higher level consuming service? Does the lower level service call (back) into into a higher level service on completion?

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