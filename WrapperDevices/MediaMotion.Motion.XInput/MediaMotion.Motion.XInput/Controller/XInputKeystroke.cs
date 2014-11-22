using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input keystroke.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputKeystroke {
		/// <summary>
		/// The virtual key.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(0)]
		public short VirtualKey;

		/// <summary>
		/// The unicode.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(2)]
		public char Unicode;

		/// <summary>
		/// The flags.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(4)]
		public short Flags;

		/// <summary>
		/// The user index.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(5)]
		public byte UserIndex;

		/// <summary>
		/// The hid code.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(6)]
		public byte HidCode;
	}
}
