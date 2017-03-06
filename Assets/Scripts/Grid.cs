using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	[SerializeField] private GameObject cubePrefab;
	[SerializeField] private int gridX = 0, gridY = 0;
	[SerializeField] private List<Node> nodeList = new List<Node>();
	[SerializeField] private GameManager gameManager;

	void Start ()
	{
		StartCoroutine(CreateGrid());
	}
	
	private IEnumerator CreateGrid()
	{
		GameObject parent = new GameObject("Parent");
		int id = 0;
		for(int x = 0; x < gridX; x++)
		{
			for(int y = 0; y < gridY; y++)
			{
				GameObject newCube = GameObject.Instantiate(cubePrefab, new Vector2((x - y) * 1.745f,x + y), cubePrefab.transform.rotation);
				Node newNode = newCube.AddComponent<Node>();
				newNode.SetValues(x,y, id);
				nodeList.Add(newNode);
				newCube.transform.SetParent(parent.transform);
				id++;
			}
		}

		parent.transform.position = new Vector3(-6.75f,-7.4f);
		parent.transform.localScale = new Vector3(0.55f,0.55f,0.55f);

		for (int i = 0; i < 7; i += 2)
		{
			nodeList[i].gameObject.tag = "StartNode";
		}
		for (int i = nodeList.Count - 1; i > nodeList.Count - 8; i -= 2)
		{
			nodeList[i].gameObject.tag = "EndNode";
		}

		foreach(GameObject go in GameObject.FindGameObjectsWithTag("StartNode"))
		{
			go.GetComponent<Renderer>().material.color = Color.red;
		}
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("EndNode"))
		{
			go.GetComponent<Renderer>().material.color = Color.blue;
		}

		List<Node> nodesToChange = new List<Node>();

		nodesToChange.AddRange(GetNodeRow(3,18,6));
		nodesToChange.AddRange(GetNodeRow(3,18,0));
		nodesToChange.AddRange(GetNodeRow(4,17,1));
		nodesToChange.AddRange(GetNodeRow(4,17,5));


		foreach(Node objct in nodesToChange)
		{
			//objct.GetComponent<Renderer>().material.color = Color.black;
			nodeList.Remove(objct);
			Destroy(objct.gameObject);
		}

		gameManager.StartGame();

		yield return new WaitForEndOfFrame();
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

	public Node GetNode(int x, int y)
	{
		int nodeToGet = (x * 7) + y;
		return nodeList[nodeToGet];
	}

	public List<Node> GetNodeList()
	{
		return nodeList;
	}
}