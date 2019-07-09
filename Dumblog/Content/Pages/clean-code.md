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


## Comments


## Switch statements

