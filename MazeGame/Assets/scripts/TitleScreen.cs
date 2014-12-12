using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// define the gui variables
	private int BHEIGHT;
	private int BWIDTH;
	private int BROW;
	private int BOFF;
	
	private int TWIDTH;
	private int THEIGHT;
	private int TOFF;
	
	public GUISkin skin;
	//public Font f;
	
	// Use this for initialization
	void Start () {
		BHEIGHT = 150;
		BWIDTH = 300;
		BROW = Screen.height/2 + BHEIGHT/2;
		BOFF = BWIDTH/2;
		
		TWIDTH = 3 * BWIDTH;
		THEIGHT = 2 * BHEIGHT;
		TOFF = TWIDTH/2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
	
		GUI.skin = skin;
		//GUI.skin.font = f;
		
		// initialize the GUI elements
		GUI.Box(new Rect(Screen.width/2 - TOFF,THEIGHT/3,TWIDTH,THEIGHT), "The Maze");
		var Tutorial = GUI.Button (new Rect(Screen.width/2 - BOFF - BWIDTH, BROW, BWIDTH, BHEIGHT), "Tutorial");
		var Rand = GUI.Button (new Rect(Screen.width/2 - BOFF + BWIDTH, BROW, BWIDTH, BHEIGHT), "Random Level");
		
		// Add behaviour to the GUI
		if (Tutorial) {
			Debug.Log ("Tutorial selected");
		}
		if (Rand) {
			Application.LoadLevel ("Main");
		}
	}
}
