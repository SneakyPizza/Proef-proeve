using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	[SerializeField] private float _fadeSpeed;
	[SerializeField] private List<SpriteRenderer> _onEnterGroup = new List<SpriteRenderer>();
	[SerializeField] private List<SpriteRenderer> _onPressGroup = new List<SpriteRenderer>();
	[SerializeField] private SpriteButton _playButton;

	void Start()
	{
		StartCoroutine(OnEnterFade());
	}

	private IEnumerator OnEnterFade()
	{
		Color newColor = _onEnterGroup[0].color;

		while (_onEnterGroup[0].color.a < 1)
		{
			foreach(SpriteRenderer ren in _onEnterGroup)
			{
				newColor.a = Mathf.MoveTowards(newColor.a, 1, _fadeSpeed * Time.deltaTime);
				ren.color = newColor;
			}
			yield return new WaitForEndOfFrame();
		}
		_playButton.enabled = true;
		yield return new WaitForEndOfFrame();
	}

	public void ActivatePlayMenu()
	{
		_playButton.enabled = false;
		StartCoroutine(ToPlayMenu());
	}

	private IEnumerator ToPlayMenu()
	{
		Color newColorGroupOne = _onEnterGroup[0].color;
		Color newColorGroupTwo = _onPressGroup[0].color;

		while (_onEnterGroup[0].color.a > 0)
		{
			foreach(SpriteRenderer ren in _onEnterGroup)
			{
				newColorGroupOne.a = Mathf.MoveTowards(newColorGroupOne.a, 0, _fadeSpeed * Time.deltaTime);
				ren.color = newColorGroupOne;
			}
			foreach(SpriteRenderer ren in _onPressGroup)
			{
				newColorGroupTwo.a = Mathf.MoveTowards(newColorGroupTwo.a, 1, _fadeSpeed * Time.deltaTime);
				ren.color = newColorGroupTwo;
			}
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}
}
