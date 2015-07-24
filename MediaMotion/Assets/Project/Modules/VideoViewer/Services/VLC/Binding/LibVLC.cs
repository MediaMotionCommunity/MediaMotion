using System;
using System.Runtime.InteropServices;

namespace MediaMotion.Modules.VideoViewer.Services.VLC.Binding {
	/// <summary>
	/// C# binding class of lib VLC
	/// </summary>
	static class LibVLC {
		/// <summary>
		/// track enum
		/// </summary>
		public enum libvlc_track_type_t : int {
			libvlc_track_unknown = -1,
			libvlc_track_audio = 0,
			libvlc_track_video = 1,
			libvlc_track_text = 2
		}

		/// <summary>
		/// audio track info structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct libvlc_media_track_info_t_audio {
			public uint i_channels;
			public uint i_rate;
		}

		/// <summary>
		/// video track info structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct libvlc_media_track_info_t_video {
			public uint i_height;
			public uint i_width;
		}

		/// <summary>
		/// track info structure
		/// </summary>
		[StructLayout(LayoutKind.Explicit)]
		public struct libvlc_media_track_info_t {
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

		/// <summary>
		/// Create a new session.
		/// </summary>
		/// <param name="argc">The argc.</param>
		/// <param name="argv">The argv.</param>
		/// <returns></returns>
		[DllImport("libvlc")]
		public static extern IntPtr libvlc_new(int argc, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] argv);

		/// <summary>
		/// Release a session.
		/// </summary>
		/// <param name="session">The session.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_release(IntPtr session);

		/// <summary>
		/// Create a media object.
		/// </summary>
		/// <param name="inst">The inst.</param>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		[DllImport("libvlc")]
		public static extern IntPtr libvlc_media_new_path(IntPtr inst, [MarshalAs(UnmanagedType.LPStr)] string path);

		/// <summary>
		/// Release a media object.
		/// </summary>
		/// <param name="media">The media.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_release(IntPtr media);

		/// <summary>
		/// Parse a media object.
		/// </summary>
		/// <param name="media">The media.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_parse(IntPtr media);

		/// <summary>
		/// Get the number of tracks info.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <param name="res">The resource.</param>
		/// <returns>The number of tracks info.</returns>
		[DllImport("libvlc")]
		private static extern int libvlc_media_get_tracks_info(IntPtr media, out IntPtr res);

		/// <summary>
		/// Get the duration of the media.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <returns>The duration</returns>
		[DllImport("libvlc")]
		public static extern long libvlc_media_get_duration(IntPtr media);

		/// <summary>
		/// Free a VLC pointer.
		/// </summary>
		/// <param name="inst">The inst.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_free(IntPtr inst);

		/// <summary>
		/// Get media tracks info.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <param name="tracks">The tracks.</param>
		public static void libvlc_media_fetch_tracks_info(IntPtr media, out libvlc_media_track_info_t[] tracks) {
			// Pre-parse the media
			libvlc_media_parse(media);

			Type tracks_type = typeof(libvlc_media_track_info_t);
			IntPtr tracks_array;
			IntPtr tracks_current;
			int tracks_number = Math.Max(libvlc_media_get_tracks_info(media, out tracks_array), 0);

			tracks = new libvlc_media_track_info_t[tracks_number];
			tracks_current = tracks_array;
			for (int i = 0; i < tracks.Length; i++) {
				tracks[i] = (libvlc_media_track_info_t)Marshal.PtrToStructure(tracks_current, tracks_type);
				tracks_current = new IntPtr(tracks_current.ToInt64() + Marshal.SizeOf(tracks_type));
			}
			libvlc_free(tracks_array);
		}

		/// <summary>
		/// Create a player object.
		/// </summary>
		/// <param name="media">The media.</param>
		/// <returns>The player.</returns>
		[DllImport("libvlc")]
		public static extern IntPtr libvlc_media_player_new_from_media(IntPtr media);

		/// <summary>
		/// Release a player object
		/// </summary>
		/// <param name="player">The player.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_player_release(IntPtr player);

		/// <summary>
		/// Get the state of the player.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns><c>1</c> if playing, <c>0</c> otherwise</returns>
		[DllImport("libvlc")]
		public static extern int libvlc_media_player_is_playing(IntPtr player);

		/// <summary>
		/// Play the player.
		/// </summary>
		/// <param name="player">The player.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_player_play(IntPtr player);

		/// <summary>
		/// Pause the player.
		/// </summary>
		/// <param name="player">The player.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_player_pause(IntPtr player);

		/// <summary>
		/// Stop the player.
		/// </summary>
		/// <param name="player">The player.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_player_stop(IntPtr player);

		/// <summary>
		/// Get player time
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns>The time.</returns>
		[DllImport("libvlc")]
		public static extern Int64 libvlc_media_player_get_time(IntPtr player);

		/// <summary>
		/// Set player time.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="time">The time.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_media_player_set_time(IntPtr player, Int64 time);

		/// <summary>
		/// Get player length
		/// </summary>
		/// <param name="player">The player.</param>
		/// <returns>The length.</returns>
		[DllImport("libvlc")]
		public static extern Int64 libvlc_media_player_get_length(IntPtr player);

		/// <summary>
		/// Format callback delegate
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="chroma">The chroma.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="pitches">The pitches.</param>
		/// <param name="lines">The lines.</param>
		/// <returns></returns>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate uint libvlc_video_format_cb(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines);

		/// <summary>
		/// Cleanup callback delegate.
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void libvlc_video_cleanup_cb(IntPtr opaque);

		/// <summary>
		/// Set callbacks for the specified player event.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="setupCallback">The setup callback.</param>
		/// <param name="cleanupCallback">The cleanup callback.</param>
		/// <param name="opaque">The opaque.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_video_set_format_callbacks(IntPtr player, libvlc_video_format_cb setupCallback, libvlc_video_cleanup_cb cleanupCallback, IntPtr opaque);

		/// <summary>
		/// Lock callback delegate
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="planes">The planes.</param>
		/// <returns></returns>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr libvlc_video_lock_cb(IntPtr opaque, ref IntPtr planes);

		/// <summary>
		/// Unlock callback delegate
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="picture">The picture.</param>
		/// <param name="planes">The planes.</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void libvlc_video_unlock_cb(IntPtr opaque, IntPtr picture, ref IntPtr planes);

		/// <summary>
		/// Display callback delegate
		/// </summary>
		/// <param name="opaque">The opaque.</param>
		/// <param name="picture">The picture.</param>
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void libvlc_video_display_cb(IntPtr opaque, IntPtr picture);

		/// <summary>
		/// Set callbacks for the specified player event.
		/// </summary>
		/// <param name="player">The player.</param>
		/// <param name="lockCallback">The lock callback.</param>
		/// <param name="unlockCallback">The unlock callback.</param>
		/// <param name="displayCallback">The display callback.</param>
		/// <param name="opaque">The opaque.</param>
		[DllImport("libvlc")]
		public static extern void libvlc_video_set_callbacks(IntPtr player, libvlc_video_lock_cb lockCallback, libvlc_video_unlock_cb unlockCallback, libvlc_video_display_cb displayCallback, IntPtr opaque);

		/// <summary>
		/// Get the version of the library.
		/// </summary>
		/// <returns>The version.</returns>
		[DllImport("libvlc")]
		public static extern IntPtr libvlc_get_version();

		/// <summary>
		/// Get the last error message.
		/// </summary>
		/// <returns>The last error message, <c>null</c> if no errors occured.</returns>
		[DllImport("libvlc")]
		public static extern IntPtr libvlc_errmsg();

		/// <summary>
		/// Binary copy of <see cref="count"/> bytes from <see cref="src"/> to <see cref="dest"/>.
		/// </summary>
		/// <param name="dest">The dest.</param>
		/// <param name="src">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns>The <see cref="dest"/>.</returns>
		[DllImport("msvcrt")]
		public static extern IntPtr memcpy(IntPtr dest, IntPtr src, uint count);
	}
}
