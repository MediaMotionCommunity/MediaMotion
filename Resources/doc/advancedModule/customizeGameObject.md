\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Core Interaction

----------

Customize Element GameObject
============================

What does it mean ?
-------------------
The customisation of the element GameObject is a process that allow a module to define its own GameObject that represent one of its file in the explorer module.


Why should I customize the Element GameObject ?
-----------------------------------------------
Changing the default GameObject allow you to modify the material which represent the element (a better icon than the default once ...) or the form of the object if you want something better (why not a preview of a 3D model file for example ?).


How can I customize the Element GameObject ?
--------------------------------------------
The `ElementController` script of the Explorer Module use services that are defined in other module to customize the Element GameObject. *__Warning: this feature can be disable if the Explorer Module is totaly overload.__*

*__In short:__ to customize an Element GameObject a Module just have to provide a Service that implement the `IElementDrawObserver` interface.*

How can I use this technique to do a preview of a file ?
--------------------------------------------------------
The `Draw` method must return a `GameObject` so you can add a script to that element before returning it. This script can use module's services to process a preview of the file and when it is ready replace the default image by the proceed one.

*__Notice:__ this technique is not actually used in the project, some issues could appear.*

Example
-------
*__ElementDrawObserver.cs__*
```csharp
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.ModuleName.Observers {
	public class ElementDrawObserver : IElementDrawObserver {
		public GameObject Draw(IElement element) {
			GameObject drewElement = GameObject.Instantiate(Resources.Load("File3D")) as GameObject;

			/* Set whatever you want such as the position, the scale, the rotation ... */
			drewElement.GetComponent<Renderer>().material = Resources.Load<Material>("Video");
			return (drewElement);
		}
	}
}
```

*__ModuleNameModule.cs__*
```csharp
public override void Configure(IContainer container) {
	IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();
	/* ... */

	/* Element Draw Observer registered as a single instance service */
	containerBuilderService.Register<ElementDrawObserver>().As<IElementDrawObserver>().SingleInstance = true;
	
	/* ... */
	this.Container = containerBuilderService.Build(container)(container);
}
```

----------

[:arrow_backward: Customize Element Entity](customizeEntity) --- [:arrow_up_small: Advanced Module](index.md)

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*