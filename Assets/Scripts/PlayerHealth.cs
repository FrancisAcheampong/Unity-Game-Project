using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth;
	public float health;
	public float invulnerableTime;
	public bool canBeHurt = true;

	void Start () {
		health = maxHealth;
	}

	public void Hurt(float dmg){
		if(canBeHurt){
			health -= dmg;
			StartCoroutine(DamageBoost(2));
			if (health < 0){
				health = 0;
				OnDeath();
			}
			GameObject.Find("Player Panel").GetComponent<UIPlayerHUD>().UpdateHud();
			StartCoroutine(GameObject.Find("Player Panel").GetComponent<UIPlayerHUD>().Flash(Color.red));

		}

	}

	public void Heal(float amt){
		health += amt;
		if (health > maxHealth)
			health = maxHealth;
		GameObject.Find("Player Panel").GetComponent<UIPlayerHUD>().UpdateHud();
		StartCoroutine(GameObject.Find("Player Panel").GetComponent<UIPlayerHUD>().Flash(Color.green));
	}

	public void IncreaseMaxHealth(float newMaxHealth){
		maxHealth = newMaxHealth;
	}

	public void HealAll(){
		health = maxHealth;
	}

	public bool isAtMaxHealth(){
		if (health == maxHealth) 
			return true;
		else if (health == 0){
			print("Health is at 0");
			return false;	
		} else 
			return false;
	}

	public void OnDeath(){
		if (SceneManager.GetActiveScene().buildIndex != 3)
			SceneManager.LoadScene(4); //load death scene
		else {
			SceneManager.LoadScene(3); //reload boss
			HealAll();
		}
	}

	public IEnumerator DamageBoost(float t){
		canBeHurt = false;
		GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
		yield return new WaitForSeconds(t);
		GetComponent<SpriteRenderer>().color = Color.white;
		canBeHurt = true;
	}

	
}

