using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TileType
{
	Blank, Wall, SpawnPlayer
};

public class Tile : MonoBehaviour {
	public GameObject Target;
	TileManager tm;
	/*
	 * the sprites for each tile
	 */
	public List<Sprite> TileSprites;
		
	private int column, row;
	
	/*
	 * the type of this particular tile.  Initialized to blank
	 */
	private TileType currentTile = TileType.Blank;
	public TileType CurrentTile {
		get{ return currentTile;}
		set{
			currentTile = (TileType)value;

			GetComponent<SpriteRenderer> ().sprite = TileSprites [(int)currentTile];
		}
	}

	/*
	 * defines where the tile is on the map
	 * Params:
	 * 		c (int): the column number
	 *		r (int): the row number
	 */
	public void SetColumnRow (int c, int r)
	{
		column = c;
		row = r;
	}

	/*
	 * prints the tile's location when clicked
	 */
	void OnMouseUp ()
	{
		print ("column " + column + ", row " + row + ", " + CurrentTile);
	}
	
	/*
	 * Spawns the player when called
	 */
	 public void SpawnPlayer () 
	{
		//Reference to TileManager
		GameObject mainGO = GameObject.Find ("MainGO");
		tm = mainGO.GetComponent<TileManager> ();

		//Offsets spawn location to be the center of a tile
		Vector3 spawnPos = new Vector3 (transform.position.x + tm.TileSize / 2, transform.position.y - tm.TileSize / 2, 0); 
	 	GameObject player = Instantiate (Resources.Load<GameObject> ("character"),spawnPos, Quaternion.identity) as GameObject;
	 }

	void OnCillisionEnter2D(Collision2D other){
			if (other.gameObject == Target) {

				}
		}
}
