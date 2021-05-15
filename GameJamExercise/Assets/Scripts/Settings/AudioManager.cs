using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    [Header("Audio Properties")]
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private string exposedParameter = "volume";

    [Header("Audio Values")]
    [SerializeField]
    private float minimumValue = 0.0001f;
    [SerializeField]
    private float maximumValue = 1f;
    [SerializeField]
    private float defaultValue = 0.75f;

    [Header("Save Data")]
    [SerializeField]
    private string saveKey = "AudioVolume";

    private void Awake() {

        if (!PlayerPrefs.HasKey(this.saveKey)) {
            PlayerPrefs.SetFloat(this.saveKey, this.defaultValue);
        }

        float value = PlayerPrefs.GetFloat(this.saveKey);
        this.audioMixer.SetFloat(this.exposedParameter, Mathf.Log10(value) * 20);
    }

}
