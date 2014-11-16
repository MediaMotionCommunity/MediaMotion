using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
    public interface ICustomDetection {
        IEnumerable<IAction> Detection(Frame frame);
    }
}