\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Core Interaction

----------

Overloading a Module
=================

Why should I overload a Module ?
--------------------------------
The module system is designed to be flexible and highly customizable, each module providing an appropriate environment to open a specific element. For some reason an existing module can already open the file or folder you want to handle (for example the ExplorerModule handles all folders but a GitModule will handle all folders with a ".git" sub-folder. To overcome this problem you have to overload the ExplorerModule).

How can I overload a Module ?
-----------------------------
This is in fact really simple, you just need to have a greater value in the `Priority` property than the module you want to overload. The `Supports` method will be called before and if your Module handles the element the other module will not be tested. For example if you want to overload the ExplorerModule (which has a `Priority` value of 0) you need to define your `Priority` property to 1 or more.

Example
-------
```csharp
using System.IO;
using MediaMotion.Core.Models.Abstracts;

namespace MediaMotion.Modules.Git {
	public sealed class GitModule : AModule {
		public GitModule() {
			// Use a greater value for Priority than the module you want to overload (here the ExplorerModule which has a Priority of 0)
			this.Priority = 100;
			this.Name = "Git browser";
			this.Scene = "Git";
		}

		// If this method return true the other module will not be tested and will not be used.
		public override bool Supports(string path) {
			// simple test to determine if it's a git directory
			return (Directory.Exists(path + "/.git"));
		}
	}
}
```

----------

[:arrow_backward: Auto-Loading](autoloading.md) --- [:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Customize Element Entity](customizeEntity.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*
