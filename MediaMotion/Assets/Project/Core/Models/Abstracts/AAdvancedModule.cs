using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Abstract Advanced Module
	/// </summary>
	public class AAdvancedModule : IAdvancedModule {
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; protected set; }

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		public IContainer Container { get; protected set; }

		/// <summary>
		/// Gets or sets the children.
		/// </summary>
		/// <value>
		/// The children.
		/// </value>
		public IModule[] Children { get; protected set; }

		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public virtual void Configure(IContainer container) {
			this.Container = container;
		}
	}
}
