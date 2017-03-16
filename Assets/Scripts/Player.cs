using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private int playerID;
	private string playerName;
	private GameObject playerObject;
	private Color playerColor;

	[SerializeField] private bool canWalk;

    public List<GameObject> Deck = new List<GameObject>();

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
    
	public void ToggleTurn()
	{
		canWalk = canWalk == true ? false : true;
	}


}
