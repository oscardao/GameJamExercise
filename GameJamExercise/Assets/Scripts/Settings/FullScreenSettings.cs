using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OsukaCreative.TheDayIRice.UI {

    public class FullScreenSettings : MonoBehaviour {

        [SerializeField]
        private Toggle toggle;

        private void Start() {
            this.toggle.isOn = Screen.fullScreen;
            this.toggle.onValueChanged.AddListener(SetFullscreen);
        }

        private void OnEnable() {
            this.toggle.interactable = true;
        }

        private void OnDisable() {
            this.toggle.interactable = false;
        }

        public void SetFullscreen(bool isFullscreen) {
            Debug.Log("Setting to: " + isFullscreen);
            Debug.Log("Before: " + Screen.fullScreen);

            if (isFullscreen) {
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            } else {
                Screen.fullScreenMode = FullScreenMode.Windowed;
            }

            Debug.Log("Now: " + Screen.fullScreen);
        }
    }

}
