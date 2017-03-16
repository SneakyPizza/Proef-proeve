using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cards = new GameObject[4];
    private List<GameObject> _CardHand = new List<GameObject>();

    private GameObject _newCard;
    private int _randomCard;
    private Card _card;
    private PlayerRotation _playerRotation;//player rotation om te checken welke player current is


    private void Start()
    {
        
    } 

    private void Update()
    {
       
    }

    public void DrawCard()
    {
        _randomCard = Random.Range(0, _cards.Length);
        _newCard = _cards[_randomCard];
        _CardHand.Add(_newCard);
        
        GameObject.Instantiate(_newCard);

        if(_CardHand.Count > 4)
        {
            _CardHand.RemoveAt(0);
        }
        Debug.Log(_newCard);
    }
}
