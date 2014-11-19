using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TileType
{
	Blank, Wall
};

public class Tile : MonoBehaviour {

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
}
