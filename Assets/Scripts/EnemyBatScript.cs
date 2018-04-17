using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatScript : MonoBehaviour {

	private Rigidbody2D rb;
	private EnemyHealth health;

	[Header("Movement")]
	private Vector3 targetPosition;
	private int targetUpdateCounter;
	public int targetUpdateSpeed;
	public float MinDistance;
	public float moveSpeed;
	public bool debugTrayectory = false;

	public bool canMove;

	[Space(10)]
	[Header("Damage Feedback")]
	public float knockBackForce;
	public float stunTimeInSeconds;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//targetUpdateSpeed += Random.Range(1,9);
		rb.drag += Random.Range(0.1f,0.5f);
		moveSpeed += Random.Range(1,15);
		health = GetComponent<EnemyHealth>();
	}
	
	void Update () {
		if (targetUpdateCounter >= targetUpdateSpeed){
			targetPosition = GetPlayerPosition();
			targetUpdateCounter = 0;
			//print("TP: "+targetPosition);
		} else targetUpdateCounter++;

		if (debugTrayectory){
			Debug.DrawRay(transform.position, targetPosition-transform.position, Color.red);
			Debug.DrawRay(transform.position, transform.position-targetPosition, Color.green);
		}

		//Old movement (manually adding speed to velocity vector)
		// rb.velocity = new Vector2( (targetPosition.x>transform.position.x? moveSpeed:-moveSpeed), targetPosition.y>transform.position.y? moveSpeed:-moveSpeed );
		
		//New Movement
		//Using addforce to allow the smoother physics simulation to take care of the movement
		//var sin = 0.2f * Mathf.Sin(2*Time.time);

		if (canMove && Vector2.Distance(transform.position, targetPosition) < MinDistance){
			var me = transform.position;
			var tp = targetPosition;
			//rb.AddForce( new Vector2( Mathf.Sign(tp.x-me.x), Mathf.Sign(tp.y-me.y) ) * moveSpeed);
			rb.AddForce( (tp-me).normalized * moveSpeed);
		}

		if (health.isHurt){
			StartCoroutine(Stun(stunTimeInSeconds));
			KnockBack(knockBackForce);
			// print("KNOCKBACK");
		}
	}

	Vector3 GetPlayerPosition(){
		//TODO: add player 2 targeting and select closest player
		Vector3 pos1;

		pos1 = GameObject.Find("player1").transform.position;

		return pos1;
	}

	private IEnumerator Stun(float stunTime){
		canMove = false;
		transform.GetComponent<SpriteRenderer>().color = new Color(0.5f,1,1,1);
		yield return new WaitForSeconds(stunTime);
		canMove = true;
		transform.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
	}

	private void KnockBack(float force){
		rb.velocity = Vector2.zero;
		rb.AddForce( (transform.position-targetPosition).normalized * force );
	}
}
