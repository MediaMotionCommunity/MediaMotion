using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;

namespace MediaMotion.Core.Models.Module.Abstracts {
	/// <summary>
	/// Abstract Module
	/// </summary>
	public abstract class AModule : IModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="AModule" /> class.
		/// </summary>
		public AModule() {
			this.Configuration = new Configuration();
		}

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <returns></returns>
		public Configuration Configuration { get; private set; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public IElement[] Parameters { get; private set; }

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public virtual void Load(IElement[] parameters = null) {
			this.Parameters = parameters;
		}

		/// <summary>
		/// Configures the module.
		/// </summary>
		public abstract void Configure();

		/// <summary>
		/// Load another module.
		/// </summary>
		public virtual void Sleep() {
		}

		/// <summary>
		/// Back to the module.
		/// </summary>
		public virtual void WakeUp() {
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		public virtual void Unload() {
		}
	}
}
