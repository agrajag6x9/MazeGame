using System;
using System.Linq;
using System.Collections.Generic;

namespace RandomMaze
{
	public enum Direction {
		N,
		S,
		E,
		W,
		M
	}

	public class BacktrackingMaze
	{
		// create the lists for tracking the maze generation
		List<BacktrackingMazeCell> visited = new List<BacktrackingMazeCell> ();
		List<BacktrackingMazeCell> unvisited = new List<BacktrackingMazeCell> ();
		Stack<BacktrackingMazeCell> working = new Stack<BacktrackingMazeCell> ();


		public int sizeX, sizeY;
		public BacktrackingMazeCell[,] cells;

		/// <summary>
		/// Creates a new maze with the specified x and y dimensions
		/// </summary>
		/// <param name="x">The number of cells in the x direction (columns)</param>
		/// <param name="y">The number of cells in the y direction (rows)</param>
		public BacktrackingMaze (int x, int y)
		{
			sizeX = x;
			sizeY = y;
		}

		/// <summary>
		/// Generate a random maze.
		/// </summary>
		public void Generate() {
			cells = new BacktrackingMazeCell[sizeX, sizeY];
			for (int x = 0; x < sizeX; x++) {
				for (int y = 0; y < sizeY; y++) {
					cells [x, y] = new BacktrackingMazeCell (x, y);
				}
			}

			// Get random values
			Random r = new Random ();
			int randX = r.Next (sizeX);
			int randY = r.Next (sizeY);

			// Remove walls to create a randomly generated maze
			visited.Add (cells [randX, randY]);
			RemoveWall (cells [randX, randY]);
		}

		public void RemoveWall(BacktrackingMazeCell current) {

			// get a neighbor of the current cell
			List<BacktrackingMazeCell> neighbors = GetUnvisitedNeighbors (current);

			if (neighbors.Count > 0) {
				Random r = new Random ();
				BacktrackingMazeCell neighbor = neighbors [r.Next(0, neighbors.Count)];

				// get the direction between the current cell and the neighbor
				Direction d = GetDirection (current, neighbor);

				// push the current cell to the stack
				working.Push (current);

				// remove the wall between it and the current cell
				if (d == Direction.N) {
					current.borders.Remove (Walls.N);
					neighbor.borders.Remove (Walls.S);
				} else if (d == Direction.S) {
					current.borders.Remove (Walls.S);
					neighbor.borders.Remove (Walls.N);
				} else if (d == Direction.W) {
					current.borders.Remove (Walls.W);
					neighbor.borders.Remove (Walls.E);
				} else if (d == Direction.E) {
					current.borders.Remove (Walls.E);
					neighbor.borders.Remove (Walls.W);
				}
				visited.Add (neighbor);
				RemoveWall (neighbor);

			} 
			// if the cell has no neighbors, but there are still working cells
			else if (working.Count > 0) {
				BacktrackingMazeCell cell = working.Pop ();
				RemoveWall (cell);
			}
				
		}

		/// <summary>
		/// Gets the neighbors of the current cell
		/// </summary>
		/// <returns>The neighbors of the celll</returns>
		/// <param name="selected">the cell that we want to get the neighbors of</param>
		private List<BacktrackingMazeCell> GetNeighbors(BacktrackingMazeCell selected) {
			List<BacktrackingMazeCell> neighbors = new List<BacktrackingMazeCell> ();

			//Northern Neighbor
			if (selected.yLoc - 1 >= 0) {
				neighbors.Add (cells [selected.xLoc, selected.yLoc - 1]);
			}

			//Southern Neighbor
			if (selected.yLoc + 1 < sizeY) {
				neighbors.Add (cells [selected.xLoc, selected.yLoc + 1]);
			}

			//Western Neighbor
			if (selected.xLoc - 1 >= 0) {
				neighbors.Add (cells [selected.xLoc - 1, selected.yLoc]);
			}

			//Eastern Neighbor
			if (selected.xLoc + 1 < sizeX) {
				neighbors.Add (cells [selected.xLoc + 1, selected.yLoc]);
			}

			return neighbors;
		}

		private List<BacktrackingMazeCell> GetUnvisitedNeighbors(BacktrackingMazeCell selected) {
			List<BacktrackingMazeCell> neighbors = GetNeighbors (selected);
			Console.WriteLine (neighbors.Count);
			IEnumerable<BacktrackingMazeCell> uvNeighbors = neighbors.Except(visited);
			return uvNeighbors.ToList();
		}

		/// <summary>
		/// Get the direction from the starting cell to the ending cell
		/// </summary>
		/// <returns>The direction from the starting cell to the end</returns>
		/// <param name="Start">the starting cell</param>
		/// <param name="End">the ending cell</param>
		private Direction GetDirection (BacktrackingMazeCell Start, BacktrackingMazeCell End) {

			Direction d = Direction.M;
			if (Start.xLoc - End.xLoc > 0) {
				d = Direction.W;
			} else if (Start.xLoc - End.xLoc < 0) {
				d = Direction.E;
			} else if (Start.yLoc - End.yLoc > 0) {
				d = Direction.N;
			} else if (Start.yLoc - End.yLoc < 0) {
				d = Direction.S;
			}

			return d;
		}

		/// <summary>
		/// Prints each cell as a list of bounding walls
		/// </summary>
		public void PrintLiteral() {
			for (int y = 0; y < sizeY; y++) {
				for (int x = 0; x < sizeX; x++) {
					Console.Write (cells[x, y].ToString() + " ");
				}
				Console.WriteLine ();
			}
		}

		/// <summary>
		/// Print the maze in a human readable format
		/// </summary>
		public void Print() {
			for (int y = 0; y < sizeY; y++) {
				for (int x = 0; x < sizeX; x++) {

					if (cells [x, y].borders.Contains (Walls.S)) {
						Console.Write ("_");
					} else {
						Console.Write (" ");
					}

					if (cells [x, y].borders.Contains (Walls.E)) {
						Console.Write ("|");
					} else {
						Console.Write (" ");
					}
				}
				Console.WriteLine ();
			}
		}
			
	}
}

