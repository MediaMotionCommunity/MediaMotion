namespace MediaMotion.Core.Services.ResourcesManager.Container.Interfaces {
	/// <summary>
	/// Resource container Interface
	/// </summary>
	public interface IResourceContainer<T> {
		/// <summary>
		/// Gets the specified name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The resource</returns>
		T Get(string name = null);
	}
}
