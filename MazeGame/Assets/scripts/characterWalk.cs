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

	private float screenHalfHeight;
	private float screenHalfWidth;

	private float distanceTraveled = 0f;
	private AudioSource asrc;

	// the size of the tile
	private float tileSize;
	public float TileSize { get { return tileSize; } }

	public float speed = 6;
	public AudioClip footstep;

	// Use this for initialization
	void Start () {
		tileSize = TilePrefab.renderer.bounds.extents.x * 2;
		InvokeRepeating ("FindTile", 0, 2);

		screenHalfHeight = Camera.main.orthographicSize;
		screenHalfWidth = (float)Mathf.Round (screenHalfHeight * Camera.main.aspect);

		asrc = GetComponent<AudioSource>();
		StartCoroutine(FootstepSound());
	}

	//movement function
	void FixedUpdate () 
	{
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		
		Vector2 move = new Vector2 (moveX * speed, moveY * speed);
		distanceTraveled += move.magnitude;
		
		rigidbody2D.velocity = move;

	}
	
	private IEnumerator FootstepSound (){
		bool playSound = true;
		while (playSound) {
		
			//Debug.Log (distanceTraveled);
			if (distanceTraveled > 100) {
				asrc.Play();
				distanceTraveled = 0;
			}
			yield return 0;
		}
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
