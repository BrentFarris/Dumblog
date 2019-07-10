# Clean Code
One of the interesting things after coding for almost 10 years now is that clean code is nothing new, it is something that almost every developer I have worked with knows, but every day we fail to participate in creating and maintaining clean code. Of course if we have so much trouble just making clean code in the first place you could imagine that we never spend the time required to clean up our existing code. A fantastic book by Robert "Uncle Bob" Martin named "Clean Code" is where I re-opened my eyes to the practices I should be doing.

## Functions
One of the fastest ways to clean up a code base is to focus on your functions.

- Functions should do 1 thing and do 1 thing only
- Functions should be no more than 6-9 lines of code, the smaller the better
- Private function names should be long and descriptive
- Public function names should be short and direct
- Try/catch blocks should only have 1 line of code within them
- The arguments to a function should be as close to 0 as possible
- Do not pass null into a function
- Do not return null from a function, instead throw an exception
- Do not pass a boolean into a function, because then the function does more than 1 thing
- Big functions with many scoped variables probably means you have a class hidden within it and should be extracted out
- A function with a return type should not mutate data
- A function with a `void` return type should mutate data

## Naming
- Names should be obvious and tell the reader what is going on without the need for a comment
- If it is hard to come up with a name, try to write a comment describing what the function is doing and pick something from that, it might make it easier
- Variable names should be nouns unless it is a boolean
- Boolean variables should start with something like "is" or "has"
- Functions returning a boolean should be named like boolean variables, starting with something like "is" or "has"
- Function names should be verbs as they do some-sort of behavior

## S.O.L.I.D.
Solid is a mnemonic that helps us remember a few key principals of programming. You should always attempt to follow these principals at all times while developing a software application.

#### Single responsibility principal
Both objects and functions should have a single responsibility. The moment they have many responsibilities it probalby should be a new objct or method. Of course things that manage a compound idea, such as managers or controllers have one responsibility of managing the compound concept. Likewise, the various objects contained within it should have single responsibilities.

#### Open/Closed principal
Functions and classes are open for extension, but closed for modification. This means that you can create a child class or an overridding function, but you should not go into an existing method and change it's behavior. This is made a lot easier when you break down functions properly to do a single thing and do it only.

#### Liskov substitution principal
This is to do the proper substitution without breaking that substitution of objects. Interfaces are an example of this but there are things that logically make sense that break this principal. For example, logically a square is a rectangle, but by no means should we derive a square from a rectangle. A square doesn't have a width and height, it only has a size. In the same light, haveing a "rotate" method on a parent class "Shape" wouldn't make much sense because what would happen when you try to rotate a circle or sphere? There is no point to rotating that particular shape. There should not be special logic or type checking in the domain, or worse, the parent level of the appliction to check for these scenarios.

#### Interface segregation principal
When implementing an interface, if the child does not implement all features of that interface, then it is probably too large of an interface and should be broken down and possibly become inherited interfaces.

#### Dependency inversion principal
Your main should be "looking down" towards the interface layer of the application and the domain should be "looking up" to the interface layer of the application. In this way we won't end up with a main that starts from the very top level of your application and then goes down to the deepest, lowest level of your application. It is essentially haulted by the interface layer which removes a great deal of dependencies; the same for the inverse ("looking up" towards the interfaces from the domain).

## Comments
In most cases comments are not needed if the code is well written and readable.

## Switch statements


## Classes


