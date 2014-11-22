using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input capabilities.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputCapabilities {
		/// <summary>
		/// The type.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(0)]
		public byte Type;

		/// <summary>
		/// The sub type.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(1)]
		public byte SubType;

		/// <summary>
		/// The flags.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(2)]
		public short Flags;


		/// <summary>
		/// The gamepad.
		/// </summary>
		[FieldOffset(4)]
		public XInputGamepad Gamepad;

		/// <summary>
		/// The vibration.
		/// </summary>
		[FieldOffset(16)]
		public XInputVibration Vibration;
	}
}
