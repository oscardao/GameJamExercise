using OsukaCreative.Utility.Variables;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace OsukaCreative.TheDayIRice.UI {

    public class AudioSettings : MonoBehaviour {

        [SerializeField]
        private Slider slider;

        [Header("Audio")]
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private string exposedParameter = "volume";
        [SerializeField]
        private FloatReference audioVolumeLevel;

        private void Start() {
            this.slider.value = this.audioVolumeLevel.Value;
            this.slider.onValueChanged.AddListener(SetVolume);
        }

        public void SetVolume(float value) {
            this.audioVolumeLevel.Value = value;
            this.audioMixer.SetFloat(this.exposedParameter, Mathf.Log10(value) * 20);
        }
    }

}