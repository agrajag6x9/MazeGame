using UnityEngine;
using System.Collections;

public class RotateFlashlight : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Camera.main.ScreenToWorldPoint ( Input.mousePosition));
		
		Vector2 MouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
		//Debug.Log (-transform.position + MouseWorld);
		//Vector3 toMouse = transform.position - Input.mousePosition;
		transform.LookAt (MouseWorld);
	}
}
