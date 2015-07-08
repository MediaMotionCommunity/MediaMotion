\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Creation and Configuration

----------

Create a new Module
===================

Create the file architecture
------------------------------
In order to maintain a homogeneous architecture, all modules MUST respect the file architecture described below.

	ModuleName/
	    Controllers/
	    Observers/ (only if you have Observers)
	    Prefabs/ (only if you have Prefabs)
	    Resources/
	    Scenes/
	    Services/ (only if you have Services)

Create the Unity scene
----------------------
Every module must provide a Unity scene.
See the [documentation of Unity](http://docs.unity3d.com/Manual/CreatingScenes.html) for more details.

Create mandatory class
----------------------
Create a class with the name of the module and ending in Module (i.e.: Module name is Default, the name of the class is DefaultModule in the file DefaultModule.cs) in the ModuleName folder.

	ModuleName/
		ModuleNameModule.cs
		Controllers/
		...

This class must implement the [IModule](http://www.google.com/) interface (the abstract class [AModule](http://www.google.com/) can be used instead).
Only the [IModule#Scene](http://www.google.com/) must be set. All other value can be set to their default value if the Module does not need them.

A simple example of the ModuleNameModule.cs file:

```csharp
using MediaMotion.Core.Models.Abstract.AModule;

namespace MediaMotion.Modules.ModuleName {
	public class ModuleNameModule : AModule {
		public ModuleNameModule() {
			this.Scene = "NameOfTheModuleScene";
		}
	}
}
```

[:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Enable a Module in the Core](enableModule.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*