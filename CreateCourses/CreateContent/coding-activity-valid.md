```c-lms
activity-type: coding
compiler: csharp
topic: First Exercise
```

## Exercise One
  
Complete the function named "SayHello" inside the Greeting class to print "Hello World!" to the console.

```c-lms
file-name: greeting.cs
file-language: csharp
```
namespace CC.LMS.Exercise1
{
    using System;
    public class Greeting
    {
        public void SayHello()
        {
#
#
#            
        }
    }
}

```c-lms
test-id: HelloWorldSyntax
test-type: syntax
test-file: greeting.cs 
``
namespace CC.LMS.Exercise1
{
    using System;
    public class Greeting
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");          
        }
    }
}

```c-lms 
test: HelloWorldSyntax
test-method: ^.*[Cc]onsole.[Ww]rite[Ll]ine.*$
test-passed: false
```
Double check your capitalziation, it matters a lot!
```c-lms 
test: HelloWorldSyntax
test-method: ^.*cout.*$
test-passed: false
```
CSharp is different than C++, cout is not a real thing, we use Console.WriteLine instead.
```c-lms 
test: HelloWorldSyntax
test-method: hash-match
test-passed: true
```
Grats, you got it exactly right!
```c-lms 
test: HelloWorldSyntax
test-method: ^.*Console.WriteLine.*$
test-passed: true
```
Good use of Console.WriteLine

```c-lms 
test-id: HelloWorldOutput
test-type: unit
file-name: program.cs
file-language: csharp
```
namespace CC.LMS.Exercise1
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var quickGreeting = new Greeting();
            quickGreeting.SayHello();
        }
    }
}

```c-lms 
test: HelloWorldOutput
test-stream: stdout
test-method: ^[Hh]ello World\n$
test-passed: false
```
Close, double check the instructions.  Programing is all about details, check your casing? Did you include a captial H? Does your output
end in an exclamation point? 

```c-lms 
test: HelloWorldOutput
test-stream: compiler
test-method: ^.*CS1002.*$
test-passed: false
```
Ouch, looks like your program failed to compile.  Double check that each line of code ends with a semi-colon.
```c-lms 
test: HelloWorldOutput
test-stream: stdout
test-method: ^Hello World\n$
test-passed: true
```
Perfect! You wrote the code that produced the correct answer. Continue on to the next question.
