\> [Documentation](../index.md) \> [Coding Convention](index.md)

----------

Global Coding Convention
========================

The whole project respect the C# convention ([https://msdn.microsoft.com/en-us/library/ff926074.aspx](https://msdn.microsoft.com/en-us/library/ff926074.aspx)) except for the point below.

Layout Conventions
------------------
 1. Identation in the beginning of the line made using tabulation ('\t' ASCII character).
 2. The opening parenthesis must not be on a new line, in a if then else statement the closing parenthesis must be in the same line as the next statement as shown below
```csharp
namespace MediaMotion.Module.Greece {
	public class ThermopylaeBattle {
		public void DoBattle() {
			try {
				if (this.IsSparta) {
					// Do something.
				} else if (Xerses.IsGod) {
					// Do something else.
				} else {
					// Do whatever you want.
				}
			} catch (Exception e) {
				// Do something with the exception.
			}
		}
	}
}
```

Language Conventions
--------------------
 1. Do not use finally block in a try catch statement.
   * *Use the using statement instead.*
   * *This statement break the normal flow of the execution.*
 2. Do not use continue or break instruction except in a switch.
   * *Those instructions break the normal flow of the execution.*

Dependency Injection Guidelines
-------------------------------
 1. Do not use a service class type, use the appropriate interface instead.
   * *If a service is overload you piece of code does not have to be change if you use interface.*
 2. Avoid the use of the `Container` by yourself lets the `Resolver` do it for you.
   * *Use the container by yourself is painful and source of errors.*
   * *Everything you can do via the container can be done via the Dependency Injection.*
   * *If you have to resolve parameter in your method inject the Resolver and use it.*
 3. Do use the Dependency Injection to get parameter.
   * *Extension of the point 2. above*

----------

[:arrow_up_small: Coding Convention](index.md) --- [:arrow_forward: Core Coding Convention](core.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*