using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelName : MonoBehaviour {

	private Text Text;
	[Range(0.7f,1.5f)]
	public float ls;

	void Start(){
		Text = GetComponent<Text>();
	}

	void Update(){
		Text.lineSpacing = ls;
	}

	public void UpdateName(int level){
		Text.text = "Level " + level.ToString();
	}

	
}
