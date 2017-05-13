using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsSplatter : MonoBehaviour {

	public PlayerControl playerScript;

	public GameObject TestPrefab;

	List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	void Start()
	{
		playerScript = GameController.instance.playerScript;
	}

	void OnCollisionStay(Collision collision)
	{
		if (playerScript.moveObjectsPaintTimer < playerScript.movePaintInterval || playerScript.mud <= 0)
			return;
		
		playerScript.moveObjectsPaintTimer = 0;

		foreach(var p in collision.contacts)
		{
			//Debug.Log (p.otherCollider.gameObject.name);
			playerScript.mud -= 2f;
			playerScript.score += 5f;
			if (playerScript.mud < 0)
				break;
			//p.otherCollider.tag;
			if (p.otherCollider != null)
			{
				//HandsPS.transform.position = p.point;

				Ray ray = new Ray (transform.position, (p.point - transform.position).normalized);
				RaycastHit outHit;
				if (Physics.Raycast (ray, out outHit, 5f, 1 << 14))
				{
					Debug.DrawRay (p.point, outHit.normal);
					Instantiate(TestPrefab, p.point, Quaternion.LookRotation (outHit.normal));
				}
			}
		}
	}
}



