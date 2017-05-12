using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum reactionType {OFFENCE, SCARED, NEUTRAL};

public class EnemyControl : MonoBehaviour {

	public bool isChasing;
	public bool isGrabbed;
	public NavMeshAgent thisAgent;
	public reactionType reaction;

	public float moveTime, moveInterval;


	void Start () {
		thisAgent = GetComponent<NavMeshAgent> ();
		isChasing = false;
		isGrabbed = false;
		moveTime = Time.time;
	}

	void Update () {
		if (!isGrabbed)
		{
			thisAgent.isStopped = false;	//???

			//Free and see player
			if (isChasing)
			{
				ReactToPlayer ();
			}
			//Free movement
			else
			{
				if (Time.time > moveTime + moveInterval)
				{
					thisAgent.destination = new Vector3 (Random.Range (-15f, 15f), 0f, Random.Range (-15f, 15f));
					moveTime = Time.time;
				}
			}
		}
		//Grabbed
		else
			thisAgent.isStopped = true;
	}

	void ReactToPlayer ()
	{
		switch(reaction)
		{
			case reactionType.OFFENCE:
				thisAgent.destination = GameController.instance.playerGO.transform.position;
				break;

			case reactionType.SCARED:
				thisAgent.destination = transform.position + (transform.position - GameController.instance.playerGO.transform.position).normalized * 5f;
				//TODO: Stop agent, test few points for remainingDistance, reset and go for longest;
				break;

			default:
				break;
		}
	}
}
