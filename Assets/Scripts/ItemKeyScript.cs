using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeyScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.name.Contains("player")){
			var p = coll.gameObject.GetComponent<PlayerController>();
			p.keys++;
			p.UpdateHUD();
			GameObject.Destroy(this.gameObject, 0.01f);
		}
	}

}
