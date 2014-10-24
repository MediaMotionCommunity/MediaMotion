namespace MediaMotion.Modules.Components.Zoom {
	public interface IZoom {
		float Coeff { get; }

		//
		// Action
		//
		void ZoomIn();
		void ZoomOut();
	};
}
