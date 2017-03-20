using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cards = new GameObject[4];
    private GameObject _newCard;
    private int _randomCard;
    private PlayerRotation _playerRotation;
    private List<List<GameObject>> PlayerDecks = new List<List<GameObject>>(4);
    private Card _card;
    private Player _player;
    [SerializeField] private bool _cardSelected = false;


    /// <summary>
    /// Get a random card from the array and return the drawn card
    /// </summary>

    private void Start()
    {
        _playerRotation = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if ((hit.collider != null))
            {
                if ((!_cardSelected) && hit.collider.tag == "Card")
                {
                    Debug.Log("One");
                    _cardSelected = true;
                    _card = hit.collider.GetComponent<Card>();
                    _card.SelectCard();
                    return;
                }

                if ((_cardSelected) && hit.collider.tag == "Player" && _card.CardSelected)
                {
                    Debug.Log("Two");
                    _card = hit.collider.GetComponent<Card>();
                    _player = _playerRotation.Players[_playerRotation.CurrentPlayer];
                    _card.ActivateSelf(_player);
                    return;
                }

                else if ((_cardSelected) && _card.CardSelected && hit.collider.tag == "Node" || hit.collider.tag == "Enemy")
                {
                    Debug.Log("Three");
                    _card = hit.collider.GetComponent<Card>();
                    _card.ActivateOther(_player);
                    return;
                }
            }
            else
            {
                _cardSelected = false;
            }         
        }   
    }

    public void DrawCard()
    {
        _randomCard = Random.Range(0, _cards.Length);
        _newCard = _cards[_randomCard];

        Debug.Log(_newCard);
        //Removes first card if more than 4 cards are in your hand
        PlayerDecks[_playerRotation.CurrentPlayer].Add(_newCard);

        if(PlayerDecks[_playerRotation.CurrentPlayer].Count >= 5)
        {
            PlayerDecks[_playerRotation.CurrentPlayer].RemoveAt(0);
        }
    }

    public GameObject NewCard
    {
        get
        {
            return _newCard;
        }
    }
}
