using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private PlayerRotation playerRotation;
	[SerializeField] private GameObject playerPrefab;

	public void StartGame()
	{
		List<Player> players = new List<Player>();
		for (int i = 0; i < 4; i++)
		{
			GameObject playerObject = GameObject.Instantiate(playerPrefab);
			Player player = playerObject.AddComponent<Player>();
			player.PlayerObject = playerObject;
			players.Add(player);
		}
		playerRotation.Players = players;
	}
}
