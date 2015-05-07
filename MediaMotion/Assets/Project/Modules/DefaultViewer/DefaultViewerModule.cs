using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.DefaultViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	public class DefaultViewerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure(IContainer container) {
			this.Priority = int.MinValue;
			this.Name = "Default viewer";
			this.Scene = "Default";
			this.Description = "Default viewer, use for testing only";
			this.Container = container;
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
