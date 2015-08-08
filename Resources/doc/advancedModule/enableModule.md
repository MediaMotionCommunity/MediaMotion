Enable a Module in the Core
===========================

When the new module is created (see [Create a new Module](https://github.com/MediaMotionCommunity/MediaMotion/blob/master/Resources/doc/advancedModule/newModule.md)) it needs to be enable in the Core.
A module is enabled if you register it in the IModuleManagerService, the better way to do it is in the method RegisterModules of the class [MediaMotion.Core.MediaMotionCore](http://www.google.com/).

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

----------
*__Notice:__ The documentation above is available offline in [PDF format](https://github.com/MediaMotionCommunity/MediaMotion/blob/master/Resources/doc/doc.pdf).*