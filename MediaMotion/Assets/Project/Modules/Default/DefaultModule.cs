using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Modules.Default {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class DefaultModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public override void Configure(IContainer container) {
			this.Name = "Default";
			this.Scene = "Loader";
			this.Description = "Default Core Module";
			this.Container = container;
		}
	}
}
