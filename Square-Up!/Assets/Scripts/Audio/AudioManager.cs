using UnityEngine.Audio;
using UnityEngine;
using System;

/*This script will manage the audio of the scenes*/

public class AudioManager : MonoBehaviour {

    [SerializeField] private Sound[] sounds;

    void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Unknown sound file: " + name);
            return;
        }
        s.source.Play();
    }
}
