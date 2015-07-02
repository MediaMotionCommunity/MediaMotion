Use a global (Core) Service
===========================

The best option to use a global service in a script is to use the Dependency Injection. An abstract script AScript, which used the [CRTP (Curiously Recurring Template Pattern)](https://en.wikipedia.org/wiki/Curiously_recurring_template_pattern), make this step easier.

If an `void Init()` method exist the script retrieve all the parameter of it and try to resolve them. If the resolve step succeed the Init method is called with all the service injected in its parameters.

The AScript class need two information to correctly do is job. The first one is the class description of the module that the script belongs to. The second one is the class witch contains the Init method (the class itself).

*__Notice:__ The AScript class inherit the MonoBehaviour class, all the action that can be done in a MonoBehaviour based class can also be done is a AScript based class.*

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