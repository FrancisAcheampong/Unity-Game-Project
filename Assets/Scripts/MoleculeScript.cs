using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeScript : MonoBehaviour {


	private GameObject target;
	private Rigidbody2D rb;
	private float speed = 250;
	private bool inPursuit = false;
	[Header("Options")]
	public float floatingTime;
	public float range;
	public int maxDrag;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		target = GameObject.Find("player1");
		StartCoroutine(StartPursuit(floatingTime));
	}

	public enum mat{
		carbon,
		silicon,
		metal
	}
	private mat material;

	void Update(){
		if (inPursuit){
			FlyTowardsPlayer();
			speed++;
			if (!(rb.drag > maxDrag)){
				rb.drag++;
			}
		}

		if (Vector2.Distance(transform.position, target.transform.position) < range){
			//TODO: Add resources to player.
			target.GetComponent<PlayerHealth>().Heal(1);
			Destroy(this.gameObject, 0.01f);
		}
	}

	private IEnumerator StartPursuit(float time){
		yield return new WaitForSeconds(time);
		inPursuit = true;
	}
	
	void FlyTowardsPlayer(){
		var dir = target.transform.position-transform.position;
		rb.AddForce(dir * speed);
	}

	public void SetMaterial(mat mat){
		material = mat;
	}
}
