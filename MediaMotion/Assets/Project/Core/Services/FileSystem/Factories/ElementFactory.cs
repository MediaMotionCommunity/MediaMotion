using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Extensions;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	/// <summary>
	/// File Factory
	/// </summary>
	public class ElementFactory : IElementFactory {
		/// <summary>
		/// The module manager service
		/// </summary>
		private readonly IModuleManagerService moduleManagerService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactory"/> class.
		/// </summary>
		public ElementFactory(IModuleManagerService moduleManagerService) {
			this.moduleManagerService = moduleManagerService;
		}

		/// <summary>
		/// Creates the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The folder or <c>null</c> if path does not point to any element or the pointed is not a folder
		/// </returns>
		public IFolder CreateFolder(string path) {
			return (this.Create(path) as IFolder);
		}

		/// <summary>
		/// Creates the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The file or <c>null</c> if path does not point to any element or the pointed is not a file</returns>
		public IFile CreateFile(string path) {
			return (this.Create(path) as IFile);
		}

		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The element or <c>null</c> if path does not point to any element</returns>
		public IElement Create(string path) {
			if (Directory.Exists(path) || File.Exists(path)) {
				if (this.moduleManagerService != null) {
					IModule module = this.moduleManagerService.Supports(path);

					if (module != null && module.Container.Has<IElementFactoryObserver>()) {
						return (module.Container.Get<IElementFactoryObserver>().Create(path));
					}
				}
				if (File.GetAttributes(path).HasAttribute(FileAttributes.Directory)) {
					return (new Folder(new DirectoryInfo(path)));
				}
				return (new Regular(new FileInfo(path)));
			}
			return (null);
		}
	}
}
