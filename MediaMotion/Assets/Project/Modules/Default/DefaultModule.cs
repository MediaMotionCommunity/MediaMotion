using MediaMotion.Core.Models.Abstracts;

namespace MediaMotion.Modules.Default {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class DefaultModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Name = "Default";
			this.Scene = "Loader";
			this.Description = "Default Core Module";
		}
	}
}
