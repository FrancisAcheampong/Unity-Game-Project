using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	private float health;
	public float maxHealth;
	public GameObject deadSprite;

	public float contactDmg;

	public bool isHurt;
	public bool canBeHurt;
	public float damageBoostTime = 1;
	public bool invin = false;

	void Start () {
		health = maxHealth;
		if (!invin)
			canBeHurt = true;
	}
	
	void Update () {
		//isHurt = false;
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.name == "player1"){
			//print("Collision 2D Enter");
			coll.gameObject.GetComponent<PlayerController>().Hurt(contactDmg);
			//coll.gameObject.GetComponent<PlayerController>().KnockBack(5000, transform.position);
		} else {
			//print("Collided with:" + coll);
		}
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(coll.gameObject.name == "SlashArea"){
			//print("Collided with slash");
			if (canBeHurt) 
				Hurt(1);
		} else if(coll.gameObject.name.Contains("Arrow")){
			//print("Collided with arrow");
			if (canBeHurt){
				Hurt(2);
				GameObject.Destroy(coll.gameObject, 0.01f);
			}
		} else if(coll.gameObject.name.Contains("Spear")){
			if (canBeHurt) 
				Hurt(2);
		} else if(coll.gameObject.name == "player1"){
			coll.gameObject.GetComponent<PlayerController>().Hurt(contactDmg);
		}

	}

	public void Hurt(float dmg){
		isHurt = true;
		StartCoroutine(SetIsHurtInSeconds(0.05f));
		StartCoroutine(DamageBoost(damageBoostTime));
		health -= dmg;
		if (health < 0) health = 0;
		if (health == 0){
			if (deadSprite != null)
				Instantiate(deadSprite, transform.position, Quaternion.identity);
			DieDestroy();
		}
	}

	void DieDestroy(){
		Destroy(this.gameObject, 0.1f);
		GetComponent<EnemyDrops>().Drop();
	}

	public IEnumerator DamageBoost(float t){
		canBeHurt = false;
		yield return new WaitForSeconds(t);
		canBeHurt = true;
	}

	public IEnumerator SetIsHurtInSeconds(float t){
		isHurt = true;
		yield return new WaitForSeconds(t);
		isHurt = false;
	}
}
