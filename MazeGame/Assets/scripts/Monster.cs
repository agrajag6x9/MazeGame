using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Monster : MonoBehaviour {

	TileManager tm;

	float screenHalfHeight;
	float screenHalfWidth;

	public GameObject TilePrefab; 
	public GameObject monster;
	public GameObject character;
	// the size of the tile
	private float tileSize;
	public float TileSize { get { return tileSize; } }

	//monster's col and row
	int mcolumn;
	int mrow;

	//closed list

	//checking list
	private List<GameObject> nodeTiles;
	// Use this for initialization
	void Start () {
		GameObject mainGO = GameObject.Find ("MainGO");
		tm = mainGO.GetComponent<TileManager> ();

		tileSize = TilePrefab.renderer.bounds.extents.x * 2;
		FindTile ();
		
		screenHalfHeight = Camera.main.orthographicSize;
		screenHalfWidth = (float)Mathf.Round (screenHalfHeight * Camera.main.aspect);
	}
	
	// Update is called once per frame
	void Update () {

	}


	void FindTile()
	{
		mcolumn = (int)Mathf.Floor((screenHalfWidth + monster.transform.position.x)/tileSize);
		mrow = (int)Mathf.Floor((screenHalfHeight - monster.transform.position.y)/tileSize);
		print ("column: " + mcolumn + "row: " + mrow);
	}

	void FindPath()
	{
		//calling surrounding tiles for testing
		GameObject tile1 = tm.Tiles [mrow + 1, mcolumn];
		GameObject tile2 = tm.Tiles [mrow - 1, mcolumn];
		GameObject tile3 = tm.Tiles [mrow, mcolumn + 1];
		GameObject tile4 = tm.Tiles [mrow, mcolumn - 1];
		nodeTiles.Add (tile1);
		nodeTiles.Add (tile2);
		nodeTiles.Add (tile3);
		nodeTiles.Add (tile4);

		//distances for each tile
		Dictionary<GameObject, int> distances = new Dictionary<GameObject, int> ();

		//remove wall tiles from list
		for (int i = 0; i < 4; i++) 
		{
			if(nodeTiles[i].GetComponent<Tile> ().CurrentTile == TileType.Wall)
			{
				nodeTiles.RemoveAt(i);
			}
		}

		//find distance to target point
		foreach (GameObject t in nodeTiles) 
		{
			int x = Mathf.RoundToInt(monster.transform.position.x + character.transform.position.x);
			int y = Mathf.RoundToInt(monster.transform.position.y + character.transform.position.y);
			int h = x + y;
			distances.Add(t, h);
		}

		//sort the dictionary
		List<KeyValuePair<GameObject, int>> myList = distances.ToList();
		
		myList.Sort(
			delegate(KeyValuePair<GameObject, int> firstPair,
		         KeyValuePair<GameObject, int> nextPair)
			{
			return firstPair.Value.CompareTo(nextPair.Value);
		}
		);
		print (myList);
	}
}
