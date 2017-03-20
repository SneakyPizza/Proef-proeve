using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestroyer : MonoBehaviour {

	private ParticleSystem _particleSystem;

	void Start()
	{
		_particleSystem = GetComponent<ParticleSystem>();
		float time = _particleSystem.main.startLifetime.constantMax;
		Destroy(this.gameObject, time);
	}
}
