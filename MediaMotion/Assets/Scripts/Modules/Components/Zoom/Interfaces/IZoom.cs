namespace MediaMotion.Modules.Components.Zoom {
	public interface IZoom {
		float Coeff { get; }

		void ZoomIn();
		void ZoomOut();
	};
}
