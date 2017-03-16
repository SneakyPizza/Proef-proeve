using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {

	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private float _animationSpeed;

	private int currentSprite = 0;

	void OnEnable()
	{
		currentSprite = 0;
		if (_sprites.Length > 0)
		{
			_spriteRenderer.sprite = _sprites[0];
			Invoke("NextSprite", _animationSpeed);
		}
		else
			Debug.LogWarning("No sprites defined to this SpriteAnimation attached to " + this.gameObject.name);
	}

	void NextSprite()
	{
		currentSprite++;
		if (_sprites.Length - 1 >= currentSprite)
		{
			_spriteRenderer.sprite = _sprites[currentSprite];
		}
		else
		{
			_spriteRenderer.sprite = _sprites[0];
			currentSprite = 0;
		}
		Invoke("NextSprite", _animationSpeed);
	}

	void OnDisable()
	{
		CancelInvoke("NextSprite");
	}

	public float Speed
	{
		get
		{
			return _animationSpeed;
		}
		set
		{
			_animationSpeed = value;
		}
	}

	void Reset()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
}
