using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
    public class LeapMotionListener : Listener {
        #region Fields
        /// <summary>
        /// The movement detection class
        /// </summary>
        private readonly Detections movementsDetection;

        /// <summary>
        /// Object for lock use of detection class
        /// </summary>
        private readonly object thisLock = new object();

        private ActionsUpdater actionsUpdater;
        #endregion

        #region Constructor
        public LeapMotionListener(Detections movementsDetection, object thisLock) {
            this.actionsUpdater = new ActionsUpdater();
            this.movementsDetection = movementsDetection;
            this.thisLock = thisLock;
        }
        #endregion

        #region Methods
        public IEnumerable<IAction> RetrieveActions() {
            lock (this.thisLock) {
                return this.actionsUpdater.RetrieveCurrentActions();
            }
        }
        #endregion

        #region Leap callback
        public override void OnFrame(Controller controller) {
            lock (this.thisLock) {
                var actions = this.movementsDetection.MovementsDetection(controller.Frame());
                this.actionsUpdater.UpdateActions(actions);
            }
        }
        #endregion
    }
}
