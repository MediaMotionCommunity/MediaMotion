\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Core Interaction

----------

Auto-Loading
============

What does it means ?
--------------------
The auto-loading is the process of loading directly a module without loading the Core (via the Loader scene). The module will automatically load a instance of the Core.

What do I need to use it ?
--------------------------
The auto-loading is supported by default by all module but all the controller of the module should be compatible. If a module is loaded directly, the `Parameters` property is null, scripts that used this property to do some stuff must check it and used a fallback value if it is not usable.

Example
-------
```csharp
using System.Linq;
using MediaMotion.Core.Models.Abstracts;

namespace MediaMotion.Modules.Explorer.Controllers {
	public class ExplorerController : AScript<ExplorerModule, ExplorerController> {
		public void Init(/* ... */) {
			if (this.module.Parameters == null || this.module.Parameters.Count(parameter => parameter is IFolder) == 0) {
				// If the value of Parameters is unusable, use the home folder as fallback
				this.OpenDirectory(this.fileSystemService.GetHomeFolder());
			} else {
				// Else use the value provide by the caller
				this.OpenDirectory(this.module.Parameters.FirstOrDefault(parameter => parameter is IFolder) as IFolder);
			}
		}
	}
}
```

----------

[:arrow_backward: Create/Overload services](createService.md) --- [:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Overload a Module](overloadModule.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*