using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour {

	public float speed;
	float currentPos;
	float distance;
	public Vector3 targetPos;
	public AnimationCurve projectileYTrajectory;
	public float YScale, YDistanceFaloff;

	// Use this for initialization
	void Start () {
		currentPos = 0;
		distance = Vector3.Distance (transform.position, targetPos);

	}
	
	// Update is called once per frame
	void Update () {

		currentPos += speed * Time.deltaTime / distance;

		if (currentPos > 1)
			Destroy (this.gameObject);
		
		transform.position = new Vector3(transform.position.x + transform.forward.x * speed * Time.deltaTime,
										YScale * projectileYTrajectory.Evaluate (currentPos) * distance / YDistanceFaloff,
										transform.position.z + transform.forward.z * speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Location")
			Destroy (this.gameObject);

		if (col.tag == "Enemy")
		{
			Debug.Log ("EnemyHit");
			Destroy (this.gameObject);
		}
	}
}
