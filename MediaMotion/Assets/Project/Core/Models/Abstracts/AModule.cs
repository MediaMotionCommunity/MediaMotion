using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Abstract Module
	/// </summary>
	public abstract class AModule : IModule {
		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; protected set; }

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Name { get; protected set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		public string Scene { get; protected set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; protected set; }

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		public IContainer Container { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether [support reload].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support reload]; otherwise, <c>false</c>.
		/// </value>
		public bool SupportReload { get; protected set; }

		/// <summary>
		/// Gets a value indicating whether [support background].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support background]; otherwise, <c>false</c>.
		/// </value>
		public bool SupportBackground { get; protected set; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public IElement[] Parameters { get; protected set; }

		/// <summary>
		/// Configures the module.
		/// </summary>
		public abstract void Configure(IContainer container);

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void Load(IElement[] parameters) {
			this.Parameters = parameters;
		}

		/// <summary>
		/// Reloads the specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <exception cref="System.NotSupportedException"></exception>
		public void Reload(IElement[] parameters) {
			throw new System.NotSupportedException("This module does not support the reload method");
		}

		/// <summary>
		/// Load another module.
		/// </summary>
		/// <returns>
		/// The parameters to restore
		/// </returns>
		public virtual IElement[] Sleep() {
			return (this.Parameters);
		}

		/// <summary>
		/// Back to the module.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void WakeUp(IElement[] parameters) {
			this.Parameters = parameters;
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		public virtual void Unload() {
		}

		/// <summary>
		/// Supports the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the element is supported, <c>false</c> otherwise
		/// </returns>
		public virtual bool Supports(IElement element) {
			// if (this.Configuration != null && this.Configuration.ElementFactoryObserver != null) {
			// 	return (this.Configuration.ElementFactoryObserver.Supports(element.GetPath()));
			// }
			return (false);
		}
	}
}
