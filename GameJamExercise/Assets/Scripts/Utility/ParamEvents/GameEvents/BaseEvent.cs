using System.Collections.Generic;
using UnityEngine;

namespace OsukaCreative.Utility.GameEvent {

    public abstract class BaseEvent<T> : ScriptableObject {

        //Listeners that will be notified when the event is raised.
        private readonly List<IEventListener<T>> eventListeners = new List<IEventListener<T>>();

        public void Raise(T t) {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(t);
        }

        public void RegisterListener(IEventListener<T> listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IEventListener<T> listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }

}
