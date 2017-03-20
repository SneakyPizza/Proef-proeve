using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	[SerializeField] private int _gridX = 0, _gridY = 0;
	[SerializeField] private GameObject _nodePrefab;

	[SerializeField] private Vector3 _gridPos;
	[SerializeField] private Vector3 _gridScale;

	private PlayerSpawner _playerSpawner;
	private List<Node> _nodeList = new List<Node>();

	void Awake()
	{
		_playerSpawner = GetComponent<PlayerSpawner>();
	}

	void Start ()
	{
		StartCoroutine(CreateGrid());
	}
	
	private IEnumerator CreateGrid()
	{
		GameObject parent = new GameObject("NodeParent");

		for(int x = 0; x < _gridX; x++)
		{
			for(int y = 0; y < _gridY; y++)
			{
				GameObject newCube = GameObject.Instantiate(_nodePrefab, new Vector2(x,y) , _nodePrefab.transform.rotation);
				Node newNode = newCube.AddComponent<Node>();
				_nodeList.Add(newNode);
				newNode.SetValues(x,y);
				newNode.gameObject.tag = Tags.NODE;
				newCube.transform.position = new Vector2((x - y) * 1.745f,x + y);
				newCube.transform.SetParent(parent.transform);
			}
		}

		parent.transform.position = _gridPos;
		parent.transform.localScale = _gridScale;

		for (int i = 0; i < 7; i += 2)
		{
			_nodeList[i].gameObject.tag = Tags.STARTNODE;
			_nodeList[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
		}
		for (int i = _nodeList.Count - 1; i > _nodeList.Count - 8; i -= 2)
		{
			_nodeList[i].gameObject.tag = Tags.ENDNODE;
			_nodeList[i].gameObject.GetComponent<Renderer>().material.color = Color.blue;
		}

		List<Node> nodesToRemove = new List<Node>();

		nodesToRemove.AddRange(GetNodeRow(3,18,6));
		nodesToRemove.AddRange(GetNodeRow(3,18,0));
		nodesToRemove.AddRange(GetNodeRow(4,17,1));
		nodesToRemove.AddRange(GetNodeRow(4,17,5));

		foreach(Node objct in nodesToRemove)
		{
			_nodeList.Remove(objct);
			Destroy(objct.gameObject);
		}

		for(int i = 0; i < _nodeList.Count; i++)
		{
			AddNeighbourNodes(_nodeList[i]);
			_nodeList[i].NodeID = i;
		}
		for(int i = 0; i < _nodeList.Count; i++)
		{
			Vector2 nodePos = _nodeList[i].gameObject.transform.position;
			_nodeList[i].SetValues(nodePos.x,nodePos.y);
		}
			
		_playerSpawner.StartGame();

		yield return new WaitForEndOfFrame();
	}

	private void AddNeighbourNodes(Node node)
	{
		for (int i = 0; i < _nodeList.Count; i++)
		{
			float dist = Vector3.Distance(node.NodePos, _nodeList[i].NodePos);
			if (dist == 1 || (dist > 1.40f && dist < 1.45f))
				node.NeighbourNodes.Add(_nodeList[i]);
		}
	}
	
	private List<Node> GetNodeRow(int xMin, int xMax, int y)
	{
		List<Node> returnNodes = new List<Node>();

		for (int i = xMin; i < xMax; i++)
		{
			returnNodes.Add(GetNode(i,y));
		}

		return returnNodes;
	}

	private Node GetNode(int x, int y)
	{
		int nodeToGet = (x * 7) + y;
		return _nodeList[nodeToGet];
	}

	public List<Node> GetNodeList()
	{
		return _nodeList;
	}
}