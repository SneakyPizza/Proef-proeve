using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviePlayer : MonoBehaviour {

	[SerializeField] private Renderer _renderer;
	[SerializeField] private MovieTexture _movieTexture;
	[SerializeField] private bool _autoPlay;
	[SerializeField] private bool _loop;

	void Start()
	{
		if (_autoPlay)
		{
			PlayMovie();
		}
	}

	void Update()
	{		
		if (_loop && !_movieTexture.isPlaying)
		{
			PlayMovie();
		}
	}

	public void StartMovie()
	{
		if (!_movieTexture.isPlaying)
		{
			PlayMovie();
		}
	}

	private void PlayMovie()
	{
		_renderer.material.mainTexture = _movieTexture;
		_movieTexture.Stop();
		_movieTexture.Play();
	}
}
