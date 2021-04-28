
namespace OsukaCreative.Utility.GameEvent {

    public interface IEventListener<T> {
        void OnEventRaised(T t);
    }

}


