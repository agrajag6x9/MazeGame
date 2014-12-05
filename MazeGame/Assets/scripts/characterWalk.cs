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
}
