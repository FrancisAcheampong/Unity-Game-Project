using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
	
	public int wave;

	void Update () {
		if (transform.childCount == 0){
			transform.name = "Wave - Done";
			GameObject.Find("Eye").GetComponent<BossManagerScript>().WaveDone(wave);
			gameObject.SetActive(false);
		}		
	}
}
