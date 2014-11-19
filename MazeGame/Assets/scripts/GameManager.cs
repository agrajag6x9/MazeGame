using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager instance;
	public static GameManager Instance {
		get{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<GameManager> ();
			}
			return instance;
	}
	}
}