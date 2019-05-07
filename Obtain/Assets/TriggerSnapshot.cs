using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerSnapshot : MonoBehaviour {

    public AudioMixer mixer;
    public AudioMixerSnapshot targetSnapshot;
    public AudioMixerSnapshot defaultSnapshot;

    private void OnTriggerEnter(Collider other) {
        targetSnapshot.TransitionTo(1);
    }
    private void OnTriggerExit(Collider other) {
        defaultSnapshot.TransitionTo(1);
    }
}
