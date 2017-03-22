using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private float _moveSpeed;
	[SerializeField] private string _animPath;

	private int _playerID;

	private bool _trapped = false;
	private bool _canWalk = false;
	private bool _hasWalked = false;
	private bool _boostActive = false;

	private string _playerName;

	private Grid _grid;
	private Color _playerColor;
	private PlayerTrail _playerTrail;
	private GameObject _playerObject;
	private List<Node> _nearbyNodes = new List<Node>();
	private SpriteAnimation _spriteAnimation;

	private List<Node> _traversedNodes = new List<Node>();
    private Node _currentNode;

	void Awake()
	{
		_spriteAnimation = GetComponentInChildren<SpriteAnimation>();
		_playerTrail = GetComponent<PlayerTrail>();
		_spriteAnimation.enabled = false;
	}

	void Start()
	{
		_playerTrail.TrailColor = _playerColor;
		Sprite[] sprites = Resources.LoadAll<Sprite>(_animPath);
		List<Sprite> spriteList = new List<Sprite>();

		for (int i = _playerID * 6; i < (1 + _playerID) * 6; i++)
		{
			spriteList.Add(sprites[i]);
		}
		_spriteAnimation.SpriteArray = spriteList.ToArray();
		GetComponentInChildren<SpriteRenderer>().sprite = spriteList[0];
	}

	void Update()
	{
        if (_trapped)
        Debug.Log(this.gameObject.name + " Trapped = " + _trapped);
		if (Input.GetMouseButtonDown(0) && (_canWalk && !_hasWalked))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if ((hit.collider != null) && (hit.collider.tag == Tags.NODE || hit.collider.tag == Tags.ENDNODE))
			{
                if (_currentNode.NeighbourNodes.Contains(hit.collider.GetComponent<Node>()))
                {
                    if (!hit.collider.GetComponent<Node>().HasPlayer)
                    {
                        _hasWalked = true;
                        ColourNearbyNodes(false);
                        StopCoroutine("MoveToNode");
                        StartCoroutine(MoveToNode(hit.collider.GetComponent<Node>()));
                        if (hit.collider.tag == Tags.ENDNODE)
                        {
                            StartCoroutine(GameObject.FindWithTag(Tags.GAMECONTROLLER).GetComponent<EndGameManager>().EndGame(this));
                        }
                    }
                }
			}
		}
	}
    
	public void EnableTurn()
	{
		_spriteAnimation.GetSpriteRenderer.sortingOrder += 10;
		_spriteAnimation.enabled = true;
		if (_trapped)
		{
			EndTurn();
			_trapped = false;
			return;
		}

		_canWalk = true;
		_hasWalked = false;

		GetNearbyNodes();
		ColourNearbyNodes(true);
	}

	public void EndTurn()
	{
		_spriteAnimation.GetSpriteRenderer.sortingOrder -= 10;
		_spriteAnimation.enabled = false;
		_canWalk = false;
		_hasWalked = true;

		if (_boostActive)
			_boostActive = false;

		GetNearbyNodes();
		ColourNearbyNodes(false);
	}

	private void GetNearbyNodes()
	{
		List<Node> nodeList = _grid.GetNodeList();
		for(int i = 0; i < nodeList.Count; i++)
		{
			if (transform.position == nodeList[i].NodePos)
			{
                _currentNode = nodeList[i];
				_nearbyNodes = new List<Node>(nodeList[i].NeighbourNodes);
				break;
			}
		}
	}

	private void ColourNearbyNodes(bool startOfTurn)
	{
		if (_nearbyNodes != null)
		{
			foreach(Node nearbyNode in _nearbyNodes)
			{
				if (startOfTurn)
				{
					if (!nearbyNode.HasPlayer)
					{
						nearbyNode.GetComponent<Renderer>().material.color = Color.cyan;
					}
				}
				else
				{
					_nearbyNodes = null;
					nearbyNode.GetComponent<Renderer>().material.color = Color.white;
				}
			}
		}
	}

	private IEnumerator MoveToNode(Node node)
	{
		List<Node> nodeList = _grid.GetNodeList();
		for(int i = 0; i < nodeList.Count; i++)
		{
			if (transform.position == nodeList[i].NodePos)
			{
				nodeList[i].HasPlayer = false;
				break;
			}
		}

		_traversedNodes.Add(node);

		StartCoroutine(_playerTrail.MakeTrail(transform, node.transform.position, 1f * Time.deltaTime));
		while (Vector2.Distance(transform.position, node.transform.position) > 0.05f)
		{
			transform.position = Vector2.MoveTowards(transform.position, node.transform.position, _moveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		transform.position = node.transform.position;

		node.HasPlayer = true;

		if (node.Occupied)
		{
			_trapped = true;
			if (_boostActive)
			{
				_boostActive = false;
				_trapped = false;
			}
		}

		if (_boostActive)
		{
			_boostActive = false;
			ActivateBoost();
		}

		yield return new WaitForEndOfFrame();
	}

	private void ActivateBoost()
	{
		if (_hasWalked)
			_boostActive = false;

		if (_trapped)
		{
			_trapped = false;
			_boostActive = false;
		}
		
		_canWalk = true;
		_trapped = false;
		_hasWalked = false;
		GetNearbyNodes();
		ColourNearbyNodes(true);
	}

	public Grid SetGrid
	{
		set
		{
			_grid = value;
		}
	}
	public int PlayerID
	{
		get
		{
			return _playerID;
		}
		set
		{
			_playerID = value;
		}
	}

	public GameObject PlayerObject
	{
		get
		{
			return _playerObject;
		}
		set
		{
			_playerObject = value;
		}
	}

	public Color PlayerColor
	{
		get
		{
			return _playerColor;
		}
		set
		{
			_playerColor = value;
		}
	}

	public bool Trapped
	{
		get
		{
			return _trapped;
		}
		set
		{
			_trapped = value;
		}
	}
	public bool BoostActive
	{
		get
		{
			return _boostActive;
		}
		set
		{
			_boostActive = value;
			if (_boostActive)
				ActivateBoost();
		}
	}
	public PlayerTrail GetPlayerTrail
	{
		get
		{
			return _playerTrail;
		}
	}
	public List<Node> TraversedNodes
	{
		get
		{
			return _traversedNodes;
		}
	}
}
