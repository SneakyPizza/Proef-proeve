using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private Sprite[] _playerSprites;

	private Grid _grid;
	private PlayerRotation _playerRotation;

	void Awake()
	{
		_grid = GetComponent<Grid>();
		_playerRotation = GetComponent<PlayerRotation>();
	}

	public void StartGame()
	{
		List<GameObject> playerSpawns = new List<GameObject>();
		List<Player> players = new List<Player>();

		foreach(GameObject objct in GameObject.FindGameObjectsWithTag(Tags.STARTNODE))
		{
			playerSpawns.Add(objct);
		}

		for (int i = 0; i < 4; i++)
		{
			GameObject playerObject = GameObject.Instantiate(_playerPrefab);
			Player player = playerObject.GetComponent<Player>();
			player.PlayerObject = playerObject;
			players.Add(player);
			player.PlayerID = i;
			player.SetGrid = _grid;
			playerObject.name = "Player " + (1 + i).ToString();
			playerObject.tag = Tags.PLAYER;
			playerObject.GetComponentInChildren<SpriteRenderer>().sprite = _playerSprites[i];
			int randomSpawn = Random.Range(0, playerSpawns.Count - 1);
			playerObject.transform.position = playerSpawns[randomSpawn].transform.position;
			playerSpawns.RemoveAt(randomSpawn);
		}

		_playerRotation.Players = players;
		players[0].EnableTurn();
	}
}
