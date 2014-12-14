using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileManager : MonoBehaviour {

	// the object on which tiles are based
	public GameObject TilePrefab;  
	
	public int Rows = 6;
	public int Cols = 6;
	
	public bool loadLevel;
	
	public float screenHalfHeight;
	public float screenHalfWidth;
	
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
		/*
		screenHalfHeight = Camera.main.orthographicSize;
		screenHalfWidth = (float)Mathf.Round (screenHalfHeight * Camera.main.aspect);
		*/

		tileSize = TilePrefab.renderer.bounds.extents.x * 2;

		mapData = (TextAsset)Resources.Load ("Start");
		
		// create new maze
		Maze startMaze;
		if ( loadLevel ) {
			startMaze = new Maze(mapData);
			Rows = startMaze.Rows;
			Cols = startMaze.Cols;
		} else {
			startMaze = new Maze(Rows, Cols);
		}
		
		screenHalfHeight = tileSize * Rows/2;
		screenHalfWidth = tileSize * Cols/2;
		

		//BuildTiles ();
		//InitializeMap ();
		MakeMap (startMaze);
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
		Maze startMaze = new Maze(Rows, Cols);
		//Maze startMaze = new Maze(mapData);
		Rows = startMaze.Rows;
		Cols = startMaze.Cols;

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
	
	public void MakeMap( Maze maze ) {
	
		float startX = -screenHalfWidth;
		float startY = screenHalfHeight;
		
		tiles = new GameObject[Cols, Rows];
		
		// make a wall on top of the stage
		for (int x = -1; x < Cols + 1; x++) {
			Vector3 topWallPos = new Vector3 (startX + x * tileSize, startY + 1 * tileSize, 0);
			GameObject topWall = Instantiate (TilePrefab, topWallPos, Quaternion.identity) as GameObject;
			topWall.GetComponent<Tile>().CurrentTile = TileType.Wall;
		}
		
		for (int y = 0; y < Rows; y++) 
		{
			// make a wall to the left of the stage
			Vector3 leftWallPos = new Vector3 (startX - 1 * tileSize, startY - y * tileSize, 0);
			GameObject leftWall = Instantiate (TilePrefab, leftWallPos, Quaternion.identity) as GameObject;
			leftWall.GetComponent<Tile>().CurrentTile = TileType.Wall;
			
			for (int x = 0; x < Cols; x++)
			{
				// initialize the Tile Object
				Vector3 position = new Vector3 (startX + x * tileSize, startY - y * tileSize, 0);
				GameObject t = Instantiate (TilePrefab, position, Quaternion.identity) as GameObject;
				t.GetComponent<Tile> ().SetColumnRow (x, y);
				tiles [x, y] = t;
				
				// set up the Tile
				tiles [x, y].GetComponent<Tile> ().CurrentTile = (TileType)maze.GetTile (x, y);
				
				/* spawn the player */
				if (tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.SpawnPlayer) {
					tiles [x, y].GetComponent<Tile> ().SpawnPlayer();
				}
				/*removes colliders from blank tiles*/
				if (tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.Blank ||
				 tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.SpawnPlayer ||
				 tiles [x, y].GetComponent<Tile> ().CurrentTile == TileType.RandomLevel
				 ) {
					//Destroy(tiles[x,y].gameObject.collider2D);
					tiles[x,y].gameObject.collider2D.isTrigger = true;
					//tiles [x, y].transform.collider.isTrigger = true;
				}
			}
			
			// make a wall to the right of the stage
			Vector3 rightWallPos = new Vector3 (startX + Cols * tileSize, startY - y * tileSize, 0);
			GameObject rightWall = Instantiate (TilePrefab, rightWallPos, Quaternion.identity) as GameObject;
			rightWall.GetComponent<Tile>().CurrentTile = TileType.Wall;
			
		}
		
		// make a wall on the bottom of the stage
		for (int x = -1; x < Cols + 1; x++) {
			Vector3 topWallPos = new Vector3 (startX + x * tileSize, startY - Rows * tileSize, 0);
			GameObject topWall = Instantiate (TilePrefab, topWallPos, Quaternion.identity) as GameObject;
			topWall.GetComponent<Tile>().CurrentTile = TileType.Wall;
		}
		
	}
}
