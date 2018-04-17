using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoofScript : MonoBehaviour {

	void Start(){
		Destroy(this.gameObject, 0.6f);
	}

	// private IEnumerator DeleteInSeconds(float t){
	// 	yield return new WaitForSeconds(t);
	// 	Destroy(this.)
	// }
}
