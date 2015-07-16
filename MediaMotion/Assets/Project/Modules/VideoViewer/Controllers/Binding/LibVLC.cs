using System;
using System.Runtime.InteropServices;

/**
    How to compile

Mac OS:
    Compiled VLC -> $CCDIR (https://wiki.videolan.org/OSXCompile/) tested with v2.2.0
    VLC Viewer Binding path -> $BIND
    Output DLL path -> $OUT

    mkdir $OUT/vlc/
    mkdir $OUT/vlc/lib/
    mkdir $OUT/vlc/lib/vlc/
    mkdir $OUT/vlc/lib/vlc/plugins
    cp -r $CCDIR/build/VLC.app/Contents/MacOS/lib/* $OUT/vlc/lib/
    cp -r $CCDIR/build/VLC.app/Contents/MacOS/plugins/* $OUT/vlc/lib/vlc/plugins/
    mv $OUT/vlc/lib/libvlc.dylib $OUT/vlc/lib/libvlc.bundle
    python $BIND/DylibLinkEditor.py $OUT/vlc/lib/vlc/plugins/

    Example (with $PWD == DyLibLinkEditor.py directory):
        export CCDIR="/Users/vincentbrunet/Downloads/vlc-2.2.0"
        export OUT="../../../../../ModulesLibraries"
        export BIND="."

    To help debugging: emacs $CCDIR/src/misc/message.c and recompile after adding:

        // In the "vlc_Log" function
        FILE *p = fopen($ABSOLUTEPATH_DEBUGFILE, "a");
        fprintf(p, "[VLC] ");
        vfprintf(p, format, ap);
        fprintf(p, "\n");
        fclose(p);

Windows:
    FIX-ME

 */
namespace MediaMotion.Modules.VideoViewer.Controllers.Binding
{
    static class LibVLC
    {
        // VLC internal components
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
            [FieldOffset(0)] public uint i_codec;
            [FieldOffset(4)] public int i_id;
            [FieldOffset(8)] public libvlc_track_type_t i_type;
            [FieldOffset(12)] public int i_profile;
            [FieldOffset(16)] public int i_level;
            [FieldOffset(20)] public libvlc_media_track_info_t_audio audio;
            [FieldOffset(20)] public libvlc_media_track_info_t_video video;
        }

        // VLC session loading
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);
        [DllImport("libvlc")]
        public static extern void libvlc_release(IntPtr inst);

        // Media loading
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_new_path(IntPtr inst, [MarshalAs(UnmanagedType.LPStr)] string path);
        [DllImport("libvlc")]
        public static extern void libvlc_media_release(IntPtr media);

        // Media track infos
        [DllImport("libvlc")]
        public static extern void libvlc_media_parse(IntPtr media);
        [DllImport("libvlc")]
        private static extern int libvlc_media_get_tracks_info(IntPtr media, out IntPtr res);
        [DllImport("libvlc")]
        public static extern void libvlc_free(IntPtr inst);

        // Media tracks parsing function
        public static void libvlc_media_fetch_tracks_info(IntPtr media, out libvlc_media_track_info_t[] tracks)
        {
            // Pre-parse the media
            libvlc_media_parse(media);
            // Get the raw tracks array
            Type tracks_type = typeof(libvlc_media_track_info_t);
            IntPtr tracks_array;
            IntPtr tracks_current;
            int tracks_number = Math.Max(libvlc_media_get_tracks_info(media, out tracks_array), 0);
            // Fill the C# tracks array
            tracks = new libvlc_media_track_info_t[tracks_number];
            tracks_current = tracks_array;
            for(int i = 0; i < tracks.Length; i++) {
                tracks[i] = (libvlc_media_track_info_t)Marshal.PtrToStructure(tracks_current, tracks_type);
                tracks_current = new IntPtr(tracks_current.ToInt64() + Marshal.SizeOf(tracks_type));
            }
            // Free the raw tracks array
            libvlc_free(tracks_array);
        }

        // Player loading
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_release(IntPtr player);

        // Player actions
        [DllImport("libvlc")]
        public static extern int libvlc_media_player_is_playing(IntPtr player);
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_play(IntPtr player);
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_pause(IntPtr player);
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_stop(IntPtr player);
        [DllImport("libvlc")]
        public static extern void libvlc_media_player_set_time(IntPtr player, Int64 time);
        [DllImport("libvlc")]
        public static extern Int64 libvlc_media_player_get_time(IntPtr player);
        [DllImport("libvlc")]
        public static extern Int64 libvlc_media_player_get_length(IntPtr player);

        // Set video format callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint libvlc_video_format_cb(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_cleanup_cb(IntPtr opaque);
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_format_callbacks(IntPtr player, libvlc_video_format_cb setup, libvlc_video_cleanup_cb cleanup, IntPtr opaque);

        // Set video rendering callbacks
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr libvlc_video_lock_cb(IntPtr opaque, ref IntPtr planes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_unlock_cb(IntPtr opaque, IntPtr picture, ref IntPtr planes);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void libvlc_video_display_cb(IntPtr opaque, IntPtr picture);
        [DllImport("libvlc")]
        public static extern void libvlc_video_set_callbacks(IntPtr player, libvlc_video_lock_cb _lock, libvlc_video_unlock_cb unlock, libvlc_video_display_cb display, IntPtr opaque);

        // Get lib infos
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_get_version();
        [DllImport("libvlc")]
        public static extern IntPtr libvlc_errmsg();

        // System memcpy from C
        [DllImport("msvcrt")]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, uint count);
    }
}
