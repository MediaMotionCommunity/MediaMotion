\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Creation and Configuration

----------

Enable a Module in the Core
===========================

When the new module is created (see [Create a new Module](newModule.md)) it needs to be enabled in the Core.
A module is enabled if you register it in the IModuleManagerService, the best way to do it is in the method RegisterModules of the class [MediaMotion.Core.MediaMotionCore](http://www.google.com/).

```csharp
namespace MediaMotion.Core {
	public static class MediaMotionCore {
		/* ... */
		private static void RegisterModules() {
			IModuleManagerService moduleManager = Container.Get<IModuleManagerService>();

			moduleManager.Register<ExplorerModule>();
			/* ... */
			
			/* Add your modules below */
			moduleManager.Register<MediaMotion.Modules.ModuleName.ModuleNameModule>();
		}
		/* ... */
	}
}
```

----------

[:arrow_backward: Create a new Module](newModule.md) --- [:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Configuration](configure.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*