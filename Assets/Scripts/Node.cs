using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
	[SerializeField] private Vector2 nodePos = new Vector2(0,0); 
	[SerializeField] private int nodeID = 0; 

	public Vector2 NodePos{get{return nodePos;}}
	public int GetID{get{return nodeID;}}

	private bool occupied = false; 
	public bool Occupied{get{return occupied;}set{occupied = value;}}

	private bool traverseAble = false; 
	public bool TraverseAble{get{return traverseAble;}set{traverseAble = value;}}

	public void SetValues(int x, int y, int id)
	{
		nodePos.x = x;
		nodePos.y = y;
		nodeID = id;
	}
}