using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	private float levelTimer;
	public Text timerText;
	//private bool messageDisplayisOn = false;
	public Text messageDisplay;

	void Start(){

	}

	void Update(){

		levelTimer += Time.deltaTime;
		
		if (timerText != null){
			timerText.text = ("Time\n" + levelTimer.ToString("0"));
		}
	}

	public void DisplayMessage(bool status, bool erase = true){
		messageDisplay.gameObject.SetActive(status);
		if (status && erase)
			StartCoroutine(DisableMessageInSeconds(3.5f));
	}

	public bool isMessageEnabled(){
		return messageDisplay.IsActive();
	}

	private IEnumerator DisableMessageInSeconds(float lenght = 2f){
		yield return new WaitForSeconds(lenght);
		DisplayMessage(false);
	}


}
