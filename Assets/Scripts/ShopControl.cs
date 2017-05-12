using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour {

	public float refillInterval;
	float lastRefillTime;
	PlayerControl playerInstance;

	void Start () {
		playerInstance = GameController.instance.playerScript;
		lastRefillTime = Time.time;
	}

	void Update () {
		if (playerInstance.isNearShop && Time.time > lastRefillTime + refillInterval)
		{
			playerInstance.AddAmmo ();
			lastRefillTime = Time.time;
		}
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
			playerInstance.isNearShop = true;
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
			playerInstance.isNearShop = false;
	}
}
