// This file provides some guidance through the problem:
// each section is numbered, and 
// solves one piece you will need. Sections contain
// general instructions, 
// [ YOUR CODE GOES HERE! ] tags where you should
// make the magic happen, and
// <F# QUICK-STARTER> blocks. These are small
// F# tutorials illustrating aspects of the
// syntax which could come in handy. Run them,
// see what happens, and tweak them to fit your goals!

// 0. GETTING READY

// <F# QUICK-STARTER> 
// With F# Script files (.fsx) and F# Interactive,
// you can "live code" and see what happens.

// Try typing let i = 1337 in the script file, 
// right-click and select "Execute in interactive" (Visual Studio) or ALT+ENTER (Visual Studio Code).

// HINT: while you can write directly into FSI (F# Interactive), you need 
// to end each statement with ";;" for it to be executed and you have to watch out 
// for the correct indentation. Hence it is much easier to write your code
// in a script file like this and "send" it to interactive for execution.

// let...

// let "binds" the value on the right to a name.

// let binds to a value are immutable, if you bind 42 to x:
let x = 42
// you cannot change this value.
// x = 43 will give you a compile error
// You *can* make a value mutable but you have to be explicit about it:
let mutable y = 42
y <- 43

// Just as you have bound the value of 42 to the name "x" you can
// bind any other expression to a name.
// Execute the following lines in FSI (highlight both
// lines and execute them "together"):
let greet name = 
    printfn "Hello, %s" name

// let also binds a name to a function.
// greet is a function with one argument, name.
// You should be able to call this function by entering
// the following and sending it to FSI:

// greet "World"

// Indentations
// As you might have noticed, F# relies on levels of indentation
// to scope the code. Whenever you would open a curly brace
// in C# you should just add a level of indentation in F#
// If your function is more than one line long, you HAVE TO 
// use a line break after "=" on the first line (see "greet").

// </F# QUICK-STARTER> 

// A data file is included in the same place you
// found this script: 
// shoppingcartpositions.csv, a file that contains 100 
// examples of a typical ecommerce shopping cart items

 
// 1. GETTING SOME DATA
 
// First let's read the contents of "shoppingcartpositions.csv"

// We will need System and System.IO to work with files,
// let's right-click / run in interactive, 
// to have these namespaces loaded:
  
open System
open System.IO

// the following might come in handy: 
// File.ReadAllLines(path)
// returns an array of strings for each line 

// [ YOUR CODE GOES HERE! ]




// 2. CLEANING UP HEADERS
 
// Did you note that the file has headers? We want to get rid of it.

// <F# QUICK-STARTER>  
// Array slicing quick starter:
// Let's start with an Array of ints:
let someNumbers = [| 0 .. 10 |] // create an array from 0 to 10
// Array literals can be created with [| opening and |], if you
// omit the | symbol and use only [ ] you are creating a list (linked list).
// You can access Array elements by index:
let first = someNumbers.[0] 
// You can also slice the array:
let twoToFive = someNumbers.[ 1 .. 4 ] // grab a slice
let upToThree = someNumbers.[ .. 2 ] 
// </F# QUICK-STARTER> 

// [ YOUR CODE GOES HERE! ]




// 3. EXTRACTING COLUMNS
 
// Break each line of the file into an array of string,
// separating by commas, using Array.map

// <F# QUICK-STARTER> 
// Array.map quick-starter:
// Array.map takes an array, and transforms it
// into another array by applying a function to it.
// Example: starting from an array of strings:
let strings = [| "Learning"; "F#"; "is"; "fun" |]

// We can transform it into a new array,
// containing the length of each string:
let lengths = Array.map (fun (s:string) -> s.Length) strings
// The exact same operation above can be 
// done using the forward pipe operator, 
// which makes it look nicer:
let lengths2 = strings |> Array.map (fun s -> s.Length)

// The "fun" part of the above declaration is how you define lambda
// functions in F#. It supports automatic destructuring i.e. if an input 
// is a tuple, "fun a, b ->" makes the tuple elemts available inside the lambda

// </F# QUICK-STARTER> 

 
// The following function might help
let csvToSplit = "1,2,3,4,5"
let splitResult = csvToSplit.Split(',')
 
 
// [ YOUR CODE GOES HERE! ]



 
// 4. MODELING WITH TYPES

// Now that we have an array containing arrays of strings,
// and the headers are gone, we need to transform it into
// a more useful form. Let's create a type that will hold
// the information for each shopping cart item.

// <F# QUICK-STARTER>  
// Record quick starter: we can declare a 
// Record (a lightweight, immutable class) type that way:
type Example = { Label:int; Description:string; Id:int }
// and instantiate one this way:
let example = { Label = 1; Description = "Much wow"; Id = 1 }
// you can nest records within each other
type MyExample = { Name:string; Examples:Example[] }
let myExample = { Name = "yo"; Examples = [| example |]}
// Type conversion starter:
// You can use your familiar dotnet apis to convert between basic types:
// For ints:
let castedInt = (int)"42"
// or, alternatively:
let convertedInt = Convert.ToInt32("42")

// the same goes for other basic types (decimal, float etc):
let convertedDecimal = Convert.ToDecimal("43.2")
// </F# QUICK-STARTER>

// Now create record that will hold the raw shopping cart item
// information and convert the input data to use your new type.
// Then create a type that represents a shopping cart for a single customer.
// It should contain all the items of one customer.
// You can explore the functions in collection Modules like "Array" to
// discover what kind of operations (e.g. fold, groupBy etc) are supported by standard library.

// [ YOUR CODE GOES HERE! ]





// 5. VALIDATION

// Let's make sure all our shopping cart items are valid.
// In our case it means every item fulfills the two following criteria:
// - Amount is greater than 0
// - Price is non-negative
// For each of those criteria write a validation function that takes a shopping cart item
// and returns a boolean indicating if an item is valid.

// [ YOUR CODE GOES HERE! ]




// Now use these functions to filter out the shopping carts containing
// invalid entries.
// Array functions such as Array.filter and Array.exists may come in handy.

// [ YOUR CODE GOES HERE! ]




// 6. ORDER CREATION

// Finally, let's create some orders out of those shopping carts!

// Design one or more types that will represent an Order.
// The following information should be included:
// - CustomerId
// - Order date
// - a collection of items that represent EITHER:
// -- an item ordered
// -- a special discount the customer has been given
// A "normal" item consists of a name, count of items and a unit-price (positive)
// A "discount" item consists of a name and a total money amount to be discounted (this amount is positive)

// <F# QUICK-STARTER>
// Every time you hear the word "either" in some kind of modeling context
// it is a strong indicator you should consider using
// a so called union- or sum-type (aka discriminated union)
// You can think of them as a kind of "enum on steroids" and if you don't define values
// to individual elemnts they behave just like plain old enums:
type Color =
    | Red
    | Green
    | Blue

// but they are really powerful when they also include data with them:
type ColorSystem =
    | RGB of red:int * green:int * blue:int
    | CMYK of cyan:int * magenta:int * yellow:int * black:int

// With "int * int * int" we have defined a tuple with 3 int values.
// This is nice for simple or ad-hoc data types, but for a more self documenting code
// you can use record types:
type HSL = { hue: int; saturation: float; lightness: float }
type ColorSystemExtended =
    | RGB of red:int * green:int * blue:int
    | CMYK of cyan:int * magenta:int * yellow:int * black:int
    | HSL of HSL

// <BORING DETAILS>
// It is part of the so-called "algebraic type system" of F# which includes
// sum-types and product-types.
// A typical product-types are tuples or records. As the name suggests, a product-type's
// number of possible values represent a product of all possible values 
// e.g. the number of possible values of a tuple of type (int * bool) is 4294967296 * 2
// In contrast to that, sum types derive their total amount of possible
// values from the sum of total possible values of each individual alternative
// e.g. the number of possible values of a following sum type:
// type IntOrBool =
//     | Integer of int
//     | Boolean of bool
// is 4294967296 + 2
// </BORING DETAILS>

// To instatiate a union type, you need to use one of its constuctors along 
// with its value:
let blue = RGB (red=0, green=0, blue=255) // names are optional
let alsoBlue = HSL { hue=240; saturation=100.0; lightness=50.0 }

// The most common way to consume these types is via pattern matching:
let doSomthingColorful palette =
    match (palette) with
    | RGB (r,g,b) -> (* do something rgb-ish *) ignore
    | CMYK(c,m,y,k) -> (* do something cmyk-ish*) ignore
    | HSL h -> (* access values via dot notation: h.hue *) ignore

// </F# QUICK-STARTER>


// now design your order type and create orders out of shopping carts!
// [ YOUR CODE GOES HERE! ]



// Now, calculate the price of each order and modify those that cost more than 1000 
// with a discount position of 20 and calculate sums again

// <F# QUICK-STARTER>
// As you may noticed, records are immutable data types. It is not possible
// to change a value in a record, you have to create a new record with all
// values being the same except for those you want to modify.
// This approach has many benefits e.g. when it comes to concurrency, but
// it obviously makes it harded in cases when you want to modify some data.
// Fortunately F# has special syntax for creating records from other records.
// Let's define a record and get an instance of it:
type MyRecord = {Some:string; Test:string; Data:int}
let myRecord = { Some = "Some"; Test = "Test"; Data = 42 }
// To get a copy of that record but with the "Test" field value changed to "Blah" use the following syntax:
let myNewRecord = { myRecord with Test = "Blah" }
// </F# QUICK-STARTER>


// [ YOUR CODE GOES HERE! ]