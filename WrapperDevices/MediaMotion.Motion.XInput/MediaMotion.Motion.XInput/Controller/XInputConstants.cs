using System;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The button flags.
	/// </summary>
	[Flags]
	public enum ButtonFlags : int {
		XINPUT_GAMEPAD_DPAD_UP = 0x0001,
		XINPUT_GAMEPAD_DPAD_DOWN = 0x0002,
		XINPUT_GAMEPAD_DPAD_LEFT = 0x0004,
		XINPUT_GAMEPAD_DPAD_RIGHT = 0x0008,
		XINPUT_GAMEPAD_START = 0x0010,
		XINPUT_GAMEPAD_BACK = 0x0020,
		XINPUT_GAMEPAD_LEFT_THUMB = 0x0040,
		XINPUT_GAMEPAD_RIGHT_THUMB = 0x0080,
		XINPUT_GAMEPAD_LEFT_SHOULDER = 0x0100,
		XINPUT_GAMEPAD_RIGHT_SHOULDER = 0x0200,
		XINPUT_GAMEPAD_A = 0x1000,
		XINPUT_GAMEPAD_B = 0x2000,
		XINPUT_GAMEPAD_X = 0x4000,
		XINPUT_GAMEPAD_Y = 0x8000,
	};

	/// <summary>
	/// The controller subtypes.
	/// </summary>
	[Flags]
	public enum ControllerSubtypes {
		XINPUT_DEVSUBTYPE_UNKNOWN = 0x00,
		XINPUT_DEVSUBTYPE_WHEEL = 0x02,
		XINPUT_DEVSUBTYPE_ARCADE_STICK = 0x03,
		XINPUT_DEVSUBTYPE_FLIGHT_STICK = 0x04,
		XINPUT_DEVSUBTYPE_DANCE_PAD = 0x05,
		XINPUT_DEVSUBTYPE_GUITAR = 0x06,
		XINPUT_DEVSUBTYPE_GUITAR_ALTERNATE = 0x07,
		XINPUT_DEVSUBTYPE_DRUM_KIT = 0x08,
		XINPUT_DEVSUBTYPE_GUITAR_BASS = 0x0B,
		XINPUT_DEVSUBTYPE_ARCADE_PAD = 0x13
	};

	/// <summary>
	/// The battery types.
	/// </summary>
	public enum BatteryTypes : byte {
		//
		// Flags for battery status level
		//
		BATTERY_TYPE_DISCONNECTED = 0x00,    // This device is not connected
		BATTERY_TYPE_WIRED = 0x01,    // Wired device, no battery
		BATTERY_TYPE_ALKALINE = 0x02,    // Alkaline battery source
		BATTERY_TYPE_NIMH = 0x03,    // Nickel Metal Hydride battery source
		BATTERY_TYPE_UNKNOWN = 0xFF,    // Cannot determine the battery type
	};


	/// <summary>
	/// These are only valid for wireless, connected devices, with known battery types
	/// The amount of use time remaining depends on the type of device.
	/// </summary>
	public enum BatteryLevel : byte {
		BATTERY_LEVEL_EMPTY = 0x00,
		BATTERY_LEVEL_LOW = 0x01,
		BATTERY_LEVEL_MEDIUM = 0x02,
		BATTERY_LEVEL_FULL = 0x03
	};

	/// <summary>
	/// The battery device type.
	/// </summary>
	public enum BatteryDeviceType : byte {
		BATTERY_DEVTYPE_GAMEPAD = 0x00,
		BATTERY_DEVTYPE_HEADSET = 0x01,
	}

	public class XInputConstants {
		/// <summary>
		/// The xinpu t_ devtyp e_ gamepad.
		/// </summary>
		public const int XINPUT_DEVTYPE_GAMEPAD = 0x01;

		/// <summary>
		/// Flags for XINPUT_CAPABILITIES
		/// </summary>
		public const int XINPUT_DEVSUBTYPE_GAMEPAD = 0x01;

		#region Constants for gamepad buttons

		#region Gamepad thresholds

		/// <summary>
		/// The xinpu t_ gamepa d_ lef t_ thum b_ deadzone.
		/// </summary>
		public const int XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;

		/// <summary>
		/// The xinpu t_ gamepa d_ righ t_ thum b_ deadzone.
		/// </summary>
		public const int XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;

		/// <summary>
		/// The xinpu t_ gamepa d_ trigge r_ threshold.
		/// </summary>
		public const int XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;
		#endregion

		/// <summary>
		/// Flags to pass to XInputGetCapabilities
		/// </summary>
		public const int XINPUT_FLAG_GAMEPAD = 0x00000001;
		#endregion


	}
}
