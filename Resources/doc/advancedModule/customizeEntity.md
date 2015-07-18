\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Core Interaction

----------

Customize Element Entity
========================

What does it mean ?
-------------------
The customisation of the Element Entity is a process that allow a Module to interfer in the creation process of the Element Entity to personnalise the entity and add some information that can be useful for it.

Why should I customize the Element Entity ?
-------------------------------------------
The Element Entity that your module use are created by the `FileSystemService`, if the entity is customized more information can be store in the entity itself and the work in the module is easier.

The information can be used by the ExplorerModule to give some information on the type of the file to the user (via a popup on hover).

How can I customize the Element Entity ?
----------------------------------------
The `ElementFactory` have an observer that allow a piece of external code to interfer in the creation process and create its own Element (if the created element implement the IElement interface).

An existing observer is already used by this factory which look if a module have a service that can create a custom Element Entity and will use it if the `Supports` method of the module return true.

*__In short:__ to customize an Element Entity a Module just have to provide a Service that implement the `IElementFactoryObserver` interface.*

Example
-------
*__ElementFactoryObserver.cs__*
```csharp
using System.IO;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Modules.ModuleName.Observers {
	public class ElementFactoryObserver : IElementFactoryObserver {
		public IElement Create(string path) {
			return (new MyElementEntity(new FileInfo(path)));
		}
	}
}
```

*__ModuleNameModule.cs__*
```csharp
public override void Configure(IContainer container) {
	IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();
	/* ... */

	/* Element Factory Observer registered as a single instance service */
	containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;
	
	/* ... */
	this.Container = containerBuilderService.Build(container)(container);
}
```

----------

[:arrow_backward: Overload a Module](overloadModule.md) --- [:arrow_up_small: Advanced Module](index.md) --- [:arrow_forward: Customize Element GameObject](customizeGameObject.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*