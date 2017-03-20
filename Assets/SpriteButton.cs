using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteButton : MonoBehaviour {

	private Collider2D myCollider;
	private SpriteRenderer spriteRenderer;

	[SerializeField] private Button.ButtonClickedEvent clickEvent;
	[SerializeField] private Color clickedColor;
	private bool selected;

	void Start()
	{
		myCollider = GetComponent<Collider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		CheckEditorPosition(position);
	}
	private void CheckEditorPosition(Vector2 position)
	{
		if (Input.GetMouseButtonDown(0))
		{			
			if (myCollider.bounds.Contains(position))
			{
				selected = true;
			}
		}
		if ((selected) && Input.GetMouseButton(0))
		{
			if (!myCollider.bounds.Contains(position))
			{
				if (spriteRenderer.color != Color.white)
					spriteRenderer.color = Color.white;
			}else if (myCollider.bounds.Contains(position))
			{
				if (spriteRenderer.color != clickedColor)
					spriteRenderer.color = clickedColor;
			}
		}
		if ((selected) && Input.GetMouseButtonUp(0))
		{
			if (myCollider.bounds.Contains(position))
			{
				clickEvent.Invoke();
			}
			if (spriteRenderer.color != Color.white)
				spriteRenderer.color = Color.white;
			selected = false;
		}
	}
}
