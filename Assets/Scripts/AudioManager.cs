using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        public float volume = 1f;
        public float pitch = 1f;
        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    void Awake()
    {
        // Removed singleton and DontDestroyOnLoad

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        // Optional: Play default music
        // Play("Music");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) s.source.Play();
        else Debug.LogWarning("Sound not found: " + name);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null) s.source.Stop();
    }

    public void MuteAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = true;
        }
    }

    public void UnmuteAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = false;
        }
    }
}
