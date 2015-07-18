\> [Documentation](../index.md) \> [Coding Convention](index.md)

----------

Core Coding Convention
======================

Guidelines
----------
All guideline defined in the previous part ([Global Coding Convention](global.md)) must be followed.

Language Conventions
--------------------
 1. Do not use implicit type (`var` keyword), prefer use explicit type, even in loop.
   * *The type of the object is clear and does not have to be deduce by the developer.*
   * *The comprehension of the code is easier.*
 2. Do not use short instantiation of array (`int[] array = {42};`) except for local variable.
   * *You can use this method for local variable if it is done one the same line as the declaration.*
   * *Do not use it in other context because the type of the array does not clearly appear.*
 3. Do not use initializer during an object creation process.
   * *For readability reason.*

----------

[:arrow_backward: Global Coding Convention](global.md) --- [:arrow_up_small: Coding Convention](index.md) --- [:arrow_forward: Lighten Coding Convention (for Pull Request)](lighten.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*