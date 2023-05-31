using Unit_Testing_Code.Classes;

/*
Unit testing, day 2:
Test-Driven Development (TDD)
Yesterday:
Writing code, and then developing the unit tests afterwards
- Had to think about our code after it was written, and try to conjure every way that it could break

Today: opposite
Start with the unit test, and then write the code afterwards. 
5-step process

1. New requirements dictate the creation of new unit tests
2. Run the unit tests - to make *sure* that they fail - make sure that they work
3. Write the simplest code that can satisfy the test requirements
    3a: this includes things like hard-coded answers, and any other shortcuts you think will get it working
    This helps excluded any extra "dead weight" code
4. Run the unit tests again
    4a: If the test fails, go back to step 3
5. Refactor your code, and repeat steps 3, 4, and 5 as needed

Name: Red-Green-Refactor
*/

Console.WriteLine("Something");
