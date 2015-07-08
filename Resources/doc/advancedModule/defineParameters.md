\> [Documentation](../index.md) \> [Advanced Module](index.md) \> Service Creation and Overload

----------

Define its own parameters
=========================

A parameter is a value that can be injected via the Dependency Injection. Contrary to a service a parameter can be anything that stores a value (such as int, string, object...) and has a name.

The name of the parameter is really important because it is the only unique information we have, contrary to a service which the type of the interface is unique.

To define/override a parameter you should use the `void Configure(IContainer container)` method. This method is used to configure the service container for the module (add new service, overload existing one, defining parameters...).

*__Notice:__ the container is a readonly class, in order to define new parameters you should use `ContainerBuilderService` (you can retrieve it using the container pass in the parameter).*

  1. Retrieve an instance of ContainerBuilder to create a new container
  2. Add as many parameters as you want using the method `void Define(string parameter, object value)`
  3. Build the container using the method `IContainer Build(IContainer parent = null)`. The parameter is used to keep the existing services and parameter if they are not overridden.
  4. Store the result of the Build method in the `Container` attribute

*__Notice:__ for security reasons the global service and parameter can only be modified in the Core, so a parameter defines/overrides in a module exists/is modified only in the module.*

Example:
--------
```csharp
public override void Configure(IContainer container) {
	// Retrieve the ContainerBuilder
	IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();

	// Define your parameters
	containerBuilderService.Define("My Parameter", "My Value");
	containerBuilderService.Define("My Version", new Version(1, 2, 3, 4));
	
	// Build and store the new Container
	this.Container = containerBuilderService.Build(container);
}
```

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*

