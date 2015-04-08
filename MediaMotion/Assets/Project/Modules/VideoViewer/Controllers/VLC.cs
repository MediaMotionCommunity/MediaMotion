using System;
using System.Runtime.InteropServices;

namespace VLC
{
    static class LibVLC
    {
        #region Structs track infos
        public enum libvlc_track_type_t : int
        {
            libvlc_track_unknown = -1,
            libvlc_track_audio = 0,
            libvlc_track_video = 1,
            libvlc_track_text = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_media_track_info_t_audio
        {
            public uint i_channels;
            public uint i_rate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct libvlc_media_track_info_t_video
        {
            public uint i_height;
            public uint i_width;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct libvlc_media_track_info_t
        {
            /* Codec fourcc */
            [FieldOffset(0)]
            public uint i_codec;

            [FieldOffset(4)]
            public int i_id;

            [FieldOffset(8)]
            public libvlc_track_type_t i_type;

            [FieldOffset(12)]
            public int i_profile;

            [FieldOffset(16)]
            public int i_level;

            [FieldOffset(20)]
            public libvlc_media_track_info_t_audio audio;

            [FieldOffset(20)]
            public libvlc_media_track_info_t_video video;
        }
        #endregion

        #region Core
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);

        [DllImport("libvlc")]
        public static extern void libvlc_release(IntPtr inst);

        [DllImport("libvlc")]
        public static extern void libvlc_free(IntPtr ptr);
        #endregion

        #region media
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_new_path(IntPtr inst, [MarshalAs(UnmanagedType.LPStr)] string path);

        [DllImport("libvlc")]
        public static extern void libvlc_media_release(IntPtr media);

        [DllImport("libvlc")]
        public static extern void libvlc_media_parse(IntPtr media);

        [DllImport("libvlc")]
        private static extern int libvlc_media_get_tracks_info(IntPtr media, out IntPtr res);
        public static int libvlc_media_fetch_tracks_info(IntPtr media, out libvlc_media_track_info_t[] tracks)
        {
            IntPtr result_buffer;
            int result = libvlc_media_get_tracks_info(media, out result_buffer);
            if(result < 0)
            {
                tracks = null;
                return result;
            }
            IntPtr buffer = result_buffer;
            tracks = new libvlc_media_track_info_t[result];
            for(int i = 0; i < tracks.Length; i++)
            {
                tracks[i] = (libvlc_media_track_info_t)Marshal.PtrToStructure(buffer, typeof(libvlc_media_track_info_t));
                buffer = (IntPtr)((int)buffer + Marshal.SizeOf(typeof(libvlc_media_track_info_t)));
            }
            libvlc_free(result_buffer);
            return result;
        }
        #endregion

        #region media player
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_release(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_play(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_pause(IntPtr player);

        [DllImport("libvlc")]
        public static extern void libvlc_media_player_stop(IntPtr player);
        #endregion

        #region video
        // Set format callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint libvlc_video_format_cb(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_cleanup_cb(IntPtr opaque);
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_format_callbacks(IntPtr player, libvlc_video_format_cb setup, libvlc_video_cleanup_cb cleanup, IntPtr opaque);

        // Set callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr libvlc_video_lock_cb(IntPtr opaque, ref IntPtr planes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_unlock_cb(IntPtr opaque, IntPtr picture, ref IntPtr planes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_display_cb(IntPtr opaque, IntPtr picture);
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_callbacks(IntPtr player, libvlc_video_lock_cb _lock, libvlc_video_unlock_cb unlock, libvlc_video_display_cb display, IntPtr opaque);

        // Set format
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_format(IntPtr player, [MarshalAs(UnmanagedType.LPStr)] string chroma, uint width, uint height, uint pitch);
        #endregion

        #region miscs
        // Importing system memcpy from C
        [DllImport("msvcrt")]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
        #endregion
    }
}
