using System.Collections.Generic;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.Debug {
    public class Debug : IWrapperDevice {
	    public void Dispose() {
		    throw new System.NotImplementedException();
	    }

	    public string Name { get; private set; }
	    public string Type { get; private set; }
	    public string Link { get; private set; }
	    public string Author { get; private set; }
	    public void Load() {
		    throw new System.NotImplementedException();
	    }

	    public void Unload() {
		    throw new System.NotImplementedException();
	    }

	    public IEnumerable<IAction> GetActions() {
		    throw new System.NotImplementedException();
	    }
    }
}
