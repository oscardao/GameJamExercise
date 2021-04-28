using System.Collections.Generic;
using UnityEngine;

namespace OsukaCreative.Utility.GameEvent {

    [CreateAssetMenu(menuName = "Events/NoArg Event")]
    public class NoArgEvent : ScriptableObject {

        private readonly List<INoArgEventListener> eventListeners = new List<INoArgEventListener>();

        public void Raise() {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(INoArgEventListener listener) {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(INoArgEventListener listener) {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }

}
