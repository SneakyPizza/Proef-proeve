using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public static class SoundGenerator {

	public static void GenerateSound(AudioClip clip)
	{
		GameObject go = new GameObject(clip.name);
		AudioSource source = go.AddComponent<AudioSource>();
		AudioAsset asset = new AudioAsset();

	}
}
