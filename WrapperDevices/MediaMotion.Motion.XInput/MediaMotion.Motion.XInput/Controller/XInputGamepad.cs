using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input gamepad.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputGamepad {
		/// <summary>
		/// The w buttons.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(0)]
		public short wButtons;

		/// <summary>
		/// The b left trigger.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(2)]
		public byte bLeftTrigger;

		/// <summary>
		/// The b right trigger.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(3)]
		public byte bRightTrigger;

		/// <summary>
		/// The s thumb lx.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(4)]
		public short sThumbLX;

		/// <summary>
		/// The s thumb ly.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(6)]
		public short sThumbLY;

		/// <summary>
		/// The s thumb rx.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(8)]
		public short sThumbRX;

		/// <summary>
		/// The s thumb ry.
		/// </summary>
		[MarshalAs(UnmanagedType.I2)]
		[FieldOffset(10)]
		public short sThumbRY;


		/// <summary>
		/// The is button pressed.
		/// </summary>
		/// <param name="buttonFlags">
		/// The button flags.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool IsButtonPressed(int buttonFlags) {
			return (this.wButtons & buttonFlags) == buttonFlags;
		}


		/// <summary>
		/// The copy.
		/// </summary>
		/// <param name="source">
		/// The source.
		/// </param>
		public void Copy(XInputGamepad source) {
			this.sThumbLX = source.sThumbLX;
			this.sThumbLY = source.sThumbLY;
			this.sThumbRX = source.sThumbRX;
			this.sThumbRY = source.sThumbRY;
			this.bLeftTrigger = source.bLeftTrigger;
			this.bRightTrigger = source.bRightTrigger;
			this.wButtons = source.wButtons;
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
			if (!(obj is XInputGamepad)) {
				return false;
			}
			var source = (XInputGamepad)obj;
			return (this.sThumbLX == source.sThumbLX)
			&& (this.sThumbLY == source.sThumbLY)
			&& (this.sThumbRX == source.sThumbRX)
			&& (this.sThumbRY == source.sThumbRY)
			&& (this.bLeftTrigger == source.bLeftTrigger)
			&& (this.bRightTrigger == source.bRightTrigger)
			&& (this.wButtons == source.wButtons);
		}
	}
}
