using System;
using System.Threading;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The xbox controller.
	/// </summary>
	public class XboxController {
		#region Static Fields
		/// <summary>
		/// The keep running.
		/// </summary>
		private static bool keepRunning;

		/// <summary>
		/// The update frequency.
		/// </summary>
		private static int updateFrequency;

		/// <summary>
		/// The wait time.
		/// </summary>
		private static int waitTime;

		/// <summary>
		/// The is running.
		/// </summary>
		private static bool isRunning;

		/// <summary>
		/// The sync lock.
		/// </summary>
		private static readonly object SyncLock;

		/// <summary>
		/// The polling thread.
		/// </summary>
		private static Thread pollingThread;
		#endregion

		#region Fields
		/// <summary>
		/// The player index.
		/// </summary>
		private readonly int playerIndex;

		/// <summary>
		/// The stop motor timer active.
		/// </summary>
		private bool stopMotorTimerActive;

		/// <summary>
		/// The stop motor time.
		/// </summary>
		private DateTime stopMotorTime;

		/// <summary>
		/// The battery information gamepad.
		/// </summary>
		private XInputBatteryInformation batteryInformationGamepad;

		/// <summary>
		/// The batter information headset.
		/// </summary>
		private XInputBatteryInformation batterInformationHeadset;

		/// <summary>
		/// The gamepad state prev.
		/// </summary>
		private XInputState gamepadStatePrev = new XInputState();

		/// <summary>
		/// The gamepad state current.
		/// </summary>
		private XInputState gamepadStateCurrent = new XInputState();

		bool isConnected;
		#endregion

		#region Constant

		/// <summary>
		/// The ma x_ controlle r_ count.
		/// </summary>
		public const int MAX_CONTROLLER_COUNT = 4;

		/// <summary>
		/// The firs t_ controlle r_ index.
		/// </summary>
		public const int FIRST_CONTROLLER_INDEX = 0;

		/// <summary>
		/// The las t_ controlle r_ index.
		/// </summary>
		public const int LAST_CONTROLLER_INDEX = MAX_CONTROLLER_COUNT - 1;
		#endregion

		/// <summary>
		/// Gets or sets the update frequency.
		/// </summary>
		public static int UpdateFrequency {
			get {
				return updateFrequency;
			}
			set {
				updateFrequency = value;
				waitTime = 1000 / updateFrequency;
			}
		}

		#region Battery Methods
		/// <summary>
		/// Gets the battery information gamepad.
		/// </summary>
		public XInputBatteryInformation BatteryInformationGamepad {
			get { return this.batteryInformationGamepad; }
			internal set { this.batteryInformationGamepad = value; }
		}

		/// <summary>
		/// Gets the battery information headset.
		/// </summary>
		public XInputBatteryInformation BatteryInformationHeadset {
			get { return this.batterInformationHeadset; }
			internal set { this.batterInformationHeadset = value; }
		}
		#endregion

		#region Controllers
		/// <summary>
		/// The controllers.
		/// </summary>
		private static XboxController[] Controllers;


		/// <summary>
		/// Initializes static members of the <see cref="XboxController"/> class.
		/// </summary>
		static XboxController() {
			Controllers = new XboxController[MAX_CONTROLLER_COUNT];
			SyncLock = new object();
			for (int i = FIRST_CONTROLLER_INDEX; i <= LAST_CONTROLLER_INDEX; ++i) {
				Controllers[i] = new XboxController(i);
			}
			UpdateFrequency = 25;
		}

		#region Events
		/// <summary>
		/// The state changed.
		/// </summary>
		public event EventHandler<XboxControllerStateChangedEventArgs> StateChanged = null;
		#endregion

		/// <summary>
		/// The retrieve controller.
		/// </summary>
		/// <param name="index">
		/// The index.
		/// </param>
		/// <returns>
		/// The <see cref="XboxController"/>.
		/// </returns>
		public static XboxController RetrieveController(int index) {
			return Controllers[index];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XboxController"/> class.
		/// </summary>
		/// <param name="playerIndex">
		/// The player index.
		/// </param>
		private XboxController(int playerIndex) {
			this.playerIndex = playerIndex;
			this.gamepadStatePrev.Copy(this.gamepadStateCurrent);
		}
		#endregion

		/// <summary>
		/// The update battery state.
		/// </summary>
		public void UpdateBatteryState() {
			XInputBatteryInformation headset = new XInputBatteryInformation(),
			gamepad = new XInputBatteryInformation();

			XInput.XInputGetBatteryInformation(this.playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD, ref gamepad);
			XInput.XInputGetBatteryInformation(this.playerIndex, (byte) BatteryDeviceType.BATTERY_DEVTYPE_HEADSET, ref headset);

			this.BatteryInformationHeadset = headset;
			this.BatteryInformationGamepad = gamepad;
		}

		/// <summary>
		/// The on state changed.
		/// </summary>
		protected void OnStateChanged() {
			if (this.StateChanged != null)
				this.StateChanged(this, new XboxControllerStateChangedEventArgs() { CurrentInputState = this.gamepadStateCurrent, PreviousInputState = this.gamepadStatePrev });
		}

		/// <summary>
		/// The get capabilities.
		/// </summary>
		/// <returns>
		/// The <see cref="XInputCapabilities"/>.
		/// </returns>
		public XInputCapabilities GetCapabilities() {
			var capabilities = new XInputCapabilities();
			XInput.XInputGetCapabilities(this.playerIndex, XInputConstants.XINPUT_FLAG_GAMEPAD, ref capabilities);
			return capabilities;
		}


		#region Digital Button States

		/// <summary>
		/// Gets a value indicating whether is d pad up pressed.
		/// </summary>
		public bool IsDPadUpPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_DPAD_UP); }
		}

		/// <summary>
		/// Gets a value indicating whether is d pad down pressed.
		/// </summary>
		public bool IsDPadDownPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN); }
		}

		/// <summary>
		/// Gets a value indicating whether is d pad left pressed.
		/// </summary>
		public bool IsDPadLeftPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT); }
		}

		/// <summary>
		/// Gets a value indicating whether is d pad right pressed.
		/// </summary>
		public bool IsDPadRightPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT); }
		}

		/// <summary>
		/// Gets a value indicating whether is a pressed.
		/// </summary>
		public bool IsAPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_A); }
		}

		/// <summary>
		/// Gets a value indicating whether is b pressed.
		/// </summary>
		public bool IsBPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_B); }
		}

		/// <summary>
		/// Gets a value indicating whether is x pressed.
		/// </summary>
		public bool IsXPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_X); }
		}

		/// <summary>
		/// Gets a value indicating whether is y pressed.
		/// </summary>
		public bool IsYPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_Y); }
		}

		/// <summary>
		/// Gets a value indicating whether is back pressed.
		/// </summary>
		public bool IsBackPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_BACK); }
		}

		/// <summary>
		/// Gets a value indicating whether is start pressed.
		/// </summary>
		public bool IsStartPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_START); }
		}


		/// <summary>
		/// Gets a value indicating whether is left shoulder pressed.
		/// </summary>
		public bool IsLeftShoulderPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER); }
		}


		/// <summary>
		/// Gets a value indicating whether is right shoulder pressed.
		/// </summary>
		public bool IsRightShoulderPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER); }
		}

		/// <summary>
		/// Gets a value indicating whether is left stick pressed.
		/// </summary>
		public bool IsLeftStickPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB); }
		}

		/// <summary>
		/// Gets a value indicating whether is right stick pressed.
		/// </summary>
		public bool IsRightStickPressed {
			get { return this.gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB); }
		}
		#endregion

		#region Analogue Input States

		/// <summary>
		/// Gets the left trigger.
		/// </summary>
		public int LeftTrigger {
			get { return (int) this.gamepadStateCurrent.Gamepad.bLeftTrigger; }
		}

		/// <summary>
		/// Gets the right trigger.
		/// </summary>
		public int RightTrigger {
			get { return (int) this.gamepadStateCurrent.Gamepad.bRightTrigger; }
		}

		/// <summary>
		/// Gets the left thumb stick.
		/// </summary>
		public Point LeftThumbStick {
			get {
				Point p = new Point() {
					X = this.gamepadStateCurrent.Gamepad.sThumbLX,
					Y = this.gamepadStateCurrent.Gamepad.sThumbLY
				};
				return p;
			}
		}

		/// <summary>
		/// Gets the right thumb stick.
		/// </summary>
		public Point RightThumbStick {
			get {
				return new Point {
					X = this.gamepadStateCurrent.Gamepad.sThumbRX,
					Y = this.gamepadStateCurrent.Gamepad.sThumbRY
				};
			}
		}

		#endregion

		

		/// <summary>
		/// Gets a value indicating whether is connected.
		/// </summary>
		public bool IsConnected {
			get { return this.isConnected; }
			internal set { this.isConnected = value; }
		}

		#region Polling

		/// <summary>
		/// The start polling.
		/// </summary>
		public static void StartPolling() {
			if (!isRunning) {
				lock (SyncLock) {
					if (!isRunning) {
						pollingThread = new Thread(PollerLoop);
						pollingThread.Start();
					}
				}
			}
		}

		/// <summary>
		/// The stop polling.
		/// </summary>
		public static void StopPolling() {
			if (isRunning)
				keepRunning = false;
		}

		/// <summary>
		/// The poller loop.
		/// </summary>
		private static void PollerLoop() {
			lock (SyncLock) {
				if (isRunning == true) {
					return;
				}
				isRunning = true;
			}
			keepRunning = true;
			while (keepRunning) {
				for (int i = FIRST_CONTROLLER_INDEX; i <= LAST_CONTROLLER_INDEX; ++i) {
					Controllers[i].UpdateState();
				}
				Thread.Sleep(updateFrequency);
			}
			lock (SyncLock) {
				isRunning = false;
			}
		}

		/// <summary>
		/// The update state.
		/// </summary>
		public void UpdateState() {
			var x = new XInputCapabilities();
			int result = XInput.XInputGetState(this.playerIndex, ref this.gamepadStateCurrent);
			this.IsConnected = result == 0;

			this.UpdateBatteryState();
			if (this.gamepadStateCurrent.PacketNumber != this.gamepadStatePrev.PacketNumber) {
				this.OnStateChanged();
			}
			this.gamepadStatePrev.Copy(this.gamepadStateCurrent);

			if (this.stopMotorTimerActive && (DateTime.Now >= this.stopMotorTime)) {
				var stopStrength = new XInputVibration { LeftMotorSpeed = 0, RightMotorSpeed = 0 };
				XInput.XInputSetState(this.playerIndex, ref stopStrength);
			}
		}
		#endregion

		#region Motor Functions
		/// <summary>
		/// The vibrate.
		/// </summary>
		/// <param name="leftMotor">
		/// The left motor.
		/// </param>
		/// <param name="rightMotor">
		/// The right motor.
		/// </param>
		public void Vibrate(double leftMotor, double rightMotor) {
			this.Vibrate(leftMotor, rightMotor, TimeSpan.MinValue);
		}

		/// <summary>
		/// The vibrate.
		/// </summary>
		/// <param name="leftMotor">
		/// The left motor.
		/// </param>
		/// <param name="rightMotor">
		/// The right motor.
		/// </param>
		/// <param name="length">
		/// The length.
		/// </param>
		public void Vibrate(double leftMotor, double rightMotor, TimeSpan length) {
			leftMotor = Math.Max(0d, Math.Min(1d, leftMotor));
			rightMotor = Math.Max(0d, Math.Min(1d, rightMotor));

			var vibration = new XInputVibration { LeftMotorSpeed = (ushort)(65535d * leftMotor), RightMotorSpeed = (ushort)(65535d * rightMotor) };
			this.Vibrate(vibration, length);
		}

		/// <summary>
		/// The vibrate.
		/// </summary>
		/// <param name="strength">
		/// The strength.
		/// </param>
		public void Vibrate(XInputVibration strength) {
			this.stopMotorTimerActive = false;
			XInput.XInputSetState(this.playerIndex, ref strength);
		}

		/// <summary>
		/// The vibrate.
		/// </summary>
		/// <param name="strength">
		/// The strength.
		/// </param>
		/// <param name="length">
		/// The length.
		/// </param>
		public void Vibrate(XInputVibration strength, TimeSpan length) {
			XInput.XInputSetState(this.playerIndex, ref strength);
			if (length != TimeSpan.MinValue) {
				this.stopMotorTime = DateTime.Now.Add(length);
				this.stopMotorTimerActive = true;
			}
		}
		#endregion

		/// <summary>
		/// The to string.
		/// </summary>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public override string ToString() {
			return this.playerIndex.ToString();
		}

	}
}
