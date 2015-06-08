﻿using System;
using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Detections Class
	/// Run all Detection class known
	/// </summary>
	public class Detections {
		#region Fields
		private readonly IDetectionContainer detectionContainer;

		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Detections" /> class.
		/// </summary>
		public Detections() {
			this.detectionContainer = new DetectionContainer();
			this.Configuration();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Run all detection class with specified frame
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <returns>List of IAction</returns>
		public IEnumerable<IAction> MovementsDetection(Frame frame) {
			var actions = new ActionCollection();
			this.detectionContainer.DetectMouvement(frame, actions);
			return actions;
		}

		public void DisableAllDetection() {
			this.detectionContainer.Clear();
		}

		public void EnableDetectionByAction(ActionType action) {
			this.detectionContainer.Enable(action);
		}
		#endregion

		#region Privates Methods
		private void Configuration() {
			this.detectionContainer.Register<ZoomDetection>(ActionType.ZoomIn, ActionType.ZoomOut);
			this.detectionContainer.Register<SwipeDetection>(ActionType.Back);
			this.detectionContainer.Register<EasyFileBrowsingDetection>(ActionType.BrowsingCursor, ActionType.BrowsingHighlight, ActionType.BrowsingScroll);
			this.detectionContainer.Register<PinchSelectionDetection>(ActionType.Select);
			this.detectionContainer.Register<PinchGrabDetection>(ActionType.GrabStart, ActionType.GrabStop);
			this.detectionContainer.Register<RightLeftDetection>(ActionType.Left, ActionType.Right);
            this.detectionContainer.Build();
		}
		#endregion
	}
}
