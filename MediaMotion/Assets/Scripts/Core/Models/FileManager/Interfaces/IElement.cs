using System;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager.Interfaces {
	public interface IElement {
		ElementType GetElementType();

		string GetPath();

		string GetName();
	}
}