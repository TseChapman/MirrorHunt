using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_sounds;

    private int m_numSounds;
    private int m_musicIndex = 0;

    private Sound m_currentMusic;

    public void Play(string name)
    {
        Sound s = Array.Find(m_sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(m_sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }

    public bool GetIsSoundPlaying(string name)
    {
        Sound s = Array.Find(m_sounds, sound => sound.name == name);
        if (s == null)
            return true;
        return s.source.isPlaying;
    }

    private void Awake()
    {
        foreach (Sound s in m_sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLoop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_numSounds = m_sounds.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
