using System;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// VideoViewer VLC Session
	/// </summary>
	public class VLCSession {

		private IntPtr vlc_session;

		public VLCSession() {
			vlc_session = LibVLC.libvlc_new(0, new string[] { });
		}

		~VLCSession() {
			if (ok()) {
				LibVLC.libvlc_release(vlc_session);
			}
		}

		public bool ok() {
			return vlc_session != IntPtr.Zero;
		}

		public bool check() {
			if (!ok()) {
				Debug.LogError("Unable to load VLC renderer");
				return false;
			}
			return true;
		}

		public IntPtr get() {
			return vlc_session;
		}

	}
}
