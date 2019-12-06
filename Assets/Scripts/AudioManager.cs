using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager actualManger;

    private void Awake()
    {
        if(actualManger == null)
        {
            actualManger = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: "+name+" doesnt exists");
            return;
        }
        s.source.Play();
    }

    // Use this for initialization
    void Start () {
        Play("TemaPrincipal");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
