using System.Collections.Generic;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.Explorer;
using UnityEngine;

namespace MediaMotion.Core.Controllers.Loader {
	/// <summary>
	/// Loading text
	/// </summary>
	public class LoadingText : BaseUnityScript<LoadingText> {
		/// <summary>
		/// The module manager
		/// </summary>
		private IModuleManagerService moduleManagerService;

		/// <summary>
		/// The file system service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// Initializes the specified module manager.
		/// </summary>
		/// <param name="moduleManager">The module manager.</param>
		public void Init(IFileSystemService fileSystemService, IModuleManagerService moduleManager) {
			this.fileSystemService = fileSystemService;
			this.moduleManagerService = moduleManager;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.moduleManagerService.LoadModule(new IElement[] { this.fileSystemService.GetHomeFolder() });
		}
	}
}
