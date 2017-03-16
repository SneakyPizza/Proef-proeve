using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour {

	[SerializeField] private Material _trailMaterial;
	private Transform _trailParent;

	void Start()
	{
		_trailParent = new GameObject("Trails").transform;
		_trailParent.SetParent(this.gameObject.transform);
	}

	public IEnumerator MakeTrail(Transform playerPos, Vector3 nodePos, float speed)
	{
		GameObject trailObject = new GameObject("Trail " + _trailParent.childCount.ToString());
		LineRenderer lineRenderer = trailObject.AddComponent<LineRenderer>();
		lineRenderer.startWidth = 0;
		lineRenderer.endWidth = 0;
		lineRenderer.material = _trailMaterial;
		Vector3 newPos = (playerPos.position + nodePos) / 2;
		trailObject.transform.position = newPos;

		trailObject.transform.SetParent(_trailParent);
		lineRenderer.SetPosition(0, playerPos.position);

		float dist = Vector2.Distance(playerPos.position, nodePos);
		float endSize = dist > 1.40f? 0.6f : 1f;

		while (playerPos.position != nodePos)
		{
			if (lineRenderer.startWidth < endSize)
			{
				lineRenderer.startWidth += (speed * endSize);
				lineRenderer.endWidth += (speed * endSize);
			}

			lineRenderer.SetPosition(1, playerPos.position);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}
}