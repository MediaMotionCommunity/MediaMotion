using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input vibration.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct XInputVibration {
		/// <summary>
		/// The left motor speed.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		public ushort LeftMotorSpeed;

		/// <summary>
		/// The right motor speed.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		public ushort RightMotorSpeed;
	}
}
