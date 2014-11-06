using System;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models.Interfaces {
	public interface IElement {
		ElementType getElementType();
	}
}