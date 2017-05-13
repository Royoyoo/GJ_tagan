using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class Weapon {
//	public float maxTargetDistance;
//	public int aimingType;
//	public float aimLoopTime;
//	public int ammoCount;
//	public int maxAmmo;
//	public GameObject aimPrefab;
//	public GameObject projectilePrefab;
//}

public class PlayerControl : MonoBehaviour {

	public bool isHolding;

	public float threat;

	//public float stamina, maxStamina;
	public float speed, speedMultiplier;
	public float throwStrength;
	//public Vector3 lookVector;

	public List<EnemyControl> enemyNear;

	public float moveTimer;
	public float enemyReactInterval;
	float lastEnemyReact;

	public Animator anim;

	public float score;
	public float mud;
	public float mudDropInterval;
	float lastMudDrop;

	EnemyControl enemyControl;

	void Start () {
		lastEnemyReact = Time.time;
		lastMudDrop = Time.time;
		isHolding = false;
	}
	
	void Update () {
		
		// Enemies reacts if threat is high
		if (threat > 0.1f && Time.time > lastEnemyReact + enemyReactInterval)
		{
			foreach (EnemyControl EC in enemyNear)
			{
				EC.ReactToPlayer (threat);
			}

			lastEnemyReact = Time.time;
		}

		// Mud Drop Routine
		if (mud > 0 && Time.time > lastMudDrop + mudDropInterval)
		{
			Debug.Log ("Drop");
			Ray rayDrop = new Ray (transform.position, Vector3.down);
			RaycastHit dropHit;
			if (Physics.Raycast (rayDrop, out dropHit, 5f, 1 << 11))
			{
				mud--;
				score += 2;
				lastMudDrop = Time.time;
				Debug.Log ("Drop_ok");
			}


			lastMudDrop = Time.time;
		}

		//camera sensitivity
		transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * 5f, 0f);

		float V = Input.GetAxis ("Vertical");
		float H = Input.GetAxis ("Horizontal");

		if (V != 0)
			transform.Translate (transform.forward * speed * V * Time.deltaTime, Space.World);

		if (H != 0)
			transform.Translate (transform.right * speed * H * Time.deltaTime, Space.World);

		if (V != 0 || H != 0)
		{
			anim.SetBool ("isWalking", true);
			moveTimer += Time.deltaTime;
		}
		else
			anim.SetBool ("isWalking", false);

		//Try to grab or drop smth
		if (Input.GetMouseButtonDown (1))
		{
			if (!isHolding)
				Grab();
			else
				Drop();
				//Throw();
		}
	}

	public void AddMud(MudSource mudSource)
	{
		mud += mudSource.mudAmmount;
		score += mudSource.scoreAmmount;
	}

	void Grab()
	{
		anim.SetTrigger ("Melee");

		Ray grabRay = new Ray (transform.position, transform.forward);
		RaycastHit grabHit;

		if (Physics.Raycast (grabRay, out grabHit, 1.5f, 1 << 9))
		{
			enemyControl = grabHit.collider.gameObject.GetComponent<EnemyControl> ();
			if (enemyControl != null)
			{
				//enemyControl.isGrabbed = true;
				enemyControl.transform.SetParent (this.transform);
				isHolding = true;
			}
		}
	}

	void Throw()
	{
		isHolding = false;
		enemyControl.transform.parent = null;
		enemyControl.GetComponent<Rigidbody> ().AddForce (transform.forward * throwStrength, ForceMode.Impulse);
		enemyControl = null;
	}

	public void Drop()
	{
		isHolding = false;
		enemyControl.transform.parent = null;
		enemyControl = null;
	}
}
