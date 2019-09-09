# What I Love About C
Brent Farris
___

C is seen as an archaic language by many programmers now days and in many cases people will look at you like you are insane for using such a language to get anything done. Many people forget that some of the most complex software ever created was written in C and that most of their programming languages have been written in C. The biggest sin in most people's eyes is that it is not natively an object-oriented language. Though you could make it work like one, I would suggest that you try to avoid this, if you want C and objects maybe move onto C++.

## Functions signatures in C
One of the things that I love about C is that I find it hard to over-engineer things, the lack of the object[^1] makes functions almost completely pure in that they take in some input and produce an output. In some common scenarios a C function will alter some bytes of an input argument. One might think that it is scary that an input could be altered by a function without the caller knowing, but in any good library an all inputs marked as `const` will let the caller know that their input will not be altered by the function. The lack of the `const` keyword tells the called that the function intends to alter the input argument. With these basic principals followed all functions in C are deterministic, easily testable, and clearly defined. This is what makes a good library and it is something that a lot of higher level languages fail to achieve.

Objects as they are known in Computer Science are not evil by any means, however their existence complicates the deterministic nature of functions, as well as the  testability and the behavior of them. Many high level object oriented languages do not support the `const` concept. This means objects can be manipulated by a function they are passed into, or other functions called within it without notice to the caller. This behavior creates a complication with libraries where the caller is required to trust the function's behavior.

Take the following function signature for a C function:
```
void render_image(RenderTarget* target, const Image* image);
```

Now take a look at a similar function in a language such as C#:
```
void RenderImage(RenderTarget target, Image image) { /*...*/ }
```

In the C `render_image` function you know that nothing in the `image` is going to change when it is called. You also know immediately that the `target` is going to change when this function is called (most likely the pixel colors or something like that will change).

Now if you examine the signature for the C# `RenderImage` function, you have no idea if the `target` is changing or if the `image` is changing. For all we know this function could take our image and invert the colors and change the size of it, we just have to trust it will do the right thing that the name says it will.

## Myth on code size
One of the common things that people claim is that I will wind up writing more code in C than I would in an object-oriented language. Though that might be true in some scenarios, people seem to be stuck in the "object" mindset and thinking that everything has to fit within that object-oriented box. I find it funny that people find it so hard to think in the terms of the strengths of C which is a stateful functional language. In many ways the C engine I've written so far is the smallest code-base engine I've written. When you can think of things in simple terms and in terms of inputs/outputs you will find ways to reduce code size and still produce premium, extensible code. The thing I like to think of often is the standard library, when you look at the concept of a function like `atan2` or `abs` you realize that it sometimes is better to have smaller building blocks that make up the whole rather than massive systems that are intertwined.

## Use primitives to create more complex tools
I love geometry, if I asked you to find the exact center of a line segment, how would you do this? There's a catch, I'm not going to tell you the length of the line segment, the only things you are allowed to know is that circles exist and what a point is. Some of you may immediately answer that having two circles who's center is at the ends of the segment and intersect each other you could draw a line between the intersection points peak and valley to bisect a line segment, and in doing so, find the exact center of the line segment. By using a primitive box of tools such as a line and a circle you can create a limitless amount of concepts and other useful tools. In this way you should try to keep your C code as primitive and simple as possible so that the combination of the primitives can create complex concepts easily. This is something that is easily achievable because of the limitations of the language which often make you think outside of the box.

![center-of-line-segment-using-circles][2]{style=max-width:50%;text-align:center;}

[^1]: A very popular paradigm in computer science where a strictly defined structure of data is contained 
[2]: https://raw.githubusercontent.com/BrentFarris/Dumblog/master/Dumblog/Content/Images/center-of-line-segment-using-circles.gif