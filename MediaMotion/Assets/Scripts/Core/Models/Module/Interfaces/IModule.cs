using MediaMotion.Core.Models.Wrapper.Events;

namespace MediaMotion.Core.Models.Module.Interfaces {
	public interface IModule {
        void ActionHandle(object Sender, ActionDetectedEventArgs Action);

		void Register();
		void Unregister();

		void Load();
        void Unload();
    }
}