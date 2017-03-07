using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private PlayerRotation playerRotation;
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject testPrefba;

	public void StartGame()
	{
		List<GameObject> playerSpawns = new List<GameObject>();
		List<Player> players = new List<Player>();

		foreach(GameObject objct in GameObject.FindGameObjectsWithTag("StartNode"))
		{
			playerSpawns.Add(objct);
		}

		for (int i = 0; i < 4; i++)
		{
			GameObject playerObject = GameObject.Instantiate(playerPrefab);
			Player player = playerObject.AddComponent<Player>();
			player.PlayerObject = playerObject;
			players.Add(player);
			player.PlayerID = i;
			int randomSpawn = Random.Range(0, playerSpawns.Count - 1);
			playerObject.transform.position = playerSpawns[randomSpawn].transform.position;
			playerSpawns.RemoveAt(randomSpawn);
		}
		testPrefba = players[0].gameObject;
		playerRotation.Players = players;
	}

	void Update()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Node");
		foreach(GameObject objcet in objects)
		{
			float dist = Vector2.Distance(testPrefba.transform.position, objcet.transform.position);
			if (dist < 1.9f && dist > 1.05f)
				objcet.GetComponent<Renderer>().material.color = Color.cyan;
			else if (objcet.GetComponent<Renderer>().material.color != Color.white)
				objcet.GetComponent<Renderer>().material.color = Color.white;
		}
	}
}
