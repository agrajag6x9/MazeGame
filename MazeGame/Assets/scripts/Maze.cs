using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RandomMaze;

public class Maze {

	private List<List<int>> tile = new List<List<int>>();
	public int Rows { get; set; }
	public int Cols { get; set; }
	
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
		
		// set the rows and columns
		Rows = tile.Count;
		Cols = tile[0].Count;
	}
	
	/// <summary>
	/// Randomly create a maze
	/// </summary>
	/// <param name="rows">the number of rows in the maze</param>
	/// <param name="cols">the number of columns in the maze.</param>
	public Maze (int rows, int cols) {
	
		// create a random maze
		// each cell has an empty space and a wall associated with it
		// so in practice we only need half the number of tiles
		int bCols = ((cols+1)/2);
		int bRows = ((rows+1)/2);
		BacktrackingMaze b = new BacktrackingMaze(bCols, bRows);
		b.Generate ();
		Debug.Log (b.Print());
		
		for (int y = 0; y < b.sizeY; y++) {
		
			// generate the top row (main tile and East wall tile)
			List<int> topRow = new List<int>();
			for (int x = 0; x < b.sizeX; x++) {
				topRow.Add ((int)TileType.Blank);
				
				if (b.cells[x, y].borders.Contains(Walls.E)) {
					topRow.Add ((int)TileType.Wall);
				}
				else {
					topRow.Add ((int)TileType.Blank);
				}
				
			}
			tile.Add (topRow);
			
			// generate the bottom row (Southern wall tile)
			List<int> bottomRow = new List<int>();
			for (int x = 0; x < b.sizeX; x++) {
				
				if (b.cells[x, y].borders.Contains(Walls.S)) {
					bottomRow.Add ((int)TileType.Wall);
				}
				else {
					bottomRow.Add ((int)TileType.Blank);
				}
				bottomRow.Add ((int)TileType.Wall);
				
			}
			tile.Add (bottomRow);
			
		}
		
		// add the player to a random tile
		bool spawnFound = false;
		while (!spawnFound) {
			tile[0][0] = (int)TileType.SpawnPlayer;
			spawnFound = true;
		}
		
		// set the rows and columns		
		Rows = tile.Count;
		Cols = tile[0].Count;
	}
}
