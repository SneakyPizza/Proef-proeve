using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cards = new GameObject[4];
    private GameObject _newCard;
    private int _randomCard;
    private Player _player;
    /// <summary>
    /// Get a random card from the array and return the drawn card
    /// </summary>
    public void DrawCard()
    {
        _randomCard = Random.Range(0, _cards.Length);
        _newCard = _cards[_randomCard];

        Debug.Log(_newCard);
        //Removes first card if more than 4 cards are in your hand
        if(_player.PlayerDeck.Count >= 4)
        {
            _player.PlayerDeck.RemoveAt(0);
        }
        _player.PlayerDeck.Add(_newCard);
    }

    public GameObject NewCard
    {
        get
        {
            return _newCard;
        }
    }
}
