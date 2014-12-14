using UnityEngine;
using System.Collections;

public class characterWalk : MonoBehaviour {

	public GameObject TilePrefab; 
	public GameObject character;
	// an enum describing the possible directions of the player
	private enum Direction {
		Right, Left
	}
	
	private Direction playerFacing = Direction.Left;

	float screenHalfHeight;
	float screenHalfWidth;

	// the size of the tile
	private float tileSize;
	public float TileSize { get { return tileSize; } }
	public float speed = 6;

	// Use this for initialization
	void Start () {
		tileSize = TilePrefab.renderer.bounds.extents.x * 2;
		InvokeRepeating ("FindTile", 0, 2);

		screenHalfHeight = Camera.main.orthographicSize;
		screenHalfWidth = (float)Mathf.Round (screenHalfHeight * Camera.main.aspect);
	}

	//movement function
	void FixedUpdate () 
	{
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		rigidbody2D.velocity = new Vector2 (moveX * speed, moveY * speed);

	}
	
	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//Finds the characters position on grid
	void FindTile()
	{
		int column = (int)Mathf.Floor((screenHalfWidth + character.transform.position.x)/tileSize);
		int row = (int)Mathf.Floor((screenHalfHeight - character.transform.position.y)/tileSize);
		//print ("column: " + column + "row: " + row);
	}
}
