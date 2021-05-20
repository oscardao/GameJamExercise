using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerIndicator : MonoBehaviour {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] sounds;

    private void Start() {
        AudioClip clip = this.sounds[Random.Range(0, this.sounds.Length)];
        this.audioSource.PlayOneShot(clip);
    }

}
