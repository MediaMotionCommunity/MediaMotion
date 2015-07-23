using System;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// VideoViewer VLC Session
	/// </summary>
	public class VLCSession : IDisposable {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VLCSession"/> class.
		/// </summary>
		public VLCSession() {
			this.Session = LibVLC.libvlc_new(0, new string[] { });
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Ok()) {
				LibVLC.libvlc_release(this.Session);
			}
		}

		/// <summary>
		/// Oks this instance.
		/// </summary>
		/// <returns></returns>
		public bool Ok() {
			return (this.Session != IntPtr.Zero);
		}

		/// <summary>
		/// Checks this instance.
		/// </summary>
		/// <returns></returns>
		public bool Check() {
			if (!this.Ok()) {
				Debug.LogError("Unable to load VLC renderer");
				return (false);
			}
			return (true);
		}
	}
}
