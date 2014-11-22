using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MediaMotion.Motion.XInput
{
    public class XInput : IWrapperDevice {
	    public XInput() {
		    this.Name = "XInput";
		    this.Type = "XInput";
		    this.Link = string.Empty;
		    this.Author = "MediaMotion";
	    }

	    public void Dispose() {
	    }

	    public string Name { get; private set; }
	    public string Type { get; private set; }
	    public string Link { get; private set; }
	    public string Author { get; private set; }
	    public void Load() {
		    
	    }

	    public void Unload() {
		    
	    }

	    public IEnumerable<IAction> GetActions() {
		    var actions = new List<IAction>();
		    var stats = GamePad.GetState(PlayerIndex.One);
			if (stats.DPad.Down == ButtonState.Pressed) actions.Add(new Action(ActionType.Down, null));
			if (stats.DPad.Right == ButtonState.Pressed) actions.Add(new Action(ActionType.Right, null));
			if (stats.DPad.Left == ButtonState.Pressed) actions.Add(new Action(ActionType.Left, null));
			if (stats.DPad.Up == ButtonState.Pressed) actions.Add(new Action(ActionType.Up, null));
			return actions;
	    }
    }
}
