using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour {

	public float startTime;
	public float duration;


	void Start () {
		startTime = Time.time;
	}

	void Update () {

		if (Time.time > startTime + duration)
			Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy")
		{
			Debug.Log ("EnemyHit");
			Destroy (this.gameObject);
		}
	}
}
