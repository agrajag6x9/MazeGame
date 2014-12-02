using UnityEngine;
using System.Collections;

public class MazeCell {

	public int Row { get; set; }
	public int Col { get; set; }
	public TileType Type { get; set; }
	
	public MazeCell (int row, int col, TileType type) {
		Row = row;
		Col = col;
		Type = type;
	}
}
