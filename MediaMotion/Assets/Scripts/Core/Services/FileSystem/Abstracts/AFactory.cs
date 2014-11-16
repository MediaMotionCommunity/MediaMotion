using System;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Abstracts {
	abstract public class AFactory {
		public string GetName(string Path) {
			int index;

			return (((index = Math.Max(Path.LastIndexOf('/'), Path.LastIndexOf('\\'))) >= 0) ? (Path.Substring(index)) : (Path));
		}

		public string GetExtension(string Name) {
			int index;

			return (((index = Name.LastIndexOf('.')) >= 0) ? (Name.Substring(index + 1).ToLower()) : (""));
		}
	}
}
