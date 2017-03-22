using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundGenerator {

	public static void GenerateSound(AudioClip clip)
	{
		GameObject go = new GameObject(clip.name);
		AudioSource source = go.AddComponent<AudioSource>();
		source.clip = clip;
		source.loop = false;
		source.Play();

		GameObject.Destroy(go, clip.length);
	}
}
