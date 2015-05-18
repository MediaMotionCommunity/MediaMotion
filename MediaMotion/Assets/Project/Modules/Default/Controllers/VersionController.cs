using System;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes;
using UnityEngine;

namespace MediaMotion.Modules.Default.Controllers {
	/// <summary>
	/// Version Controller
	/// </summary>
	public class VersionController : AScript<VersionController> {
		/// <summary>
		/// Initializes the specified version.
		/// </summary>
		/// <param name="version">The version.</param>
		/// <param name="BuildMode">The build mode.</param>
		public void Init([Parameter("Version")] Version version, [Parameter("BuildMode")] string BuildMode) {
			string versionString = "Version " + version.ToString(3);

			if (BuildMode.CompareTo("Release") != 0) {
				versionString = "[" + BuildMode + "] " + versionString;
			}
			this.gameObject.GetComponent<GUIText>().text = versionString;
		}
	}
}
