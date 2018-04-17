using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeScript : MonoBehaviour {

	public GameObject player;
	public float rangeToAttack;
	public int countdownToAttack;
	private int attackCounter;
	public GameObject eyeProjectile;

	public Sprite[] s_left = new Sprite[2];
	public Sprite[] s_down  = new Sprite[2];
	public Sprite[] s_up    = new Sprite[2];

	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>();
		player = GameObject.Find("player1");
	}
	
	void Update () {

		var ppos = player.transform.position;
		var pos  = transform.position;

		//HAS DETECTED PLAYER?
		var state = 0;
		if (Vector2.Distance(this.transform.position, player.transform.position) > rangeToAttack && player != null){
			state = 0;
			attackCounter = 0;
		} else {
			state = 1;	
			attackCounter++;
		}

		//ATTACK PLAYER
		if (attackCounter >= countdownToAttack){
			attackCounter = 0;
			ShootPlayer();
		}

		//ANIMATE
		sr.flipX = (ppos.x > pos.x);

		if ( Mathf.Abs(ppos.x - pos.x) > 3.5f ){
			sr.sprite = s_left[state];
		} else {
			if (ppos.y < pos.y){
				sr.sprite = s_down[state];
			} else {
				sr.sprite = s_up[state];
			}
		}
	}
	
	void ShootPlayer(){
		var obj = Instantiate(eyeProjectile, transform.position + new Vector3(0,0,-2), Quaternion.identity);
		obj.GetComponent<EyeProjectileScript>().direction = (Vector2)(player.transform.position - transform.position).normalized;
		obj.GetComponent<EyeProjectileScript>().Setup();
	}

}
