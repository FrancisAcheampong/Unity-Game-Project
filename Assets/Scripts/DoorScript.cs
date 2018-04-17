using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

	public Sprite smallDoor;
	public Sprite gateDoor;
	private GameManagerScript GM;

	public enum d{
		small,
		gate
	}
	public d type;


	void Start(){
		GM = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		var sr = GetComponent<SpriteRenderer>();

		if (type == d.small){
			sr.sprite = smallDoor;
		} else {
			sr.sprite = gateDoor;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.transform.name.Contains("player")){
			if (other.gameObject.GetComponent<PlayerController>().keys > 0){
				var p = other.gameObject.GetComponent<PlayerController>();
				p.keys--;
				p.UpdateHUD();
				OpenDoor();
			} else {
				//if (GM.isMessageEnabled() == false)
					GM.DisplayMessage(true);
					GM.messageDisplay.text = "You need a key to open the door...";
			}
		}
	}

	void OpenDoor(){
		//print("Opening Door");
		GameObject.Destroy(this.gameObject, 0.1f);
		
		
	}
}
