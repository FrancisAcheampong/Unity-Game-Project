using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObstacle : MonoBehaviour {

	private Rigidbody2D rb;
	private int rot = 0;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		rb.MoveRotation(rot++);
		if (rot >= 360) rot = 0;
	}
}
