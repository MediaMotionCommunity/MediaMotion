namespace MediaMotion.Modules.Components.Zoom {
	public interface IZoom {
		float Zoom { get; protected set; }

		//
		// Action
		//
		void ZoomIn();
		void ZoomOut();
	};
}
