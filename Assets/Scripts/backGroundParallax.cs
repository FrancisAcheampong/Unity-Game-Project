using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGroundParallax : MonoBehaviour {

	private Vector2 startingPosition;
	private Vector3 offsetPosition;
	public float offset;
	public float depth;

	void Start () {
		startingPosition = transform.position;	
	}
	
	void Update () {
		var pos = startingPosition;
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;

		offsetPosition = new Vector3(pos.x - (mousePos.x*offset), pos.y*offset, depth);
		transform.position = offsetPosition;
	}
}
