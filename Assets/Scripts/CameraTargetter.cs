using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetter : MonoBehaviour {
	

	public IEnumerator Target(GameObject target, float zoomOutTime, float zoomInTime)
	{
		Vector3 targetPos = new Vector3(0,0, -10f);
		targetPos.z = -10f;

		while (Vector3.Distance(transform.position, targetPos) > 0.25f)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetPos, zoomOutTime * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		targetPos = target.transform.position / 2;
		targetPos.z = -10;

		while (Vector3.Distance(transform.position, targetPos) > 0.25f)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetPos, zoomInTime * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();
	}
}
