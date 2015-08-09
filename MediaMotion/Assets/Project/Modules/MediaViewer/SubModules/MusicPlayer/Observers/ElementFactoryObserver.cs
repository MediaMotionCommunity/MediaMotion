using System.IO;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Models;

namespace MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>The element</returns>
		public IElement Create(string path) {
			return (new Music(new FileInfo(path)));
		}
	}
}
