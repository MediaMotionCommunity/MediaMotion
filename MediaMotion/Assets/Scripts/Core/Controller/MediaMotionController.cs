using System;
using System.Collections.Generic;
using MediaMotion.Core.Exceptions;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Models.Wrapper.Exceptions;
using MediaMotion.Core.Models.Wrapper.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Logger.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Controllers {
	class MediaMotionController : MonoBehaviour {
		private ILogger Logger;
		private IFileSystem FileSystem;
		private IWrapper Wrapper;
		private IModule Module;

		public MediaMotionController() {
			this.FileSystem = new FileSystem();
			this.Wrapper = null;
			this.Module = null;
		}

		public void ProxyActionHandle(object Sender, ActionDetectedEventArgs Args) {
			if (this.Module != null) {
				// proxify
			}
		}

		public void LoadModule(string Name) {
			IModule Module = null;

			throw new ModuleNotFoundException(Name);
		}

		public void LoadWrapper(string Name) {
			IWrapper Wrapper = null;

			// Load library
			throw new WrapperNotFoundException(Name);

			try {
				IWrapper current = this.Wrapper;

				Wrapper.Load();
				Wrapper.OnActionDetected += ProxyActionHandle;
				this.Wrapper = Wrapper;

				if (current != null) {
					current.OnActionDetected -= ProxyActionHandle;
					current.Unload();
				}
			} catch (WrapperLoadException Exception) {
				System.Diagnostics.Debug.WriteLine("Error while loading Wrapper, previous Wrapper always in use: " + Exception.ToString());
			} catch (WrapperUnloadException Exception) {
				System.Diagnostics.Debug.WriteLine("Error while unloading Wrapper, new Wrapper in use: " + Exception.ToString());
			} catch (Exception Exception) {
				if (this.Wrapper != null) {
					try {
						this.Wrapper.OnActionDetected -= ProxyActionHandle;
						this.Wrapper.Unload();
					} catch (Exception) { }
				}
				this.Wrapper = null;
				System.Diagnostics.Debug.WriteLine("Unexpected exception on load Wrapper, no more Wrapper in use: " + Exception.ToString());
			}
		}
	}
}
