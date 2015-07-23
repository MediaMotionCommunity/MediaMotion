using System;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// VLC Media callback handling
	/// </summary>
	public class VLCCallbacks {

		/// <summary>
		/// VLC Callback : Called when video frame is gonna be decoded
		/// </summary>
		static public IntPtr VideoLock(IntPtr opaque, ref IntPtr planes) {
			MediaController instance = (MediaController)((GCHandle)opaque).Target;

			instance.VideoLock.WaitOne();
			if (instance.VideoBuffer == IntPtr.Zero) {
				instance.VideoBuffer = Marshal.AllocHGlobal((Int32)(instance.VideoPitches * instance.VideoLines));
			}
			planes = instance.VideoBuffer;
			return (instance.VideoBuffer);
		}

		/// <summary>
		/// VLC Callback : Called when video frame is done decoded
		/// </summary>
		static public void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes) {
			MediaController instance = (MediaController)((GCHandle)opaque).Target;

			instance.VideoLock.ReleaseMutex();
		}

		/// <summary>
		/// VLC Callback : Called when video frame is ready to render
		/// </summary>
		static public void VideoDisplay(IntPtr opaque, IntPtr picture) {
			MediaController instance = (MediaController)((GCHandle)opaque).Target;

			instance.VideoResultBuffer = picture;
			IntPtr pixels = instance.TexturePixels.Ptr;
			if (pixels != IntPtr.Zero && picture != IntPtr.Zero) {
				LibVLC.memcpy(pixels, picture, (uint)(instance.VideoXSize * instance.VideoYSize * 4));
			}
		}

		/// <summary>
		/// VLC Callback : Configure video frame format
		/// </summary>
		static public uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines) {
			MediaController instance = (MediaController)((GCHandle)opaque).Target;

			// Set color chroma ("BGRA")
			chroma = 'B';
			chroma <<= 8;
			chroma += 'G';
			chroma <<= 8;
			chroma += 'R';
			chroma <<= 8;
			chroma += 'A';

			// Calc pitch/lines
			pitches = width * 4;
			lines = height;
			instance.VideoPitches = pitches;
			instance.VideoLines = lines;
			return (1);
		}

		/// <summary>
		/// VLC callback : Ends video frame formating
		/// </summary>
		static public void VideoUnformat(IntPtr opaque) {
		}

	}
}