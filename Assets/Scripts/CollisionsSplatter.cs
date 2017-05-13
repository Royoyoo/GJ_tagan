using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsSplatter : MonoBehaviour {

	public PlayerControl playerScript;

	void Start()
	{
		playerScript = GameController.instance.playerScript;
	}

	void OnCollisionStay(Collision collision)
	{
		if (playerScript.moveObjectsPaintTimer < playerScript.movePaintInterval || playerScript.mud < 0)
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
//			if (p.otherCollider != null)
//			{
//				brush.Color = brushColors.Evaluate (Random.Range(0f,1f));
//				brush.Scale = Random.Range (0.01f, 0.1f) * canvas.UVScale;
//				canvas.Paint (brush, p.point);
//				//Debug.Log ("Painted");
//			}
		}
	}
}
