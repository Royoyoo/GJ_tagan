using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum reactionType {OFFENCE, SCARED, NEUTRAL};

public class EnemyControl : MonoBehaviour {

	public bool isChasing;
	public NavMeshAgent thisAgent;
	public reactionType reaction;

	public PlayerControl playerScript;

	public float moveTime, moveInterval;


	void Start () {
		thisAgent = GetComponent<NavMeshAgent> ();
		playerScript = GameController.instance.playerScript;

		isChasing = false;
		moveTime = Time.time;
	}

	void Update () {
		
			//Free and see player
		if (reaction != reactionType.NEUTRAL)
			{
				ReactToPlayer (playerScript.threat);
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

	public void ReactToPlayer (float playerThreat)
	{
		if (playerThreat > 0.45f)
			reaction = reactionType.OFFENCE;
		else
			reaction = reactionType.SCARED;

		switch(reaction)
		{
			case reactionType.OFFENCE:
				thisAgent.destination = playerScript.transform.position;
				break;

			case reactionType.SCARED:
				thisAgent.destination = transform.position + (transform.position - playerScript.transform.position).normalized * 5f;
				//TODO: Stop agent, test few points for remainingDistance, reset and go for longest;
				break;

			default:
				break;
		}
	}
}
