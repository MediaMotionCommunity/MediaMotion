Use a global (Core) Service
===========================

The best option to use a global service in a script is to use the Dependency Injection. An abstract script AScript, which used the [CRTP (Curiously Recurring Template Pattern)](https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern), makes this step easier.

If a `void Init()` method exists the script retrieves all the parameters of it and tries to resolve them. If the resolve step succeeds the Init method is called with all the services injected in its parameters.

The AScript class needs two pieces of information to correctly do its job. The first one is the class description of the module that the script belongs to. The second one is the class witch contains the Init method (the class itself).

*__Notice:__ The AScript class inherits the MonoBehaviour class, any action that can be done in a MonoBehaviour based class can also be done is a AScript based class.*

Example
-------
    using MediaMotion.Core.Models.Abstracts;
    using MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes;
    
    namespace MediaMotion.Modules.ModuleName {
	    public class MyScript : AScript<ModuleNameModule, MyScript> {
		    public void Init(IFileSystemService fileSystemService, [Parameter("Version")] Version version, ...) {
			    // Do some stuff, store the services and/or parameters to use it later...
		    }
		    
		    public void Update() {
			    // Do some stuff
		    }
	    }
    }

----------
*__Notice:__ The documentation above is available offline in [PDF format](../doc.pdf).*