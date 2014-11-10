using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// define the different audio states
	// (normal): the default exploration theme
	// (chase): the theme for when the player is being chased
	enum State {
		normal,
		chase
	}
	
	// define the variables
	public AudioClip normalClip;
	public AudioClip chaseClip;
	
	private AudioSource asrc;
	private State curState;
	
	// Use this for initialization
	void Start () {
		asrc = GetComponent<AudioSource>() as AudioSource;
		asrc.clip = normalClip;
		asrc.Play ();
		
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetButtonDown ("Jump") ) {
			if (asrc.clip == normalClip) {
				PlayChase ();
			} else {
				PlayNormal ();
			}
		}
	}
	
	
	void PlayNormal() {
		asrc.clip = normalClip;
		asrc.Play ();
	}
	
	void PlayChase() {
		asrc.clip = chaseClip;
		asrc.Play ();
	}
}
