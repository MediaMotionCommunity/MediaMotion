using System;
using MediaMotion.Modules.VideoViewer.Services.VLC.Binding;
using System.Runtime.InteropServices;
using UnityEngine;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces;

namespace MediaMotion.Modules.VideoViewer.Services.VLC.Models {
	/// <summary>
	/// VLC player callback
	/// </summary>
	public class PlayerCallbacks {
		/// <summary>
		/// Configure video frame format.
		/// </summary>
		static public uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			chroma = ('B' << 24) | ('G' << 16) | ('R' << 8) | 'A';
			pitches = width * 4;
			lines = height;
			instance.Pitches = (int)pitches;
			instance.Lines = (int)lines;
			return (1);
		}

		/// <summary>
		/// VLC callback : Ends video frame formating
		/// </summary>
		static public void VideoUnformat(IntPtr opaque) {
		}

		/// <summary>
		/// Called when video frame is gonna be decoded.
		/// </summary>
		static public IntPtr VideoLock(IntPtr opaque, ref IntPtr planes) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			instance.Lock.WaitOne();
			planes = instance.Buffer;
			return (instance.Buffer);
		}

		/// <summary>
		/// Called when video frame is done decoded.
		/// </summary>
		static public void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			instance.Lock.ReleaseMutex();
		}

		/// <summary>
		/// Called when video frame is ready to render.
		/// </summary>
		static public void VideoDisplay(IntPtr opaque, IntPtr picture) {
			IPlayer instance = (IPlayer)((GCHandle)opaque).Target;

			if (instance.Texture != null && instance.Texture.Ptr != IntPtr.Zero && picture != IntPtr.Zero) {
				LibVLC.memcpy(instance.Texture.Ptr, picture, (uint)(instance.Media.Width * instance.Media.Height * 4));
			}
		}
	}
}