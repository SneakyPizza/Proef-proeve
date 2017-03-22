using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {

	[SerializeField] private float _panSpeedSteps;
	[SerializeField] private float _minPanSpeed;
	[SerializeField] private float _currentPanSpeed;
	[SerializeField] private float _maxSpeed;
	[SerializeField] private float _fadeSpeed;

	[SerializeField] private List<SpriteRenderer> _onEnterGroup = new List<SpriteRenderer>();
	[SerializeField] private List<SpriteRenderer> _onPressGroup = new List<SpriteRenderer>();

	[SerializeField] private SpriteButton _playButton;
	[SerializeField] private SpriteButton _tutorialButton;
	[SerializeField] private SpriteButton _backToEntryButton;

	[SerializeField] private GameObject _smokeEffect;
	[SerializeField] private Renderer _smokeTexture;

	[SerializeField] private GameObject _mainCamera;
	[SerializeField] private GameObject _gameController;
	[SerializeField] private GameObject _objectToDestroy;
	[SerializeField] private GameObject _firstObjectToDestroy;

	private bool _moving = false;

	void Start()
	{
		StartCoroutine(ToEntryMenu());
	}

	private IEnumerator ToEntryMenu()
	{
		Color newColorGroupOne = _onEnterGroup[0].color;
		Color newColorGroupTwo = _onPressGroup[0].color;

		while (_onEnterGroup[0].color.a < 1)
		{
			foreach(SpriteRenderer ren in _onEnterGroup)
			{
				newColorGroupOne.a = Mathf.MoveTowards(newColorGroupOne.a, 1, _fadeSpeed * Time.deltaTime);
				ren.color = newColorGroupOne;
			}
			foreach(SpriteRenderer ren in _onPressGroup)
			{
				newColorGroupTwo.a = Mathf.MoveTowards(newColorGroupTwo.a, 0, _fadeSpeed * Time.deltaTime);
				ren.color = newColorGroupTwo;
			}
			yield return new WaitForEndOfFrame();
		}
		_playButton.enabled = true;
		_tutorialButton.enabled = true;
		yield return new WaitForEndOfFrame();
	}

	public void ActivatePlayMenu()
	{
		StartCoroutine(ToPlayMenu());
	}

	public void ActivateEntryMenu()
	{
		StartCoroutine(ToEntryMenu());
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
		_playButton.enabled = false;
		_tutorialButton.enabled = false;
		_backToEntryButton.enabled = true;

		yield return new WaitForEndOfFrame();
	}

	public void PanToScreen()
	{
		if (!_moving)
		{
			_moving = true;
			Vector3 desiredPos = Camera.main.transform.position;
			desiredPos.x = desiredPos.x == 0 ? 38.25f : 0f;
			StartCoroutine(PanToOtherScreen(desiredPos));
		}
	}

	private IEnumerator PanToOtherScreen(Vector3 desiredPos)
	{
		Vector3 camPos = Camera.main.transform.position;
		_currentPanSpeed = _minPanSpeed;
		while (camPos != desiredPos)
		{
			float dist = Vector2.Distance(Camera.main.transform.position, desiredPos);

			if (dist > 15.5f)
				_currentPanSpeed = Mathf.MoveTowards(_currentPanSpeed, _maxSpeed, Time.deltaTime * _panSpeedSteps);
			else
				_currentPanSpeed = Mathf.MoveTowards(_currentPanSpeed, _minPanSpeed, Time.deltaTime * _panSpeedSteps);
			
			camPos = Vector3.MoveTowards(camPos, desiredPos, _currentPanSpeed * Time.deltaTime);
			Camera.main.transform.position = camPos;
			yield return new WaitForEndOfFrame();
		}
		_moving = false;
		_currentPanSpeed = _minPanSpeed;
		yield return new WaitForEndOfFrame();
	}

	public void SelectPlayers(int amount)
	{
		_smokeEffect.SetActive(true);
		StartCoroutine(TransitionToGame());
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.S))
			SelectPlayers(0);
	}

	private IEnumerator TransitionToGame()
	{
		Color startColor = _smokeTexture.material.color;
		startColor.a = 0;
		_smokeTexture.material.color = startColor;
		while (_smokeTexture.material.color.a < 1)
		{
			startColor.a = Mathf.MoveTowards(startColor.a, 1, Time.deltaTime / 2);
			_smokeTexture.material.color = startColor;
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(3f);

		_gameController.SetActive(true);
		_mainCamera.SetActive(true);

		Destroy(_firstObjectToDestroy);

		Color newColor = _smokeTexture.material.color;
		newColor.a = 1;
		_smokeTexture.material.color = newColor;
		while (_smokeTexture.material.color.a > 0)
		{
			newColor.a = Mathf.MoveTowards(newColor.a, 0, Time.deltaTime / 2);
			_smokeTexture.material.color = newColor;
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForEndOfFrame();

		Destroy(_objectToDestroy);
	}
}
