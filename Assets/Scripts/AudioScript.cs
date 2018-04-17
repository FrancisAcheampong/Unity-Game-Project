using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

	private AudioSource audio;
	public bool playDelayed;
	public bool loop;


	void Start(){

		audio = GetComponent<AudioSource>();

		if (!playDelayed)
			audio.Play();
		else {
			audio.volume = 0;
			
		}

		audio.loop = loop;
			
	}

	void Update(){
		if (playDelayed){
			fadeInAudio();
		}
	}

	void fadeInAudio(){
		if (audio.volume < 0.3f) {
        	audio.volume += 0.03f * Time.deltaTime;
     	} else if (audio.volume >= 1){
			audio.volume = 1;
			playDelayed = false;
		}
	}
}
