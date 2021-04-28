using UnityEngine;

namespace OsukaCreative.Utility.GameEvent {

    public class NoArgEventRaiser : MonoBehaviour {

        [SerializeField]
        private NoArgEvent[] events;

        public void RaiseEvents() {
            for (int i = 0; i < this.events.Length; i++) {
                this.events[i].Raise();
            }
        }

    }

}
