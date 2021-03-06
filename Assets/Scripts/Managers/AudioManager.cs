﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	[SerializeField] private Sound[] sounds;

	public static AudioManager Instance;

	private void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void Start() {
		foreach(var sound in sounds) {
			sound.source = gameObject.AddComponent<AudioSource>();
			sound.source.clip = sound.clip;

			sound.source.volume = sound.volume;
			sound.source.pitch = sound.pitch;
			sound.source.loop = sound.loop;
		}
		Play("Background Music");
		Play("Ambient Sounds");
	}

	public void Play(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null) {
			print(name + " not found");
			return;
		}		
		s.source.Play();
	}

	private void Pause(string name, bool enabled) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if(s == null) {
			print(name + " not found");
			return;
		}		
		if(enabled) {
			s.source.Pause();
		}
		else {
			s.source.UnPause();
		}
	} 

	public void OnPause(object src, EventArgs e) {
		Pause("Background Music", true);
		Pause("Ambient Sounds", true);
		Play("Pause");
	}

	public void OnUnPause(object src, EventArgs e) {
		Pause("Background Music", false);
		Pause("Ambient Sounds", false);
		Play("Pause");
	}
}
