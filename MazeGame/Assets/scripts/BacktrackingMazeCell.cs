using System;
using System.Collections.Generic;

namespace RandomMaze
{
	public enum Walls {
		N,
		S,
		E,
		W
	}

	public class BacktrackingMazeCell
	{
		public List<Walls> borders = new List<Walls>();
		public int xLoc, yLoc;

		public BacktrackingMazeCell (int x, int y)
		{

			xLoc = x;
			yLoc = y;
			borders.Add (Walls.N);
			borders.Add (Walls.S);
			borders.Add (Walls.E);
			borders.Add (Walls.W);
		}

		public override string ToString ()
		{
			string s = "";
			foreach (Walls w in borders) {
				s += w.ToString ();
			}
			return s;
		}


	}
}

