using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSpawner : MonoBehaviour {

	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private Sprite[] _playerSprites;
	[SerializeField] private Color[] _playerColors;

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
			player.PlayerColor = _playerColors[i];
			playerObject.name = "Player " + (1 + i).ToString();
			playerObject.tag = Tags.PLAYER;

			int randomSpawn = Random.Range(0, playerSpawns.Count - 1);
			playerObject.transform.position = playerSpawns[randomSpawn].transform.position;
			playerSpawns.RemoveAt(randomSpawn);
		}

		_playerRotation.Players = players;
	}
}
