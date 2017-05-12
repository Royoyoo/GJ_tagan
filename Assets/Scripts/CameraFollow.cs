using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Vector3 camOffsetT;
	public Vector3 camOffsetR;
	public float smoothFactorT;
	public float smoothFactorR;
	Transform playerTransform;

	void Start () {
		playerTransform = GameController.instance.playerGO.transform;
		transform.position = playerTransform.position + (camOffsetT.x * playerTransform.right + camOffsetT.y * playerTransform.up + camOffsetT.z * playerTransform.forward);
		transform.rotation = GameController.instance.playerGO.transform.rotation;
			
	}

	void FixedUpdate () {
		playerTransform = GameController.instance.playerGO.transform;
		transform.position = Vector3.Lerp(transform.position, playerTransform.position +
			(camOffsetT.x * playerTransform.right + camOffsetT.y * playerTransform.up + camOffsetT.z * playerTransform.forward), smoothFactorT);
		transform.rotation = Quaternion.Lerp(transform.rotation, GameController.instance.playerGO.transform.rotation * Quaternion.Euler(camOffsetR), smoothFactorR);
	}
}
