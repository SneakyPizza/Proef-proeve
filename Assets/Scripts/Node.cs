using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	private int _nodeID = 0; 

	private Vector3 _nodePos = new Vector2(0,0); 
	private List<Node> _neighbourNodes = new List<Node>();

	public Vector3 NodePos {get{return _nodePos;}}
	public int GetID {get{return _nodeID;}}

	[SerializeField]private bool occupied = false; 
	public bool Occupied {get{return occupied;}set{occupied = value;}}

	[SerializeField]private bool hasPlayer = false;
	public bool HasPlayer {get{return hasPlayer;}set{hasPlayer = value;}}

	public void SetValues(float x, float y)
	{
		_nodePos.x = x;
		_nodePos.y = y;
	}

	public int NodeID
	{
		set
		{
			_nodeID = value;
		}
	}
	public List<Node> NeighbourNodes
	{
		get
		{
			return _neighbourNodes;
		}
		set
		{
			_neighbourNodes = value;
		}
	}
}