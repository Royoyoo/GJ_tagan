using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Text scoreText;
	public Text mudText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Очки:" + GameController.instance.playerScript.score.ToString ();
		mudText.text = "Грязь:" + GameController.instance.playerScript.mud.ToString ();
	}
}
