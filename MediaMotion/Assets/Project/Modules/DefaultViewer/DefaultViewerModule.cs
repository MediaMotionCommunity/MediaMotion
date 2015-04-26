using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
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
			this.Configuration.Priority = int.MinValue;
			this.Configuration.Name = "Default viewer";
			this.Configuration.Scene = "Default";
			this.Configuration.Description = "Default viewer, use for testing only";
		}

		/// <summary>
		/// Supports the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the element is supported, <c>false</c> otherwise
		/// </returns>
		public override bool Supports(IElement element) {
			return (true);
		}
	}
}
