using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input state.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputState {
		/// <summary>
		/// The packet number.
		/// </summary>
		[FieldOffset(0)]
		public int PacketNumber;

		/// <summary>
		/// The gamepad.
		/// </summary>
		[FieldOffset(4)]
		public XInputGamepad Gamepad;

		/// <summary>
		/// The copy.
		/// </summary>
		/// <param name="source">
		/// The source.
		/// </param>
		public void Copy(XInputState source) {
			this.PacketNumber = source.PacketNumber;
			this.Gamepad.Copy(source.Gamepad);
		}

		/// <summary>
		/// The equals.
		/// </summary>
		/// <param name="obj">
		/// The obj.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public override bool Equals(object obj) {
			if (obj == null || (!(obj is XInputState))) {
				return false;
			}
			var source = (XInputState)obj;
			return (this.PacketNumber == source.PacketNumber)
				 && this.Gamepad.Equals(source.Gamepad);
		}
	}
}
