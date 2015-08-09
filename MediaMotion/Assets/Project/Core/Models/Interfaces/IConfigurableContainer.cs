using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Models.Interfaces {
	/// <summary>
	/// Configurable Container interface
	/// </summary>
	public interface IConfigurableContainer {
		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		IContainer Container { get; }

		/// <summary>
		/// Configures the module.
		/// </summary>
		/// <param name="container">The container.</param>
		void Configure(IContainer container);
	}
}
