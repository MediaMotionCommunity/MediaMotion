using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The x input.
	/// </summary>
	public static class XInput {
#if WINDOWS7
        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputGetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref XInputState pState        // [out] Receives the current state
        );

        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputSetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref XInputVibration pVibration    // [in, out] The vibration information to send to the controller
        );

        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputGetCapabilities
        (
            int dwUserIndex,   // [in] Index of the gamer associated with the device
            int dwFlags,       // [in] Input flags that identify the device type
            ref XInputCapabilities pCapabilities  // [out] Receives the capabilities
        );


        //this function is not available prior to Windows 8
        public static int XInputGetBatteryInformation
        (
              int dwUserIndex,        // Index of the gamer associated with the device
              byte devType,            // Which device on this user index
            ref XInputBatteryInformation pBatteryInformation // Contains the level and types of batteries
        )
        {
           return 0;
        }

        //this function is not available prior to Windows 8
        public static int XInputGetKeystroke
        (
            int dwUserIndex,              // Index of the gamer associated with the device
            int dwReserved,               // Reserved for future use
           ref      XInputKeystroke pKeystroke    // Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
        )
        {
            return 0;
        }
#else
		/// <summary>
		/// The x input get state.
		/// </summary>
		/// <param name="dwUserIndex">
		/// [in] Index of the gamer associated with the device
		/// </param>
		/// <param name="pState">
		/// [out] Receives the current state
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("xinput1_4.dll")]
		public static extern int XInputGetState(int dwUserIndex, ref XInputState pState);

		/// <summary>
		/// The x input set state.
		/// </summary>
		/// <param name="dwUserIndex">
		/// [in] Index of the gamer associated with the device
		/// </param>
		/// <param name="pVibration">
		/// [in, out] The vibration information to send to the controller
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("xinput1_4.dll")]
		public static extern int XInputSetState(int dwUserIndex, ref XInputVibration pVibration);

		/// <summary>
		/// The x input get capabilities.
		/// </summary>
		/// <param name="dwUserIndex">
		/// [in] Index of the gamer associated with the device
		/// </param>
		/// <param name="dwFlags">
		/// [in] Input flags that identify the device type
		/// </param>
		/// <param name="pCapabilities">
		/// [out] Receives the capabilities
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("xinput1_4.dll")]
		public static extern int XInputGetCapabilities(int dwUserIndex, int dwFlags, ref XInputCapabilities pCapabilities);

		/// <summary>
		/// The x input get battery information.
		/// </summary>
		/// <param name="dwUserIndex">
		/// Index of the gamer associated with the device
		/// </param>
		/// <param name="devType">
		/// Which device on this user index
		/// </param>
		/// <param name="pBatteryInformation">
		/// Contains the level and types of batteries
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("xinput1_4.dll")]
		public static extern int XInputGetBatteryInformation(int dwUserIndex, byte devType, ref XInputBatteryInformation pBatteryInformation);

		/// <summary>
		/// The x input get keystroke.
		/// </summary>
		/// <param name="dwUserIndex">
		/// Index of the gamer associated with the device
		/// </param>
		/// <param name="dwReserved">
		/// Reserved for future use
		/// </param>
		/// <param name="pKeystroke">
		/// Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
		/// </param>
		/// <returns>
		/// The <see cref="int"/>.
		/// </returns>
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here."), DllImport("xinput1_4.dll")]
		public static extern int XInputGetKeystroke(int dwUserIndex, int dwReserved, ref XInputKeystroke pKeystroke);
#endif
	}
}
