using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TileManager : MonoBehaviour {
	public BoxCollider bc;

	public GameObject TilePrefab;
	
	public int Rows = 4;
	public int Cols = 4;
	
	float screenHalfHeight;
	float screenHalfWidth;
	
	private GameObject[,] tiles;
	public GameObject[,] Tiles { get { return tiles; } }
	
	private float tileSize;
	public float TileSize { get { return tileSize; } }
	
	private int TileCount;

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

		mapData = (TextAsset)Resources.Load ("map");
		print (mapData.text);

		BuildTiles ();
		InitializeMap ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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


	public void InitializeMap ()
	{
		map = new int[Cols, Rows];

		string[] lines = mapData.text.Split ('\n');

		for (int y = 0; y < Rows; y++) {

			string[] line = (lines [y].Split (','));

			for(int x = 0; x < Cols; x++) {

					int num = int.Parse (line[x]);

					map[x, y] = num;
				}
			}

		for (int y = 0; y < Rows; y++) {
			for (int x = 0; x < Cols; x++){
				tiles [x, y].GetComponent<Tile> ().CurrentTile = (TileType)map [x, y];
			}
		}
	}
}
