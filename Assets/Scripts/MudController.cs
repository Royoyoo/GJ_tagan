using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudController : MonoBehaviour {

	public GameObject Player; 
	public Collider ThisColl;
	public int Mud = 10;
	public int Score = 0;
	float currentDelay = 0f;
	float timeLeft = 10;
	bool colourChangeCollision = false;
	GameObject tempObj;


	void OnCollisionEnter(Collision other) {

		currentDelay = Time.time;
		if (other.gameObject.layer == 20) {
			Mud = Mud + 1;
			Score = Score + 10;  
		}

		if (other.gameObject.layer == 21) {
			Mud = Mud - 1; 
		}

		if (other.gameObject.tag == "Bonus" && isActiveAndEnabled) {
			Debug.Log ("Collision");	
			Mud = Mud + 20;
			Score = Score + 200;
			timeLeft = 10;
			tempObj = other.gameObject;
			tempObj.SetActive (false);
		}
	}
	void Update()
	{
		if (tempObj != null) {
			if (timeLeft == 10)
				Debug.Log ("-Yoba, ti gde?");
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				tempObj.SetActive (true);
				Debug.Log ("-4o blya? Ti popal, pidor");	
				tempObj = null;
			}
		}
	}
		/// 
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 100), "Score: " +Score.ToString());
	}
}