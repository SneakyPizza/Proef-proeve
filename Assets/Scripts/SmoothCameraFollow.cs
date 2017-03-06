using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	[Range(0,10f)][SerializeField] private float followSpeed;
	[SerializeField] private Transform target;

	private Vector3 newPos;

	void Update()
	{
		if (target == null)
			return;

		FollowTarget(target);
	}

	private void FollowTarget(Transform target)
	{
		newPos = (new Vector3(0,0,-5) + target.position) / 2;
		newPos.z = -5f;
		float dist = Vector2.Distance(transform.position, newPos);
		if (dist > 0.05f)
		{
			Vector3 tempPos = transform.position;
			tempPos = Vector3.Lerp(tempPos, newPos, followSpeed * Time.smoothDeltaTime);
			tempPos.x = (float)System.Math.Round(tempPos.x, 2);
			tempPos.y = (float)System.Math.Round(tempPos.y, 2);
			transform.position = tempPos;
		}
	}

	public Transform SetTarget
	{
		get
		{
			return target;
		}
		set
		{
			if (target != value)
				target = value;
		}
	}
}
