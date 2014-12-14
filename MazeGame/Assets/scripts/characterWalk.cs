using UnityEngine;
using System.Collections;

public class characterWalk : MonoBehaviour {

	// an enum describing the possible directions of the player
	private enum Direction {
		Right, Left
	}
	
	private Direction playerFacing = Direction.Left;
	
	private float ScreenHalfWidth;
	private float ScreenHalfHeight;
	
	private float distanceTraveled = 0f;
	private AudioSource asrc;
	
	public float speed = 6;
	public AudioClip footstep;
	
	// Use this for initialization
	void Start () {
		asrc = GetComponent<AudioSource>();
		StartCoroutine(FootstepSound());
	}
	
	// Update is called once per frame
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
}
