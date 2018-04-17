using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonScript : MonoBehaviour {


	public enum st{
		hidden,
		hiding,
		rising,
		walking
	}
	public st state = st.walking;

	public float walkingSpeed = 6.5f;
	public float hiddenSpeed = 1f;
	public GameObject player;
	public float knockBackForce;
	public bool canMove = true;
	public float stunTimeInSeconds;

	private Rigidbody2D rb;
	private Animator an;
	private Animation animation;
	private BoxCollider2D bc;
	private EnemyHealth health;

	private bool inTransition = false;

	void Start(){
		rb = GetComponent<Rigidbody2D>();
		an = GetComponent<Animator>();
		bc = GetComponent<BoxCollider2D>();
		animation = GetComponent<Animation>();
		health = GetComponent<EnemyHealth>();
	}

	void OnTriggerStay2D(Collider2D coll){
		if (coll.transform.name.Contains("Ground")){
			if(state == st.hidden){
				transform.position = new Vector3(transform.position.x, transform.position.y, 1);
			}
		} 
	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.transform.name.Contains("Ground")){
			if(state == st.hidden){
				transform.position = new Vector3(transform.position.x, transform.position.y, 25);
			}
		} 
	}


	void Update() {

		GetComponent<SpriteRenderer>().flipX = (player.transform.position.x < transform.position.x);

		if (Vector2.Distance(player.transform.position, transform.position) < 20){
			//inPursuit = true;
		} else {
			//inPursuit = false;
			return;
		}


		if (health.isHurt){
			//print("I was hurt");
			StartCoroutine(TransitionTo("hidden"));
			KnockBack(knockBackForce);
			StartCoroutine(Stun(stunTimeInSeconds));
		}


		if (inTransition == false){
			if (Vector2.Distance(player.transform.position, transform.position) < 5){
				if (state == st.hidden)
					StartCoroutine(TransitionTo("walking"));
				//state = st.walking;
			} else {
				if (state != st.hidden)
					StartCoroutine(TransitionTo("hidden"));
				//state = st.hidden;
			}
		}

		

		switch(state){
			case st.walking:
				if (canMove)
					rb.velocity = (player.transform.position - transform.position).normalized*walkingSpeed;
				bc.enabled = true;
				break;

			case st.hidden:
				if (canMove)
					rb.velocity = (player.transform.position - transform.position).normalized*hiddenSpeed;
				bc.enabled = false;
				break;
				
			case st.rising:
				break;

			case st.hiding:
				break;
		}


	}

	private void KnockBack(float force){
		rb.velocity = Vector2.zero;
		rb.AddForce( (transform.position-player.transform.position).normalized * force );
	}

	private IEnumerator TransitionTo(string str){
		if (str == "walking"){

			state = st.rising;
			an.SetBool("rising", true);
			an.SetBool("hidden", false);

			inTransition = true;

			yield return new WaitForSeconds(2f);
			
			state = st.walking;
			an.SetBool("walking", true);
			an.SetBool("rising", false);

			inTransition = false;

		} else if (str == "hidden"){

			state = st.hiding;
			an.SetBool("hiding", true);
			an.SetBool("walking", false);

			inTransition = true;

			yield return new WaitForSeconds(0.5f);

			state = st.hidden;
			an.SetBool("hiding", false);
			an.SetBool("hidden", true);

			inTransition = false;
		}
	}

	private IEnumerator Stun(float stunTime){
		canMove = false;
		state = st.hidden;
		transform.GetComponent<SpriteRenderer>().color = new Color(0.5f,1,1,1);
		yield return new WaitForSeconds(stunTime);
		canMove = true;
		transform.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	}
	
}
