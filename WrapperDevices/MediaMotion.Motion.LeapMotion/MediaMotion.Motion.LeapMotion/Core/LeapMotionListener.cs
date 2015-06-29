using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaMotion.Motion.LeapMotion.Core {

    class LeapMotionListener : Listener {

        #region Fields

        /// <summary>
        /// The movement detection class
        /// </summary>
        private readonly Detections movementsDetection;

        /// <summary>
        /// Object for lock use of detection class
        /// </summary>
        private readonly Object thisLock = new Object();

        private ActionsUpdater actionsUpdater;
        #endregion

        #region Constructor
        public LeapMotionListener(Detections movementsDetection, Object thisLock) {
            this.actionsUpdater = new ActionsUpdater();
            this.movementsDetection = movementsDetection;
            this.thisLock = thisLock;
        }
        #endregion

        #region Methods
        public IEnumerable<IAction> RetrieveActions() {
            lock (thisLock) {
                return actionsUpdater.retrieveCurrentActions();
            }
        }
        #endregion

        #region Leap callback
        public override void OnFrame(Controller controller) {
            lock (thisLock) {
                var actions = this.movementsDetection.MovementsDetection(controller.Frame());
                actionsUpdater.UpdateActions(actions);
            }
        }
        #endregion
    }
}
