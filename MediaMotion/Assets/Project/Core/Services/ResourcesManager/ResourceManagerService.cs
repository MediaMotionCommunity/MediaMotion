using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Services.ResourcesManager.Container;
using MediaMotion.Core.Services.ResourcesManager.Container.Interfaces;
using MediaMotion.Core.Services.ResourcesManager.Interfaces;

namespace MediaMotion.Core.Services.ResourcesManager {
	/// <summary>
	/// Resource manager service
	/// </summary>
	public class ResourceManagerService : IResourceManagerService {
		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <typeparam name="T">The type of the resource.</typeparam>
		/// <param name="resource">The resource.</param>
		/// <returns>
		/// The container
		/// </returns>
		public IResourceContainer<T> GetContainer<T>(string resource) where T : UnityEngine.Object {
			return (new ResourceContainer<T>(resource.Split(new char[] { ';' })));
		}
	}
}
