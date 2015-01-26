using MediaMotion.Core.Models.Service;

namespace MediaMotion.Core.Models.Core {
	/// <summary>
	/// Core Interface
	/// </summary>
	public interface ICore {
		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Namespace">The namespace.</param>
		/// <returns>The service</returns>
		ServiceBase GetService(string Name, string Namespace = null);
	}
}
