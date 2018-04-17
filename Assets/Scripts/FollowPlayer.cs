using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;
	[Range(-5, -15)]
	public float positionZ;
	
	void Update () {
		transform.position = player.transform.position;
		transform.position = new Vector3(transform.position.x, transform.position.y, positionZ);
	}
}
