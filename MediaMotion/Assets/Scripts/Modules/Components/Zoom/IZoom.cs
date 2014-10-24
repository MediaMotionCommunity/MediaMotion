namespace MediaMotion.Modules.Components.Zoom {
	public interface IZoom {
		float Zoom { get; }

		//
		// Action
		//
		void ZoomIn();
		void ZoomOut();
	};
}
