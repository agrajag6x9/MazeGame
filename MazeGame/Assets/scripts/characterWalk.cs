using UnityEngine;
using System.Collections;

public class characterWalk : MonoBehaviour {

	// an enum describing the possible directions of the player
	private enum Direction {
		Right, Left
	}
	
	private Direction playerFacing = Direction.Left;
	
	public float speed = 6;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// left
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
			if (playerFacing == Direction.Right) {
				Flip ();
				playerFacing = Direction.Left;
			}
		}
		
		// right
		if(Input.GetKey (KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
			if (playerFacing == Direction.Left) {
				Flip ();
				playerFacing = Direction.Right;
			}
		}
		
		// up
		if(Input.GetKey (KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		
		// down
		if(Input.GetKey (KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		
	}

	void OnTriggerEnter (Collider col) {
		print("hello world");
	}
	
	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
