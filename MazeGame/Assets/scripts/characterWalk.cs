using UnityEngine;
using System.Collections;

public class characterWalk : MonoBehaviour {

	public float speed = 12;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey (KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
			Flip ();
		}
		if(Input.GetKey (KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
			Flip();
		}
		if(Input.GetKey (KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		if(Input.GetKey (KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		
	}
	void Flip()
	{
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
