Create/Overload services
========================

To create a service, a single class is enough. Their is no other requirements.

To register/overload a service you should use the `void Configure(IContainer container)` method. This method is used to configure the service container for the module (add new service, overload existing one, defining parameters...).

*__Notice:__ the container is a readonly class, in order to define new parameter we should use the `ContainerBuilderService` (you can retrieve it using the container pass in the parameter).*

  1. Retrieve an instance of ContainerBuilder to create a new container
  2. Add as many parameters as needed using the method `IDefinition Register<Service>(Service instance = null) where Service : class`
    * The result is a `IDefinition`, this class have two main elements:
      1. The `IDefinition As<Service>() where Service : class` method that define the alias of the service. *__Notice:__ the method also exist in non-generic using the `System.Type`.
      2. The `bool SingleInstance { get; set; }` property which define if the service should be create only one time or should create a new instance for each injection.
    * The `instance` parameter is used to define the instance of the service that will be return during the resolution process. If this parameter is not null the `SingleInstance` must be set to `true`.
  3. Build the container using the method `IContainer Build(IContainer parent = null)`. The parameter is used to keep the existing services and parameter if they are not override.
  4. Store the result of the build method in the `Container` property.

*__Notice:__ for security reason the global service and parameter can only be modified in the Core, so a service create/overload in a module exists/is modified only in the module.*

Example:
--------
	public override void Configure(IContainer container) {
		// Retrieve the ContainerBuilder
		IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();

		// Define your parameters
		containerBuilderService.Define("My Parameter", "My Value");
		containerBuilderService.Define("My Version", new Version(1, 2, 3, 4));
		
		// Build and store the new Container
		this.Container = containerBuilderService.Build(container);
	}

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*

