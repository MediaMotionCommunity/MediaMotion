namespace MediaMotion.Modules.Components.Rotate {
	public interface IRotate {
		int Angle { get; }

		//
		// Action
		//
		void RotateLeft();
		void RotateRight();
	};
}
