using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.ModuleManager.Models {
	/// <summary>
	/// Module instance model
	/// </summary>
	public class ModuleInstance {
		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleInstance"/> class.
		/// </summary>
		/// <param name="module">The module.</param>
		/// <param name="parameters">The parameters.</param>
		public ModuleInstance(IModule module, IElement[] parameters) {
			this.Module = module;
			this.Parameters = parameters;
		}

		/// <summary>
		/// Gets the module.
		/// </summary>
		/// <value>
		/// The module.
		/// </value>
		public IModule Module { get; private set; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public IElement[] Parameters { get; private set; }
	}
}
