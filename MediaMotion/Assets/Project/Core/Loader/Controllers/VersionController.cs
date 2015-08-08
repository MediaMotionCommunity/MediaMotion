using System;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes;
using UnityEngine;

namespace MediaMotion.Core.Loader.Controllers {
	/// <summary>
	/// Version Controller
	/// </summary>
	public class VersionController : AScript<VersionController> {
		/// <summary>
		/// Initializes the specified version.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <param name="buildMode">The build mode.</param>
		public void Init([Parameter("Version")] Version version, [Parameter("BuildMode")] string buildMode) {
			string versionString = "Version " + version.ToString(3);

			if (buildMode.CompareTo("Release") != 0) {
				versionString = "[" + buildMode + "] " + versionString;
			}
			this.gameObject.GetComponent<GUIText>().text = versionString;
		}
	}
}
