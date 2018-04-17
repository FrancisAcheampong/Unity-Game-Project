using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UImenuManager : MonoBehaviour {

	public void StartGame(){
		SceneManager.LoadScene(1);
	}

	public void CloseGame(){
		Application.Quit();
	}

	public void BackToMenu(){
		SceneManager.LoadScene(0);
	}
}
