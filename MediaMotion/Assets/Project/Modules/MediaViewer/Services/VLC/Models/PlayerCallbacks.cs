using System;
using System.Runtime.InteropServices;
using MediaMotion.Modules.MediaViewer.Services.VLC.Bindings;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.MediaViewer.Services.VLC.Models {
	/// <summary>
	/// VLC player callback
	/// </summary>
	public class PlayerCallbacks {
		/// <summary>
		/// Configure video frame format.
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="chroma">The chroma.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="pitches">The pitches.</param>
		/// <param name="lines">The lines.</param>
		/// <returns>always <c>1</c></returns>
		public static uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			chroma = ('B' << 24) | ('G' << 16) | ('R' << 8) | 'A';
			pitches = width * 4;
			lines = height;
			instance.Pitches = (int)pitches;
			instance.Lines = (int)lines;
			return (1);
		}

		/// <summary>
		/// VLC callback : Ends video frame format
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		public static void VideoUnformat(IntPtr opaque) {
		}

		/// <summary>
		/// Called when video frame is decoded.
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="planes">The planes.</param>
		/// <returns>The buffer.</returns>
		public static IntPtr VideoLock(IntPtr opaque, ref IntPtr planes) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			instance.Lock.WaitOne();
			planes = instance.Buffer;
			return (instance.Buffer);
		}

		/// <summary>
		/// Called when video frame is done decoded.
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="picture">The picture.</param>
		/// <param name="planes">The planes.</param>
		public static void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			instance.Lock.ReleaseMutex();
		}

		/// <summary>
		/// Called when video frame is ready to render.
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="picture">The picture.</param>
		public static void VideoDisplay(IntPtr opaque, IntPtr picture) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			if (instance.Texture != null && instance.Texture.Ptr != IntPtr.Zero && picture != IntPtr.Zero) {
				LibVLC.memcpy(instance.Texture.Ptr, picture, (uint)(instance.Media.Width * instance.Media.Height * 4));
			}
		}
	}
}