using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private float _moveSpeed;

	private int _playerID;

	private bool _trapped = false;
	private bool _canWalk = false;
	private bool _hasWalked = false;
	private bool _boostActive = false;

	private string _playerName;

	private Grid _grid;
	private Color _playerColor;
	private SpriteOutline _spriteOutline;
	private PlayerTrail _playerTrail;
	private GameObject _playerObject;
	private List<Node> _nearbyNodes = new List<Node>();

    private List<GameObject> _playerDeck = new List<GameObject>();

	void Awake()
	{
		_spriteOutline = GetComponentInChildren<SpriteOutline>();
		_playerTrail = GetComponent<PlayerTrail>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && (_canWalk && !_hasWalked))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

			if ((hit.collider != null) && (hit.collider.tag == Tags.NODE || hit.collider.tag == Tags.ENDNODE))
			{
				_hasWalked = true;
				ColourNearbyNodes(false);
				StopCoroutine("MoveToNode");
				StartCoroutine(MoveToNode(hit.collider.GetComponent<Node>()));
			}
		}
		if (_trapped)
			Debug.Log(this.gameObject.name + " Trapped!");
	}
    
	public void EnableTurn()
	{
        //get card (id)
		_spriteOutline.enabled = true;
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
		_spriteOutline.enabled = false;
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
		Node node = null;
		for(int i = 0; i < nodeList.Count; i++)
		{
			if (transform.position == nodeList[i].NodePos)
			{
				node = nodeList[i];
				_nearbyNodes = node.NeighbourNodes;
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
					nearbyNode.GetComponent<Collider2D>().enabled = true;
					nearbyNode.GetComponent<Renderer>().material.color = Color.cyan;
				}
				else
				{
					nearbyNode.GetComponent<Collider2D>().enabled = false;
					_nearbyNodes = null;
					nearbyNode.GetComponent<Renderer>().material.color = Color.white;
				}
			}
		}
	}

	private IEnumerator MoveToNode(Node node)
	{
		StartCoroutine(_playerTrail.MakeTrail(transform, node.transform.position, 1f * Time.deltaTime));
		while (Vector2.Distance(transform.position, node.transform.position) > 0.05f)
		{
			transform.position = Vector2.MoveTowards(transform.position, node.transform.position, _moveSpeed * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		transform.position = node.transform.position;

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

    public List<GameObject> PlayerDeck
    {
        get
        {
            return _playerDeck;
        }
        set
        {
            _playerDeck = value;
        }
    }
}
