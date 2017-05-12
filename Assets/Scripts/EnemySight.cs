using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour {

	public EnemyControl thisEnemyControl;


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
			thisEnemyControl.isChasing = true;
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player")
			thisEnemyControl.isChasing = false;
	}
}
