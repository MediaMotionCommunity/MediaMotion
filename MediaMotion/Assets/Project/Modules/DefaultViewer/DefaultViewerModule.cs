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
		/// Initializes a new instance of the <see cref="DefaultViewerModule" /> class.
		/// </summary>
		public DefaultViewerModule()
			: base() {
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Configuration.Name = "Default viewer";
			this.Configuration.Scene = "Default";
			this.Configuration.Description = "Default viewer, use for testing only";
		}
	}
}
