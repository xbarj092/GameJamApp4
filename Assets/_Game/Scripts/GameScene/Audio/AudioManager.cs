using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    [SerializeField] private bool SpacialBlend;
    [SerializeField] private AudioMixerGroup mixer;
    // Start is called before the first frame update
    
    static private AudioManager activeInstance = null;
    static public AudioManager ActiveIntance => activeInstance;

    void Awake()
    {
        if(activeInstance == null)
            activeInstance = this;
        else {
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound s in sounds) {
            for(int i = 0; i < s.NumberOfSource; i++) { 
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.spatialBlend = s.SpatialBlend;
                source.outputAudioMixerGroup = mixer;
                s.source.Add(source);
            }
        }
    }

    // Update is called once per frame
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        bool find = false;
        s.source.Find((source) => { find = !source.isPlaying; return find; }).Play();
        if(!find) {
            s.source[0].Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.ForEach((source) => source.Stop());
    }

    public bool newIsPlaying(string name) { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.Exists((source) => source.isPlaying);
    }
    
    
}
