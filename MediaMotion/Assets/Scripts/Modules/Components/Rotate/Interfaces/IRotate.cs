namespace MediaMotion.Modules.Components.Rotate {
	public interface IRotate {
		int Angle { get; }

		void RotateLeft();
		void RotateRight();
	};
}
