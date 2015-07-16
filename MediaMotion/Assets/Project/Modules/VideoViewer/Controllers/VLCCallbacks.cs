using System;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers
{
    /// <summary>
    /// VLC Media callback handling
    /// </summary>
    public class VLCCallbacks {

        /// <summary>
        /// VLC Callback : Called when video frame is gonna be decoded
        /// </summary>
        static public IntPtr VideoLock(IntPtr opaque, ref IntPtr planes)
        {
            VLCMedia instance = (((GCHandle)opaque).Target as VLCMedia);
            // Block and allocate buffer, or use already allocated buffer
            instance.vlc_video_lock.WaitOne();
            if (instance.vlc_video_buffer == IntPtr.Zero) {
                instance.vlc_video_buffer = Marshal.AllocHGlobal((Int32)(instance.vlc_video_pitches * instance.vlc_video_lines));
            }
            planes = instance.vlc_video_buffer;
            return instance.vlc_video_buffer;
        }

        /// <summary>
        /// VLC Callback : Called when video frame is done decoded
        /// </summary>
        static public void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes)
        {
            VLCMedia instance = (((GCHandle)opaque).Target as VLCMedia);
            // Unlock buffer
            instance.vlc_video_lock.ReleaseMutex();
        }

        /// <summary>
        /// VLC Callback : Called when video frame is ready to render
        /// </summary>
        static public void VideoDisplay(IntPtr opaque, IntPtr picture)
        {
            VLCMedia instance = (((GCHandle)opaque).Target as VLCMedia);
            // Save the frame buffer pointer
            instance.vlc_video_result_buffer = picture;
            IntPtr pixels = instance.vlc_texture_pixels.Ptr();
            // Copy the picture content to texture pixels :)
            if (pixels != IntPtr.Zero && picture != IntPtr.Zero) {
                // Use memcpy as the destination texture and the vlc buffer are configured with the same memory pattern
                LibVLC.memcpy(pixels, picture, (uint)(instance.vlc_video_xsize * instance.vlc_video_ysize * 4));
            }
        }

        /// <summary>
        /// VLC Callback : Configure video frame format
        /// </summary>
        static public uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            VLCMedia instance = (((GCHandle)opaque).Target as VLCMedia);
            // Set color chroma ("BGRA")
            chroma = 0;
            chroma += 'A';
            chroma <<= 8;
            chroma += 'B';
            chroma <<= 8;
            chroma += 'G';
            chroma <<= 8;
            chroma += 'R';
            // Calc pitch/lines
            pitches = instance.vlc_video_xsize * 4;
            lines = instance.vlc_video_ysize;
            instance.vlc_video_pitches = pitches;
            instance.vlc_video_lines = lines;
            instance.vlc_video_xref = width;
            instance.vlc_video_yref = height;
            return 1;
        }

        /// <summary>
        /// VLC callback : Ends video frame formating
        /// </summary>
        static public void VideoUnformat(IntPtr opaque)
        {
        }

    }
}