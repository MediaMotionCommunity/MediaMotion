using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;

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
			this.moduleManagerService.LoadModule(new IElement[] { this.fileSystemService.GetHomeFolder() });
		}
	}
}
