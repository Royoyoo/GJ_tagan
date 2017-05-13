using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum reactionType {OFFENCE, SCARED, NEUTRAL};

public class EnemyControl : MonoBehaviour {

	public bool isChasing;
	public NavMeshAgent thisAgent;
	public reactionType reaction;

	public Animator anim;

	public PlayerControl playerScript;

	public float randomMoveTime, randomMoveInterval;
	public float lastCatchTime, catchInterval;


	void Start () {
		thisAgent = GetComponent<NavMeshAgent> ();
		playerScript = GameController.instance.playerScript;

		isChasing = false;
		randomMoveTime = Time.time;
		lastCatchTime = Time.time;
	}

	void Update () {

		if (Vector3.Distance(transform.position, playerScript.transform.position) < .75f && Time.time > lastCatchTime + catchInterval)
		{
			anim.SetFloat ("Speed", 0f);
			anim.SetTrigger ("Catch");
			lastCatchTime = Time.time;
		}

			//Free and see player
			if (reaction != reactionType.NEUTRAL)
			{
			thisAgent.speed = 5f;
			anim.SetFloat ("Speed", thisAgent.velocity.magnitude/5);

				//ReactToPlayer (playerScript.threat);
			}
			//Free movement
			else
			{
			thisAgent.speed = 2f;
			anim.SetFloat ("Speed", thisAgent.velocity.magnitude/5);

				if (Time.time > randomMoveTime + randomMoveInterval)
				{
					thisAgent.destination = new Vector3 (Random.Range (-15f, 15f), 0f, Random.Range (-15f, 15f));
					randomMoveTime = Time.time;
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

	public void TryCatch()
	{
		Debug.Log ("!!!");
		if (Vector3.Distance (transform.position, playerScript.transform.position) < 0.7f && Vector3.Dot (transform.forward, -transform.position + playerScript.transform.position) > 0.5f)
			Debug.Log ("Caught");
		else
			Debug.Log(Vector3.Distance (transform.position, playerScript.transform.position).ToString() + "   "
				+ Vector3.Dot (transform.forward, -transform.position + playerScript.transform.position).ToString());
	}
}
