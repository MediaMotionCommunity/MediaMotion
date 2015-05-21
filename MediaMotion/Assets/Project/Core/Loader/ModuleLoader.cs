using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Loader {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ModuleLoader : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public override void Configure(IContainer container) {
			this.Name = "Module Loader";
			this.Scene = "Loader";
			this.Description = "Core Module to load other module";
			this.Container = container;
		}
	}
}
