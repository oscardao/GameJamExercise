using UnityEngine;
using UnityEngine.Events;

namespace OsukaCreative.Utility.GameEvent {

    public abstract class BaseEventListener<T, E, UE> : MonoBehaviour, IEventListener<T> where E : BaseEvent<T> where UE : UnityEvent<T> {

        [Tooltip("Event to register with.")]
        public E Event;

        [Tooltip("Response to invoke when Event is raised."), SerializeField]
        private UE Response = null;

        private void OnEnable() {
            Event.RegisterListener(this);
        }

        private void OnDisable() {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(T t) {
            Response.Invoke(t);
        }
    }

}
