using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer {
    /// <summary>
    /// Default viewer module
    /// </summary>
    public class VideoViewerModule : AModule {

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewerModule"/> class.
        /// </summary>
        public VideoViewerModule()
            : base() {
        }

        /// <summary>
        /// Configures this instance.
        /// </summary>
        protected override void Configure() {
            this.Configuration.Name = "Video Viewer";
            this.Configuration.Scene = "VideoViewer";
            this.Configuration.Description = "Watch your movies and videos";
        }

    }
}
