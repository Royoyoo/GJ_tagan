using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject playerGO;
	public PlayerControl playerScript;
	public static GameController instance;

	// Use this for initialization
	void Awake () {
		instance = this;
		playerScript = playerGO.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
