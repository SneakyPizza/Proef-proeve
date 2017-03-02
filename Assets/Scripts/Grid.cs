using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	
	public class Node : MonoBehaviour
	{
		[SerializeField] private Vector2 nodePos = new Vector2(0,0); public Vector2 NodePos{get{return nodePos;}}
		private bool occupied = false; public bool Occupied{get{return occupied;}set{occupied = value;}}
		private bool occupidTwice = false; public bool OccupiedTwice{get{return occupidTwice;}set{OccupiedTwice = value;}}
		[SerializeField] private int nodeID = 0; public int GetID{get{return nodeID;}}

		public void SetValues(int x, int y, int id)
		{
			nodePos.x = x;
			nodePos.y = y;
			nodeID = id;
		}
	}

	[SerializeField] private GameObject cubePrefab;
	[SerializeField] private int gridX = 0, gridY = 0;

	void Start ()
	{
		CreateGrid();
	}
	
	private void CreateGrid()
	{
		GameObject parent = new GameObject("Parent");
		int id = 0;
		for(int x = 0; x < gridX; x++)
		{
			for(int y = 0; y < gridY; y++)
			{
				GameObject newCube = GameObject.Instantiate(cubePrefab, new Vector2(x,y),Quaternion.identity);
				Node newNode = newCube.AddComponent<Node>();
				newNode.SetValues(x,y, id);

				newCube.transform.SetParent(parent.transform);
				id++;
			}
		}
		parent.transform.position = new Vector3(-8,-3f);
	}
}
