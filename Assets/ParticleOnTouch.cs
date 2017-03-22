using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnTouch : MonoBehaviour {

	[SerializeField] private GameObject _particle;
	[SerializeField] private Collider2D _myCollider;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);				
			if (_myCollider.bounds.Contains(position))
			{
				GameObject.Instantiate(_particle, position, Quaternion.identity);
			}
		}
	}
}
