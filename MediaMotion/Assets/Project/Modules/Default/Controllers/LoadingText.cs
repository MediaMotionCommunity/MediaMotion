using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Modules.Default.Controllers {
	/// <summary>
	/// Loading text
	/// </summary>
	public class LoadingText : AScript<LoadingText> {
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
		/// <param name="fileSystemService">The file system service.</param>
		/// <param name="moduleManagerService">The module manager service.</param>
		public void Init(IFileSystemService fileSystemService, IModuleManagerService moduleManagerService) {
			this.fileSystemService = fileSystemService;
			this.moduleManagerService = moduleManagerService;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			Debug.Log(this.fileSystemService);
			Debug.Log(this.moduleManagerService);
			this.moduleManagerService.Load(new IElement[] { this.fileSystemService.GetHomeFolder() });
		}
	}
}
