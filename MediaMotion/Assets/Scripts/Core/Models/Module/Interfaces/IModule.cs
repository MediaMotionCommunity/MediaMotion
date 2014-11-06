namespace MediaMotion.Core.Models.Module.Interfaces {
	public interface IModule {
		void Register();
		void Unregister();

		void Load();
		void Unload();
	}
}