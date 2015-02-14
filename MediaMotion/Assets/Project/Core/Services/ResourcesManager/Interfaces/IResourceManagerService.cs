using MediaMotion.Core.Services.ResourcesManager.Container.Interfaces;

namespace MediaMotion.Core.Services.ResourcesManager.Interfaces {
	/// <summary>
	/// Resource manager Service Interface
	/// </summary>
	public interface IResourceManagerService {
		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="resource">The resource.</param>
		/// <returns>The container</returns>
		IResourceContainer<T> GetContainer<T>(string resource) where T : UnityEngine.Object;
	}
}
