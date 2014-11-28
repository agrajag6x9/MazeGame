using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze {

	private List<List<int>> tile = new List<List<int>>();
	
	/// <summary>
	/// Gets a tile at the specified coordinates
	/// </summary>
	/// <returns>The tile.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public int GetTile (int x, int y) {
		return tile[y][x];
	}
	
	/// <summary>
	/// Initializes a new instance of the Maze class from a text file.
	/// </summary>
	/// <param name="MazeFile">a text file on which the maze is based.</param>
	public Maze (TextAsset MazeFile) {
	
		// parse by text by lines
		string[] lines = MazeFile.text.Split ('\n');
		
		// parse by commas, and convert to int
		for (int y = 0; y < lines.Length; y++) {
			string[] line = lines[y].Split(',');
			List<int> tileRow = new List<int>();
			for (int x = 0; x < line.Length; x++) {
				tileRow.Add (int.Parse (line[x]));
			}
			
			// add the row to the main tiles list
			tile.Add (tileRow);
		}
	}
}
