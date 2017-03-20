using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour {

	[SerializeField] private Material _trailMaterial;
	private Transform _trailParent;
	private Color _trailColor = Color.white;

	void Start()
	{
		_trailParent = new GameObject("Trails").transform;
		_trailParent.SetParent(this.gameObject.transform);
	}

	public IEnumerator MakeTrail(Transform playerPos, Vector3 nodePos, float speed)
	{
		GameObject trailObject = new GameObject("Trail " + _trailParent.childCount.ToString());
		LineRenderer lineRenderer = trailObject.AddComponent<LineRenderer>();
		lineRenderer.material = _trailMaterial;
		lineRenderer.startColor = _trailColor;
		lineRenderer.endColor = _trailColor;
		Vector3 newPos = (playerPos.position + nodePos) / 2;
		trailObject.transform.position = newPos;

		trailObject.transform.SetParent(_trailParent);
		lineRenderer.SetPosition(0, playerPos.position);

		float lineWidth = 0.1f;
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;

		while (playerPos.position != nodePos)
		{
			lineRenderer.SetPosition(1, playerPos.position);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}

	public Color TrailColor
	{
		set
		{
			_trailColor = value;
		}
	}

	public List<GameObject> TrailChildren
	{
		get
		{
			List<GameObject> children = new List<GameObject>();
			Transform[] ts = _trailParent.GetComponentsInChildren<Transform>();

			foreach(Transform transform in ts)
			{
				if (transform != _trailParent)
					children.Add(transform.gameObject);
			}
			return children;
		}
	}
}