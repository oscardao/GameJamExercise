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

        [Header("Save Data")]
        [SerializeField]
        private string saveKey = "AudioVolume";

        private void Start() {
            this.slider.value = PlayerPrefs.GetFloat(this.saveKey);
            this.slider.onValueChanged.AddListener(SetVolume);
        }

        public void SetVolume(float value) {
            Debug.Log(value + "     " + Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat(saveKey, value);
            this.audioMixer.SetFloat(this.exposedParameter, Mathf.Log10(value) * 20);
        }
    }

}