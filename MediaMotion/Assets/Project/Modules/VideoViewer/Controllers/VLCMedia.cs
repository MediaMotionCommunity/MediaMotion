using System;
using System.Threading;
using System.Runtime.InteropServices;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers
{
    /// <summary>
    /// VLC Media node
    /// </summary>
    public class VLCMedia : MonoBehaviour {

        // VLC components
        private VLCSession  vlc_session;
        private IntPtr      vlc_media = IntPtr.Zero;
        private IntPtr      vlc_player = IntPtr.Zero;

        // Rendering
        public IntPtr      vlc_video_buffer = IntPtr.Zero;
        public IntPtr      vlc_video_result_buffer = IntPtr.Zero;

        // Video infos
        public string      vlc_path = "";
        public uint        vlc_video_xsize = 0;
        public uint        vlc_video_ysize = 0;
        public uint        vlc_video_xref = 0;
        public uint        vlc_video_yref = 0;
        public uint        vlc_video_pitches = 0;
        public uint        vlc_video_lines = 0;
        public Mutex       vlc_video_lock = new Mutex();

        // Texture infos
        public Texture     vlc_texture_base = null;
        public Texture2D   vlc_texture = null;
        public AutoPinner  vlc_texture_pixels = null;

        public void Init(VLCSession session, string path) {
            // Initial state
            vlc_texture_base = GetComponent<Renderer>().material.mainTexture;
            // External components
            vlc_session = session;
            vlc_path = path;
            // If all is good
            if (vlc_session.check()) {
                // Load media
                vlc_media = LibVLC.libvlc_media_new_path(vlc_session.get(), path);
                // Load media success
                if (vlc_media != IntPtr.Zero) {
                    // Fetch meta datas from the file (such as video size)
                    LibVLC.libvlc_media_track_info_t[] tracks;
                    LibVLC.libvlc_media_fetch_tracks_info(vlc_media, out tracks);
                    for (int i = 0; i < tracks.Length; i++) {
                        // If the file contains video, get its size
                        if (tracks[i].i_type == LibVLC.libvlc_track_type_t.libvlc_track_video) {
                            vlc_video_xsize = tracks[i].video.i_width;
                            vlc_video_ysize = tracks[i].video.i_height;
                            vlc_video_xsize = (vlc_video_xsize / 32 + 1) * 32;
                            vlc_video_ysize = (vlc_video_ysize / 32 + 1) * 32;
                            vlc_video_xref = vlc_video_xsize;
                            vlc_video_yref = vlc_video_ysize;
                        }
                    }
                    // Load the player for the media
                    vlc_player = LibVLC.libvlc_media_player_new_from_media(vlc_media);
                    // Load video success
                    if (vlc_player != IntPtr.Zero) {
                        // If has video to display
                        if (vlc_video_xsize * vlc_video_ysize > 0) {
                            // Create reception texture
                            vlc_texture = new Texture2D(
                                (int)vlc_video_xsize,
                                (int)vlc_video_ysize,
                                TextureFormat.RGBA32,
                                false
                            );
                            vlc_texture.hideFlags = HideFlags.HideAndDontSave;
                            vlc_texture.wrapMode = TextureWrapMode.Clamp;
                            vlc_texture.filterMode = FilterMode.Point;
                            // Fetch rendering texture raw datas pointer
                            vlc_texture_pixels = new AutoPinner(
                                vlc_texture.GetPixels32(0)
                            );
                            // Set video buffer format
                            LibVLC.libvlc_video_set_format_callbacks(
                                vlc_player,
                                VLCCallbacks.VideoFormat,
                                VLCCallbacks.VideoUnformat,
                                (IntPtr)GCHandle.Alloc(this)
                            );
                            // Set video buffer rendering
                            LibVLC.libvlc_video_set_callbacks(
                                vlc_player,
                                VLCCallbacks.VideoLock,
                                VLCCallbacks.VideoUnlock,
                                VLCCallbacks.VideoDisplay,
                                (IntPtr)GCHandle.Alloc(this)
                            );
                            // Scale the model to match the pdf ratio
                            float size = 1.0f / 10.0f;
                            transform.localScale = new Vector3(size / ratio(), size, -size);
                            // Set mesh texture to vlc
                            if (GetComponent<Renderer>() && ok()) {
                                GetComponent<Renderer>().material.mainTexture = vlc_texture;
                                GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
                            }
                            // Play on load
                            play();
                        }
                    }
                }
            }
        }

        public void OnDestroy() {
            // Unload player
            if (vlc_player != IntPtr.Zero) {
                LibVLC.libvlc_media_player_stop(vlc_player);
                LibVLC.libvlc_media_player_release(vlc_player);
            }
            // Unload page
            if (vlc_media != IntPtr.Zero) {
                LibVLC.libvlc_media_release(vlc_media);
            }
            // Unload render texture
            GetComponent<Renderer>().material.mainTexture = vlc_texture_base;
            if (vlc_texture != null) {
                Destroy(vlc_texture);
            }
            // Unload rendering buffer
            if (vlc_video_buffer != IntPtr.Zero) {
                Marshal.FreeHGlobal(vlc_video_buffer);
                vlc_video_buffer = IntPtr.Zero;
            }
        }

        public void Update() {
            // If video buffer ready apply on mesh texture (flush VLC frame to Unity Frame)
            if (ok()) {
                // Set main texture scale
                GetComponent<Renderer>().material.mainTextureScale = new Vector2(
                    (float)vlc_video_xref / (float)vlc_video_xsize,
                    (float)vlc_video_yref / (float)vlc_video_ysize
                );
                // Update main texture content
                vlc_texture.SetPixels32((Color32[])vlc_texture_pixels.Obj(), 0);
                vlc_texture.Apply();
            }

        }

        public void play() {
            if (check()) {
                // Call play on vlc media
                LibVLC.libvlc_media_player_play(vlc_player);
            }
        }

        public void pause() {
            if (check()) {
                // Call pause on vlc media
                LibVLC.libvlc_media_player_pause(vlc_player);
            }
        }

        public void stop() {
            if (check()) {
                // Call stop on vlc media
                LibVLC.libvlc_media_player_stop(vlc_player);
            }
        }

        public void time(double ratio) {
            if (check()) {
                Int64 length = LibVLC.libvlc_media_player_get_length(vlc_player);
                Int64 time = (Int64)((double)length * ratio);
                LibVLC.libvlc_media_player_set_time(vlc_player, time);
            }
        }

        public double time() {
            if (check()) {
                Int64 length = LibVLC.libvlc_media_player_get_length(vlc_player);
                Int64 time = LibVLC.libvlc_media_player_get_time(vlc_player);
                return (double)length / (double)time;
            }
            return 0;
        }

        public float ratio() {
            return (float)vlc_video_yref / (float)vlc_video_xref;
        }

        public bool ok() {
            // If VLC media loaded correctly
            if (vlc_session.ok() && vlc_media != IntPtr.Zero && vlc_player != IntPtr.Zero) {
                // If texture loaded correctly
                if (vlc_texture_pixels != null && vlc_texture != null) {
                    return true;
                }
            }
            return false;
        }

        public bool check() {
            if (!ok()) {
                Debug.LogError("Unable to load vlc media: " + vlc_path);
                return false;
            }
            return true;
        }
    }
}
