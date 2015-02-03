using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.DefaultViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class DefaultViewerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultViewerModule"/> class.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public DefaultViewerModule(IContainerBuilder builder)
			: base(builder) {
		}

		/// <summary>
		/// Load another module.
		/// </summary>
		public override void Sleep() {
		}

		/// <summary>
		/// Back to the module.
		/// </summary>
		public override void WakeUp() {
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		public override void Unload() {
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		protected override void Configure() {
			this.Configuration.Name = "Default viewer";
			this.Configuration.Scene = "Default";
			this.Configuration.Description = "Default viewer, use for testing only";
		}
	}
}
