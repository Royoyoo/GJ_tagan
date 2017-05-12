using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon {
	public float maxTargetDistance;
	public int aimingType;
	public float aimLoopTime;
	public int ammoCount;
	public int maxAmmo;
	public GameObject aimPrefab;
	public GameObject projectilePrefab;
}


public class PlayerControl : MonoBehaviour {

	public bool isHolding;
	public bool isNearShop;
	public float stamina, maxStamina;
	public float speed, speedMultiplier;
	public float throwStrength;
	//public Vector3 lookVector;
	public List<Weapon> weaponList;
	public Weapon currentWeapon;

	public Animator anim;

	public int score;

	EnemyControl enemyControl;

	float targetDistance;

	void Start () {
		isHolding = false;
		isNearShop = false;
		currentWeapon = weaponList [0];
	}
	
	void Update () {
		
//		Ray lookRay = Camera.main.ScreenPointToRay (Input.mousePosition);
//		RaycastHit lookHit;
//		Physics.Raycast (lookRay, out lookHit, 1000f);
//		lookVector = lookHit.point;
//
//		lookVector.y = transform.position.y;
//		transform.LookAt (lookVector);

		transform.eulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * 5f, 0f);

		if (Input.GetAxis ("Vertical") != 0)
			transform.Translate (transform.forward * speed * Input.GetAxis ("Vertical") * Time.deltaTime, Space.World);

		if (Input.GetAxis ("Horizontal") != 0)
			transform.Translate (transform.right * speed * Input.GetAxis ("Horizontal") * Time.deltaTime, Space.World);

		//!!!!!!!!!!Redo
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0)
			anim.SetBool ("isWalking", true);
		else
			anim.SetBool ("isWalking", false);

		//Use weapon or Throw
		if (Input.GetMouseButtonDown (0))
		{
			if (!isHolding && currentWeapon.ammoCount > 0)
				StartCoroutine ("Aim");
			else
				if(isHolding)
					Throw();
		}

		//Try to grab or drop smth
		if (Input.GetMouseButtonDown (1))
		{
			if (!isHolding)
				Grab();
			else
				Drop();
		}
			
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
				enemyControl.isGrabbed = true;
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
		enemyControl.isGrabbed = false;
		enemyControl = null;
	}

	public void Drop()
	{
		isHolding = false;
		enemyControl.transform.parent = null;
		enemyControl.isGrabbed = false;
		enemyControl = null;
	}

	IEnumerator Aim()
	{
		anim.SetTrigger ("Aim");

		switch(currentWeapon.aimingType)
		{
			case 1:
				float t = 0;
				float dir = 1;

				GameObject aimGO = Instantiate (currentWeapon.aimPrefab, transform.position, transform.localRotation);

				while (!Input.GetMouseButtonUp (0))
				{
					if (Input.GetMouseButtonDown (1)) {
						Destroy (aimGO);
						yield break;
					}

					t += dir * Time.deltaTime / currentWeapon.aimLoopTime;

					if (t > 1 || t < 0)
						dir = -dir;
				
					aimGO.transform.position = Vector3.Lerp (transform.position, transform.position + transform.forward * currentWeapon.maxTargetDistance, t);
					yield return null;

				}

				Shoot (aimGO.transform.position);
				Destroy (aimGO);
				break;


			default :
				Shoot (transform.position + transform.forward.normalized);
				break;
		}
	}

	void Shoot(Vector3 pos)
	{
		anim.SetTrigger ("Throw");

		GameObject proj;
		currentWeapon.ammoCount--;

		switch(currentWeapon.aimingType)
		{
			case 1:				
				proj = Instantiate (currentWeapon.projectilePrefab, transform.position, transform.localRotation);
				proj.GetComponent<ProjectileControl> ().targetPos = pos;
				break;


			default:
				proj = Instantiate (currentWeapon.projectilePrefab, pos, transform.localRotation);
				break;
		}
	}

	public void AddAmmo()
	{
		foreach (Weapon w in weaponList)
		{
			if (w.ammoCount < w.maxAmmo)
				w.ammoCount++;
		}
	}

}
