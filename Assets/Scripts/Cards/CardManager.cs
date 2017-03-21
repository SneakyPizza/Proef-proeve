using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _cardPositions = new List<Transform>();
    [SerializeField] private List<Sprite> _cardSprites = new List<Sprite>();

    [SerializeField] private GameObject[] _cards = new GameObject[2];

    [SerializeField] private GameObject _cardPrefab;

    private List<List<GameObject>> PlayerDecks = new List<List<GameObject>>(4);
    private List<GameObject> _Deck = new List<GameObject>(4);

    private GameObject _newCard;
    private int _randomCard;
    private PlayerRotation _playerRotation;
    private Card _card;
    private Player _player;
    private bool _cardSelected = false;


    /// <summary>
    /// Get a random card from the array and return the drawn card
    /// </summary>

    private void Start()
    {
        _playerRotation = GameObject.FindGameObjectWithTag(Tags.GAMECONTROLLER).GetComponent<PlayerRotation>();
        
        foreach(Player _player in _playerRotation.Players)
        {
            PlayerDecks.Add(_Deck);
        }
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

    public void DrawCard(int currentPlayer)
    {
        GameObject cardObject = GameObject.Instantiate(_cardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        _randomCard = Random.Range(0, _cards.Length);
        switch(_randomCard)
        {
            case (0):
            {
                    cardObject.AddComponent<BoostC>();
                break;
            }
            case (1):
            {
                    cardObject.AddComponent<TrapC>();
                break;
            }
        }
        // Check doormiddel van Currentplayer waar de kaart moet worden geplaatst

        if (PlayerDecks[currentPlayer].Count >= 4)
        {
            GameObject toDestroy = PlayerDecks[_playerRotation.CurrentPlayer][0];
            PlayerDecks[_playerRotation.CurrentPlayer].RemoveAt(0);
            Destroy(toDestroy);
        }
        _newCard = _cards[_randomCard];
        cardObject.transform.position = _cardPositions[(4 * currentPlayer) + PlayerDecks[currentPlayer].Count].position;

        Debug.Log(_newCard);
        //Removes first card if more than 4 cards are in your hand
        //Deck[_playerRotation.CurrentPlayer].Add(_newCard);
        PlayerDecks[currentPlayer].Add(cardObject);

    }

    public void CardToggler(int CurrentPlayer, bool Active)
    {
        foreach(GameObject card in PlayerDecks[CurrentPlayer])
        {
            card.SetActive(Active);
        }
    }
     
    public GameObject NewCard
    {
        get
        {
            return _newCard;
        }
    }
    public List<GameObject> Deck
    {
        get
        {
            return _Deck;
        }
    }
}
