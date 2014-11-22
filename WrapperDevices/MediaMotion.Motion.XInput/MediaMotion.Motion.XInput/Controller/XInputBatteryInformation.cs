using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input battery information.
	/// </summary>
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputBatteryInformation {
		/// <summary>
		/// The battery type.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(0)]
		public byte BatteryType;

		/// <summary>
		/// The battery level.
		/// </summary>
		[MarshalAs(UnmanagedType.I1)]
		[FieldOffset(1)]
		public byte BatteryLevel;

		/// <summary>
		/// The to string.
		/// </summary>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public override string ToString() {
			return string.Format("{0} {1}", (BatteryTypes)this.BatteryType, (BatteryLevel)this.BatteryLevel);
		}
	}
}
