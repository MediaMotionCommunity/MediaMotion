using System;
using System.Collections;
using MediaMotion.Modules.MediaViewer.Services.VLC.Bindings;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Services.VLC.Models {
	/// <summary>
	/// VLC Media
	/// </summary>
	public class MediaInfo : IMediaInfo {
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaInfo" /> class.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="element">The element.</param>
		/// <exception cref="System.Exception">Could not load media  + this.Element.GetName()</exception>
		public MediaInfo(IntPtr session, IMedia element) {
			this.Session = session;
			this.Element = element;
			this.Resource = LibVLC.libvlc_media_new_path(this.Session, this.Element.GetPath());

			if (this.Resource == IntPtr.Zero) {
				throw new Exception("Could not load media " + this.Element.GetName());
			}
			this.RetrieveMediaInfo();
		}

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session { get; private set; }

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		public IMedia Element { get; private set; }

		/// <summary>
		/// Gets the media.
		/// </summary>
		/// <value>
		/// The media.
		/// </value>
		public IntPtr Resource { get; private set; }

		/// <summary>
		/// Gets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		public int Duration { get; private set; }

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		public int Height { get; private set; }

		/// <summary>
		/// Gets the channel.
		/// </summary>
		/// <value>
		/// The channel.
		/// </value>
		public int Channel { get; private set; }

		/// <summary>
		/// Gets the rate.
		/// </summary>
		/// <value>
		/// The rate.
		/// </value>
		public int Rate { get; private set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Resource != IntPtr.Zero) {
				LibVLC.libvlc_media_release(this.Resource);
				this.Resource = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Gets the information.
		/// </summary>
		private void RetrieveMediaInfo() {
			LibVLC.libvlc_media_track_info_t[] tracks;

			LibVLC.libvlc_media_fetch_tracks_info(this.Resource, out tracks);
			for (int i = 0; i < tracks.Length; i++) {
				switch (tracks[i].Type) {
					case LibVLC.libvlc_track_type_t.libvlc_track_video:
						this.Width = (int)tracks[i].Video.Width;
						this.Height = (int)tracks[i].Video.Height;
						break;
					case LibVLC.libvlc_track_type_t.libvlc_track_audio:
						this.Channel = (int)tracks[i].Audio.i_channels;
						this.Rate = (int)tracks[i].Audio.i_rate;
						break;
				}
			}
			this.Duration = (int)(LibVLC.libvlc_media_get_duration(this.Resource) / 1000);
		}
	}
}
