using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudSource : MonoBehaviour {

	public Collider thisCollider;

	public float mudAmmount;
	public float scoreAmmount;

	public float inactiveInterval;
	float startTime;


	void Start () {
		startTime = Time.time - inactiveInterval;
		thisCollider = GetComponent<Collider> ();
	}

	void Update () 
	{
		if (Time.time > startTime + inactiveInterval)
		{
			thisCollider.enabled = true;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player" && Time.time > startTime + inactiveInterval)
		{
			GameController.instance.playerScript.AddMud (this);
			thisCollider.enabled = false;
			startTime = Time.time;
		}
	}
}
