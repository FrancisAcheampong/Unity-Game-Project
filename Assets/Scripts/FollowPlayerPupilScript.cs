using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerPupilScript : MonoBehaviour {

	public GameObject player;
	public float val;
	public float vibRange = 0;
	public bool followPlayer;

	public Sprite idle;
	public Sprite fire;
	public Sprite dying;

	void Update () {
		if (followPlayer){
			var xx = player.transform.position.x - transform.parent.position.x;
			var yy = player.transform.position.y - transform.parent.position.y;
			var vec = new Vector3( Mathf.Clamp(xx, -val+Random.Range(-vibRange, vibRange), val+Random.Range(-vibRange, vibRange)), Mathf.Clamp(yy, -val+Random.Range(-vibRange, vibRange), val+Random.Range(-vibRange, vibRange)) , -2 );
			transform.localPosition = vec;
		}
		
	}
}
