using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace OsukaCreative.TheDayIRice.UI {

    public class ResolutionSettings : MonoBehaviour {

        [SerializeField]
        private TMP_Dropdown dropDown;

        private Resolution[] resolutions;

        void Start() {
            int CurrentResolutionIndex = 0;
            this.resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

            this.dropDown.ClearOptions();

            List<string> options = new List<string>();

            for (int i = 0; i < this.resolutions.Length; i++) {
                options.Add(this.resolutions[i].width + " x " + this.resolutions[i].height);

                if (this.resolutions[i].width == Screen.width && this.resolutions[i].height == Screen.height) {
                    CurrentResolutionIndex = i;
                }
            }

            this.dropDown.AddOptions(options);
            this.dropDown.value = CurrentResolutionIndex;
            this.dropDown.RefreshShownValue();

            this.dropDown.onValueChanged.AddListener(SetResolution);
        }

        public void SetResolution(int ResolutionIndex) {
            Resolution resolution = resolutions[ResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

    }
}