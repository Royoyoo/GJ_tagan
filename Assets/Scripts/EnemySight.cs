using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	public EnemyControl thisEnemyControl;


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
			GameController.instance.playerScript.enemyNear.Add (thisEnemyControl);
			//thisEnemyControl.isChasing = true;
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
		{
			GameController.instance.playerScript.enemyNear.Remove (thisEnemyControl);
			thisEnemyControl.reaction = reactionType.NEUTRAL;
		}
	}
}
