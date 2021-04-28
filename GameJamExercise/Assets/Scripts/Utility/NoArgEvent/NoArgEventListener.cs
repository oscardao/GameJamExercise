using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OsukaCreative.Utility.GameEvent {

    public class NoArgEventListener : MonoBehaviour, INoArgEventListener {

        [Tooltip("Event to register with.")]
        public NoArgEvent Event;

        [Tooltip("Response to invoke when Event is raised."), SerializeField]
        private UnityEvent Response = null;

        private void OnEnable() {
            Event.RegisterListener(this);
        }

        private void OnDisable() {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised() {
            Response.Invoke();
        }


    }

}
