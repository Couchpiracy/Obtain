using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TriggerSnapshot : MonoBehaviour {
    
    public AudioMixerSnapshot targetSnapshot;
    public AudioMixerSnapshot defaultSnapshot;
    public AudioMixerSnapshot targetSnapshotTwo;

    public void SwapSnapshot(AudioMixerSnapshot snapshot, float time) {
        snapshot.TransitionTo(time);
    }

    private void OnTriggerEnter(Collider other) {
        SwapSnapshot(targetSnapshot, 3.5f);
    }
    private void OnTriggerExit(Collider other) {
        SwapSnapshot(defaultSnapshot, 3.5f);
    }
}
