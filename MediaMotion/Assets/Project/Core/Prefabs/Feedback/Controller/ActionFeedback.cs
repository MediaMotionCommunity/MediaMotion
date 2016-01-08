using UnityEngine;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using System;

namespace MediaMotion.Core.Prefabs.Feedback.Controllers {
	/// <summary>
	/// Action feedback
	/// </summary>
	public class ActionFeedback : AScript<ActionFeedback> {
		/// <summary>
		/// The time before disparition after cancel
		/// </summary>
		const float TIME_BEFORE_APARITION_AFTER_START = 0.50f;

		/// <summary>
		/// The time before disparition after cancel
		/// </summary>
		const float TIME_BEFORE_DISPARITION_AFTER_CANCEL = 1.0f;

		/// <summary>
		/// The progress bar container
		/// </summary>
		public GameObject ProgressBarContainer;

		/// <summary>
		/// The progress bar
		/// </summary>
		public GameObject ProgressBar;

		/// <summary>
		/// The icone
		/// </summary>
		public GameObject Image;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The action
		/// </summary>
		private ActionType action;

		/// <summary>
		/// The start
		/// </summary>
		private float? start;

		/// <summary>
		/// The cancel
		/// </summary>
		private float? cancel;

		/// <summary>
		/// The progress bar controller
		/// </summary>
		private ActionFeedbackProgressBar progressBarController;

		/// <summary>
		/// The image controller
		/// </summary>
		private ActionFeedbackImage imageController;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;

			this.progressBarController = this.ProgressBar.GetComponent<ActionFeedbackProgressBar>();
			this.imageController = this.Image.GetComponent<ActionFeedbackImage>();
			this.ClearAction();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			if (this.cancel.HasValue) {
				if ((this.cancel -= Time.deltaTime) <= 0.0f) {
					this.ClearAction();
				}
			}
			if (this.start.HasValue) {
				if ((this.start -= Time.deltaTime) <= 0.0f) {
					this.StartAction();
				}
			}
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.StartLeave:
					case ActionType.StartBack:
						this.InitAction(action.Type, (TimeSpan)action.Parameter);
						break;
					case ActionType.CancelLeave:
					case ActionType.CancelBack:
						if (!this.start.HasValue) {
							this.CancelAction();
						} else {
							this.ClearAction();
						}
						break;
					case ActionType.Leave:
					case ActionType.Back:
						this.ClearAction();
						break;
				}
			}
		}

		/// <summary>
		/// Inits the action.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="actionDuration">Duration of the action.</param>
		private void InitAction(ActionType action, TimeSpan actionDuration) {
			this.ClearAction();
			this.start = ActionFeedback.TIME_BEFORE_APARITION_AFTER_START;
			this.imageController.Init(action);
			this.progressBarController.Init(actionDuration, ActionFeedback.TIME_BEFORE_APARITION_AFTER_START);
		}

		/// <summary>
		/// Starts the action.
		/// </summary>
		private void StartAction() {
			this.start = null;
			if (this.ProgressBarContainer != null) {
				this.ProgressBarContainer.SetActive(true);
			}
			this.ProgressBar.SetActive(true);
			this.Image.SetActive(true);
		}

		/// <summary>
		/// Cancels the action.
		/// </summary>
		private void CancelAction() {
			this.imageController.Cancel = true;
			this.progressBarController.Cancel = true;
			this.cancel = ActionFeedback.TIME_BEFORE_DISPARITION_AFTER_CANCEL;
		}

		/// <summary>
		/// Clears the action.
		/// </summary>
		private void ClearAction() {
			if (this.ProgressBarContainer != null) {
				this.ProgressBarContainer.SetActive(false);
			}
			this.ProgressBar.SetActive(false);
			this.Image.SetActive(false);
			
			this.start = null;
			this.cancel = null;
		}
	}
}
