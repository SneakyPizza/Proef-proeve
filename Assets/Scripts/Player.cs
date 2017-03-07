using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private int playerID;
	private string playerName;
	private GameObject playerObject;
	private Color playerColor;

	private bool canWalk;

	public int PlayerID
	{
		get
		{
			return playerID;
		}
		set
		{
			playerID = value;
		}
	}
	public bool CanWalk
	{
		get
		{
			return canWalk;
		}
		set
		{
			canWalk = value;
		}
	}
	public GameObject PlayerObject
	{
		set
		{
			playerObject = value;
		}
		get
		{
			return playerObject;
		}
	}
}
