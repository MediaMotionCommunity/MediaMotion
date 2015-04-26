using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Abstract Module
	/// </summary>
	public abstract class AModule : IModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="AModule" /> class.
		/// </summary>
		protected AModule() {
			this.Configuration = new ModuleConfiguration();
			this.Configuration.Priority = 1;
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns></returns>
		public IModuleConfiguration Configuration { get; private set; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public IElement[] Parameters { get; private set; }

		/// <summary>
		/// Configures the module.
		/// </summary>
		public abstract void Configure();

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void Load(IElement[] parameters) {
			this.Parameters = parameters;
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
			if (this.Configuration != null && this.Configuration.ElementFactoryObserver != null) {
				return (this.Configuration.ElementFactoryObserver.Supports(element.GetPath()));
			}
			return (false);
		}
	}
}
