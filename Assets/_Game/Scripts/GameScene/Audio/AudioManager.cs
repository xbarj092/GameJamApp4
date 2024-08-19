using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    public Sound[] sounds;
    [SerializeField] private bool SpacialBlend;
    [SerializeField] private AudioMixerGroup mixer;
    
    void Awake()
    {
        foreach (Sound s in sounds) {
            for(int i = 0; i < s.NumberOfSource; i++) { 
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.playOnAwake = false;
                source.loop = false;
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.spatialBlend = s.SpatialBlend;
                source.outputAudioMixerGroup = mixer;
                s.source.Add(source);
            }
        }
    }

    public void Play(SoundType name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        bool find = false;

        AudioSource foundSource = s.source.Find((source) => {
            find = !source.isPlaying;
            return find;
        });

        if (foundSource != null)
        {
            foundSource.Play();

            if (name == SoundType.CoreDamaged)
            {
                foundSource.pitch *= 0.8f;
            }
        }
        else
        {
            s.source[0].Play();

            if (name == SoundType.CoreDamaged)
            {
                s.source[0].pitch *= 0.8f;
            }
        }
    }

    public void Stop(SoundType name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.ForEach((source) => source.Stop());
    }

    public bool NewIsPlaying(SoundType name) { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.Exists((source) => source.isPlaying);
    }
}
