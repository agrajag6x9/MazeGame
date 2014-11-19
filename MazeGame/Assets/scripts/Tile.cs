using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TileType
{
	Blank, Wall
};

public class Tile : MonoBehaviour {
	public List<Sprite> TileSprites;
		
	private int column, row;
	private TileType currentTile = TileType.Blank;

	public TileType CurrentTile {
		get{ return currentTile;}
		set{
			currentTile = (TileType)value;

			GetComponent<SpriteRenderer> ().sprite = TileSprites [(int)currentTile];
		}
	}

	public void SetColumnRow (int c, int r)
	{
		column = c;
		row = r;
	}

	void OnMouseUp ()
	{
		print ("column " + column + ", row " + row + ", " + CurrentTile);
	}
}
