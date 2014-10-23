using System;

public class MediaEvent : EventArgs {
	public Element Element { get; }

	public MediaEvent(Element Element) {
		this.Element = Element;
	}
}
