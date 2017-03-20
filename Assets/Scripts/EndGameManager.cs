using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameManager : MonoBehaviour {

	[SerializeField] private GameObject _winParticles;

	private PlayerRotation _playerRotation;

	void Awake()
	{
		_playerRotation = GetComponent<PlayerRotation>();
	}

	public IEnumerator EndGame(Player wonPlayer)
	{
		List<Player> players = _playerRotation.Players;

		foreach (Player player in players)
		{
			player.EndTurn();
		}

		List<List<GameObject>> playerTrailLists = new List<List<GameObject>>();

		for (int i = 0; i < players.Count; i++)
		{
			if (players[i] != wonPlayer)
			{
				playerTrailLists.Add(players[i].GetPlayerTrail.TrailChildren);
			}
		}

		int j = 0;
		for(int i = 0; i < playerTrailLists.Count; i++)
		{
			while (j < playerTrailLists[i].Count)
			{
				Destroy(playerTrailLists[i][j]);
				j++;
				yield return new WaitForSeconds(0.1f);
			}
			j = 0;
		}

		List<Node> traversedNodes = wonPlayer.TraversedNodes;
		traversedNodes.Reverse();

		for (int i = 0; i < traversedNodes.Count; i++)
		{
			traversedNodes[i].GetComponent<Renderer>().material.color = wonPlayer.PlayerColor;
			GameObject.Instantiate(_winParticles, traversedNodes[i].NodePos, Quaternion.identity);
			yield return new WaitForSeconds(0.2f);
		}

		//TODO : End Game Screen

		yield return new WaitForEndOfFrame();
	}
}
