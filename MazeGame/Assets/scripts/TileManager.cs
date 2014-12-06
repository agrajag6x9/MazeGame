using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileManager : MonoBehaviour {

	// the object on which tiles are based
	public GameObject TilePrefab;  
	
	public int Rows = 6;
	public int Cols = 6;
	
	float screenHalfHeight;
	float screenHalfWidth;
	
	// 2D array of tiles
	private GameObject[,] tiles;
	public GameObject[,] Tiles { get { return tiles; } }
	
	// the size of the tile
	private float tileSize;
	public float TileSize { get { return tileSize; } }
	
	// the number of tiles in the map
	private int TileCount;

	// the map of the tiles and the data on which it is based
	public int[,] map;
	private TextAsset mapData;
	

	private  static TileManager instance;   
	public static TileManager Instance {     
		get {    
			if (instance == null)  
				instance = GameObject.FindObjectOfType<TileManager> ();      
			return instance;     
		}
	}
	

	void Start () {
		screenHalfHeight = Camera.main.orthographicSize;
		screenHalfWidth = (float)Mathf.Round (screenHalfHeight * Camera.main.aspect);

		tileSize = TilePrefab.renderer.bounds.extents.x * 2;

		mapData = (TextAsset)Resources.Load ("Start");
		print (mapData.text);

		BuildTiles ();
		InitializeMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * Creates a 2D array of blank tiles
	 */
	public void BuildTiles()
	{
		float startX = -screenHalfWidth;
		float startY = screenHalfHeight;

		tiles = new GameObject[Cols, Rows];

		for (int y = 0; y < Rows; y++) 
		{
			for (int x = 0; x < Cols; x++)
			{
				Vector3 position = new Vector3 (startX + x * tileSize, startY - y * tileSize, 0);
				GameObject t = Instantiate (TilePrefab, position, Quaternion.identity) as GameObject;
				t.GetComponent<Tile> ().SetColumnRow (x, y);
				tiles [x, y] = t;
			}
		}

		TileCount = Cols * Rows;
		Debug.Log ("Total tiles: " + TileCount);
	}


	/*
	 * Set the values of the tile array based on the data in map data file
	 */
	public void InitializeMap ()
	{
		/*
		map = new int[Cols, Rows];

		string[] lines = mapData.text.Split ('\n');

		
		for (int y = 0; y < Rows; y++) {

			string[] line = (lines [y].Split (','));

			for(int x = 0; x < Cols; x++) {

					int num = int.Parse (line[x]);

					map[x, y] = num;
				}
			}
		*/
		//Maze startMaze = new Maze(mapData);
		Maze startMaze = new Maze(mapData);

		for (int y = 0; y < Rows; y++) {
			for (int x = 0; x < Cols; x++){
				tiles [x, y].GetComponent<Tile> ().CurrentTile = (TileType)startMaze.GetTile (x, y);
				
				/* spawn the player */
				if (tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.SpawnPlayer) {
					tiles [x, y].GetComponent<Tile> ().SpawnPlayer();
				}
				/*removes colliders from blank tiles*/
				if (tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.Blank || tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.SpawnPlayer) {
					Destroy(tiles[x,y].gameObject.collider2D);
					//tiles [x, y].transform.collider.isTrigger = true;
				}
			}
		}
	}
}
