using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	[Range(0,10f)][SerializeField] private float _followSpeed;
	[SerializeField] private Vector2 _minPos = new Vector2(), _maxPos = new Vector2();
	[SerializeField] private float _maxDist = 0.05f;

	[SerializeField] private float _zoomSpeed;

	private Transform _target;
	private Vector3 newPos;

	void OnEnable()
	{
		StartCoroutine(Zoom());
	}

	private IEnumerator Zoom()
	{
		Camera cam = GetComponent<Camera>();

		cam.orthographicSize = 10.75f;

		while (cam.orthographicSize != 6.5f)
		{
			cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 6.5f, _zoomSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}

	void Update()
	{
		if (_target == null)
			return;

		FollowTarget(_target);
	}

	private void FollowTarget(Transform target)
	{
		newPos = (new Vector3(0,0,-5) + target.position * 1.25f) / 2;
		newPos.z = -5f;
		float dist = Vector2.Distance(transform.position, newPos);
		if (dist > _maxDist)
		{
			Vector3 tempPos = transform.position;
			tempPos = Vector3.Lerp(tempPos, newPos, _followSpeed * Time.smoothDeltaTime);
			tempPos.x = (float)System.Math.Round(tempPos.x, 2);
			tempPos.y = (float)System.Math.Round(tempPos.y, 2);
			transform.position = tempPos;
		}
		Vector3 clampPos = transform.position;
		clampPos.x = Mathf.Clamp(clampPos.x, _minPos.x, _maxPos.x);
		clampPos.y = Mathf.Clamp(clampPos.y, _minPos.y, _maxPos.y);
		transform.position = clampPos;
	}

	public Transform SetTarget
	{
		get
		{
			return _target;
		}
		set
		{
			if (_target != value)
				_target = value;
		}
	}
}
