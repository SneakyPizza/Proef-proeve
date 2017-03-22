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
    private int[] _counter = new int[4];
    //private List<GameObject> _Deck = new List<GameObject>(4);

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
        _player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Player>();

        foreach(Player _player in _playerRotation.Players)
        {
            List<GameObject> deck = new List<GameObject>(4);
            PlayerDecks.Add(deck);
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
                    _cardSelected = true;
                    _card = hit.collider.GetComponent<Card>();
                    _card.SelectCard();
                    return;
                }
                else if((_cardSelected) && hit.collider.tag == "Card")
                {
                    _cardSelected = false;
                    _card.DeactivateCard();
                }

                if ((_cardSelected) && hit.collider.tag == "Player" && _card.CardSelected)
                {
                    _card.DeactivateCard();
                    if (hit.collider.GetComponent<Player>())
                    {
                        _card.ActivateSelf(hit.collider.GetComponent<Player>());
                    }
                    _cardSelected = false;
                    return;
                }

                else if ((_cardSelected) && _card.CardSelected && hit.collider.tag == "Node")
                {
                    //_card = hit.collider.GetComponent<Card>();
                    _card.DeactivateCard();
                    if (hit.collider.GetComponent<Player>())
                    {
                        _card.ActivateOther(hit.collider.GetComponent<Player>());
                    }
                    else if (hit.collider.GetComponent<Node>())
                    {
                        _card.ActivateOther(null, hit.collider.GetComponent<Node>());
                    }
                    _cardSelected = false;
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
              cardObject.AddComponent<TrapC>();  
              break;
            }
            case (1):
            {
               cardObject.AddComponent<BoostC>();
               break;
            }
        }
        //Removes first card if more than 4 cards are in your hand
        _newCard = cardObject;
        _newCard = _cards[_randomCard];

        if (PlayerDecks[currentPlayer].Count >= 4)
        {
            GameObject toDestroy = PlayerDecks[_playerRotation.CurrentPlayer][_counter[currentPlayer]];
            Destroy(toDestroy);
            PlayerDecks[currentPlayer][_counter[currentPlayer]] = _newCard;
            _counter[currentPlayer]++;

            if(_counter[currentPlayer] >= 4)
            {
                _counter[currentPlayer] = 0;
            }
        }
        else
        {
            PlayerDecks[currentPlayer].Add(cardObject);
        }
       
        cardObject.transform.position = _cardPositions[(4 * currentPlayer) + PlayerDecks[currentPlayer].Count].position;
        cardObject.name = cardObject.GetComponent<Card>().GetType().ToString();
        Debug.Log(_newCard);
       
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
            return null;
        }
    }
}
