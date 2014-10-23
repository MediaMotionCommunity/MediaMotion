namespace MediaMotion.Modules.Components.Rotate {
	public interface IRotate {
		int Angle { get; protected set; }

		//
		// Action
		//
		void RotateLeft();
		void RotateRight();
	};
}
