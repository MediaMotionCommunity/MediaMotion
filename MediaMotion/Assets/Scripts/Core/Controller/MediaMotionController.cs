using System;
using System.Collections.Generic;
//using log4net;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Models.Wrapper.Exceptions;
using MediaMotion.Core.Models.Wrapper.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Controllers {
	class MediaMotionController : MonoBehaviour {
		//private ILog Logger;
		private IFileSystem FileSystem;
		//private IWrapper Wrapper;
		private IModule Module;

		public MediaMotionController() {
			//this.Logger = LogManager.GetLogger("Core");
			this.FileSystem = new FileSystem();
			//this.Wrapper = null;
			this.Module = null;
		}

		public void Start() {
			this.LoadModule("MediaMotion.Core.Controllers.FolderContentController");
		}

		public void ProxyActionHandle(object Sender, ActionDetectedEventArgs Args) {
			if (this.Module != null) {
				// proxify
			}
		}

		public void LoadModule(string Name) {
			this.Module = new FolderContentController();
			this.Module.Load();
		}

		public void LoadWrapper(string Name) {
			//IWrapper Wrapper = null;

			// Load library
			throw new WrapperNotFoundException(Name);

			//try {
			//	IWrapper current = this.Wrapper;

			//	//this.Logger.Debug("Configuration of new Wrapper");
			//	Wrapper.Load();
			//	Wrapper.OnActionDetected += ProxyActionHandle;
			//	this.Wrapper = Wrapper;
			//	//this.Logger.Debug("New Wrapper successfully loaded");

			//	if (current != null) {
			//		//this.Logger.Debug("Unloading previous wrapper");
			//		current.OnActionDetected -= ProxyActionHandle;
			//		current.Unload();
			//		//this.Logger.Debug("Previous wrapper successfully unloaded");
			//	}
			//} catch (WrapperLoadException /*Exception*/) {
			//	//this.Logger.Error("Loading Wrapper, previous Wrapper always in use", Exception);
			//} catch (WrapperUnloadException /*Exception*/) {
			//	//this.Logger.Error("Unloading Wrapper, new Wrapper in use", Exception);
			//} catch (Exception /*Exception*/) {
			//	if (this.Wrapper != null) {
			//		try {
			//			this.Wrapper.OnActionDetected -= ProxyActionHandle;
			//			this.Wrapper.Unload();
			//		} catch (Exception) { }
			//	}
			//	this.Wrapper = null;
			//	//this.Logger.Fatal("Unknown error on Wrapper loading, no more Wrapper in use", Exception);
			//}
		}
	}
}
