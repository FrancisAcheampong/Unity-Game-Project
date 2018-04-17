using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endingScript : MonoBehaviour {

	public int sceneToLoad;

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.name.Contains("player")){
			SceneManager.LoadScene(sceneToLoad);
		}
	}

}
