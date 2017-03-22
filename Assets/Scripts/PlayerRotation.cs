using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

	private int _currentPlayer = 1;

	private SmoothCameraFollow _cameraFollow;
	public List<Player> _players = new List<Player>();
    private CardManager _cardManager;

	void Awake()
	{
        _cardManager = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<CardManager>();
        _cameraFollow = Camera.main.GetComponent<SmoothCameraFollow>();

	}

	void Start()
	{
		_cameraFollow.SetTarget = _players[_currentPlayer].PlayerObject.transform;
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			StartCoroutine(EndTurn(0.25f));
		if (Input.GetKeyDown(KeyCode.A))
			_players[_currentPlayer].BoostActive = true;
	}

	public IEnumerator EndTurn(float waitTime)
	{

        GameObject[] goCards = GameObject.FindGameObjectsWithTag(Tags.CARD);
        Card[] cards = new Card[goCards.Length];
        foreach(Card card in cards)
        {
            if (card.CardSelected)
                card.CardSelected = false;
        }
        // _cardManager.CardToggler(CurrentPlayer, false);
        _players[_currentPlayer].EndTurn();
        

		yield return new WaitForSeconds(waitTime);
		_currentPlayer ++;

		if (_currentPlayer >= _players.Count)
			_currentPlayer = 0;
       // _cardManager.CardToggler(CurrentPlayer, true);
        
       
		_cameraFollow.SetTarget = _players[_currentPlayer].PlayerObject.transform;
		_players[_currentPlayer].EnableTurn();
        _cardManager.DrawCard(_currentPlayer);

        yield return new WaitForEndOfFrame();
	}

	public List<Player> Players
	{
        get
        {
            return _players;
        }
		set 
		{
			_players = value;
		}
	}

    public int CurrentPlayer
    {
        get
        {
            return _currentPlayer;
        }
        set
        {
            _currentPlayer = value;
        }
    }
}
